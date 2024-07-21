using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomLetters : MonoBehaviour
{
    //name of letter folder
    public string letterFolder = "Letters";
   
    //grid to be filled
    public GameObject[] letterGrid;

    //selection of letters (adjusted in unity inspector)
    private List<Sprite> letterList;

    //adjusting letter sprite size
    public Vector3 spriteScale = new Vector3(0.25f, 0.25f, 1.0f);
    //spacing for formatting
    public float spacing = 0.5f;

    //reshuffle button (tentative)
    public Button reshuffleButton;
    public Button otherButton;
    public Button eraseButton;

    // dictionary for tile mapping
    public Dictionary<string, (int value, char character)> letterMap;

    private List<TileDetails> clickedTileDetails = new List<TileDetails>();

    private List<GameObject> clonedTiles = new List<GameObject>();
    public Vector3 cloneStartPos = new Vector3(5.0f, 1.5f, 0);
    private Vector3 nextClonePos;

    //for verifying a word and determining its score
    private Animator animator;
    public string wordSpelled;
    public int wordScore;
    public SpellChecker spellChecker;
    public bool isValid;//not needed?

    public static RandomLetters instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       letterList = new List<Sprite>(Resources.LoadAll<Sprite>(letterFolder));
       InitializeLetterMappings();
       nextClonePos = cloneStartPos;
       wordSpelled = string.Empty;
       wordScore = 0;
       spellChecker = new SpellChecker();
       animator = GetComponent<Animator>();

        for (int i = 0; i < 16; i++){

        if (letterGrid[i] != null){
          Sprite randomLetter = GetRandomLetter();
          SpriteRenderer spriteRenderer = letterGrid[i].GetComponent<SpriteRenderer>();
          if (spriteRenderer != null){
                spriteRenderer.sprite = randomLetter;
          }
          LetterValues letterValues = letterGrid[i].GetComponent<LetterValues>();
          if(letterValues == null)
          {
                    letterValues = letterGrid[i].AddComponent<LetterValues>();
          }

         (int val, char character) letterInfo = GetSpriteInfo(randomLetter.name);
         letterValues.SetLetterValues(randomLetter,letterInfo.val,letterInfo.character);
         //resize
         letterGrid[i].transform.localScale = spriteScale;

         //adjust position
         float x = ((i%4) * spacing)+1f;//i%4
         float y = (-((i)/4) * spacing)-2;
         letterGrid[i].transform.position = new Vector3(x, y, 0);

         BoxCollider2D collider = letterGrid[i].GetComponent<BoxCollider2D>();
         if (collider == null)
         {
           collider = letterGrid[i].AddComponent<BoxCollider2D>();
         }


        }
       }
       if (reshuffleButton != null)
        {
            reshuffleButton.onClick.AddListener(ReshuffleLetters);
            
        }
       if(otherButton != null)
        {
            otherButton.onClick.AddListener(PrintClickedTileDetails);
            
        }
       if(eraseButton != null)
        {
            eraseButton.onClick.AddListener(ResetSelection);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void ReshuffleLetters()
    {
        for (int i = 0;i < 16; i++)
        {
            if (letterGrid[i] != null)
            {
                SpriteRenderer spriteRenderer = letterGrid[i].GetComponent<SpriteRenderer>();
                Sprite randomLetter = GetRandomLetter();
                spriteRenderer.sprite = randomLetter;

                LetterValues letterValues = letterGrid[i].GetComponent<LetterValues>();
                (int val, char character) letterInfo = GetSpriteInfo(randomLetter.name);
                letterValues.SetLetterValues(randomLetter, letterInfo.val, letterInfo.character);

                BoxCollider2D collider = letterGrid[i].GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }
        clickedTileDetails.Clear();
        RemoveClones();
        wordSpelled = string.Empty;
        wordScore = 0;
    }
   
    Sprite GetRandomLetter()
    {
        int randomIndex = Random.Range(0, letterList.Count);
        return letterList[randomIndex];
    }

    void InitializeLetterMappings()
    {
        letterMap = new Dictionary<string, (int value, char character)>();

        //spritename,points,character
        //based on scrabble for now

        letterMap.Add("letter_A", (1, 'a'));
        letterMap.Add("letter_E", (1, 'e'));
        letterMap.Add("letter_I", (1, 'i'));
        letterMap.Add("letter_O", (1, 'o'));
        letterMap.Add("letter_U", (1, 'u'));
        letterMap.Add("letter_L", (1, 'l'));
        letterMap.Add("letter_N", (1, 'n'));
        letterMap.Add("letter_S", (1, 's'));
        letterMap.Add("letter_T", (1, 't'));
        letterMap.Add("letter_R", (1, 'r'));

        letterMap.Add("letter_D", (2, 'd'));
        letterMap.Add("letter_G", (2, 'g'));

        letterMap.Add("letter_B", (3, 'b'));
        letterMap.Add("letter_C", (3, 'c'));
        letterMap.Add("letter_M", (3, 'm'));
        letterMap.Add("letter_P", (3, 'p'));

        letterMap.Add("letter_F", (4, 'f'));
        letterMap.Add("letter_H", (4, 'h'));
        letterMap.Add("letter_V", (4, 'v'));
        letterMap.Add("letter_W", (4, 'w'));
        letterMap.Add("letter_Y", (4, 'y'));

        letterMap.Add("letter_K", (5, 'k'));

        letterMap.Add("letter_J", (8, 'j'));
        letterMap.Add("letter_X", (8, 'x'));

        letterMap.Add("letter_Q", (10, 'q'));
        letterMap.Add("letter_Z", (10, 'z'));

    }

    (int value, char character) GetSpriteInfo(string spriteName)
    {
        if (letterMap.ContainsKey(spriteName))
        {
            return letterMap[spriteName];
        }
        else
        {
            // Return default values if the sprite is not in the specific mappings
            return (0, ' ');
        }
    }

    public void TileClicked(GameObject tile)
    {
        LetterValues letterValues = tile.GetComponent<LetterValues>();
        SaveTileDetail(letterValues.sprite, letterValues.val, letterValues.character);
        wordSpelled = wordSpelled + letterValues.character;
        wordScore = wordScore + letterValues.val;
        Debug.Log($"Current word:{wordSpelled}");
        DuplicateTile(tile);

        tile.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SaveTileDetail(Sprite sprite, int value, char character)
    {
        clickedTileDetails.Add(new TileDetails(sprite, value, character));
        Debug.Log($"Tile saved: {character}, Value: {value}");
        
    }

    public void PrintClickedTileDetails()
    {
        foreach (var detail in clickedTileDetails)
        {
            Debug.Log($"Sprite: {detail.sprite.name}, Value: {detail.value}, Character: {detail.character}");
        }
        if (spellChecker != null)
        {
            if (spellChecker.CheckWord(wordSpelled))
            {
                Debug.Log($"Word is valid, Score is {wordScore}");
                PlayValidWordAnimation(true);

            }
            else
            {
                Debug.Log($"Word is NOT valid");
            }
        }
        else
        {
            Debug.LogError("spellChecker is not initialized!");
        }
    }

    public void PlayValidWordAnimation(bool isValid)
    {
        Debug.Log("i am called");
        if (animator != null)
        {
            animator.SetBool("WordValid", isValid);
            // Optionally, reset the trigger after the animation plays
            StartCoroutine(ResetAnimationTrigger());
        }
    }

    private System.Collections.IEnumerator ResetAnimationTrigger()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("WordValid", false);
    }

    public void HandleValidWordAnimation()
    {
        Debug.Log("Handling valid word animation in RandomLetters script!");
        //me when chatgpt, pls delete
        
    }

    

    public void ResetSelection()
    {
        clickedTileDetails.Clear();
        RemoveClones();
        wordSpelled = string.Empty;
        wordScore = 0;
        for (int i = 0; i < 16; i++)
        {
            BoxCollider2D collider = letterGrid[i].GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    public void DuplicateTile(GameObject origTile) 
    {
        GameObject tileClone = Instantiate(origTile);
        tileClone.transform.localScale = spriteScale;
        tileClone.transform.position = nextClonePos;
        nextClonePos.x += spacing;

        clonedTiles.Add(tileClone);
    }

    public void RemoveClones()
    {
        foreach (GameObject clone in clonedTiles)
        {
            Destroy(clone);
        }
        clonedTiles.Clear();
        nextClonePos = cloneStartPos;
    }
}
