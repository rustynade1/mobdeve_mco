using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//Logic for mark's animations
public class PlayerAnimLogic : MonoBehaviour
{
    public static PlayerAnimLogic instance;
    public UnityEngine.UI.Button atkButton;
    public RandomLetters randomLetters;
    public GameObject animatorGameObject;
    private Animator animator;
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

    void Start()
    {
        if (atkButton != null)
        {
            atkButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component is not assigned!");
        }

        if (animatorGameObject != null)
        {
            animator = animatorGameObject.GetComponent<Animator>();
        }
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the specified GameObject!");
        }
    }

    void Update()
    {
        PlayWritingAnimation();
    }

    void OnDestroy()
    {
        if (atkButton != null)
        {
            atkButton.onClick.RemoveListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (randomLetters != null)
        {
            randomLetters.PrintClickedTileDetails();
           
        }
        else
        {
            Debug.LogError("RandomLetters instance is not assigned!");
        }
    }

    public void PlayValidWordAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("WordIsValid");
        }
    }
    
    public void PlayWritingAnimation()
    {
        if (animator != null)
        {
            if (randomLetters.clickedTileDetails.Count > 0)
            {
                animator.SetBool("LettersSelected",true);
            }
            else
            {
                animator.SetBool("LettersSelected", false);
            }
            
        }
    }
    
}
