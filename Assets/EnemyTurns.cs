//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyTurns : MonoBehaviour
{
    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;
    

    public string enemySpriteFolder = "Enemies/Dummy";
    private Sprite[] sprites;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
    private Text scoreText;
    private int scoreCount = 0;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
        sprites = Resources.LoadAll<Sprite>(enemySpriteFolder);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();

        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthBar.hp <= 0)
        {   
            //switch in new enemy with full health
            CycleSprite();
            enemyHealthBar.FullRestore();
        }
    }
    
    //enemy damage is 1-7
    public void DamagePlayer()
    {
        int minDmg = 1;
        int maxDmg = 7;
        int damage = Random.Range(minDmg, maxDmg);
        playerHealthBar.TakeDamage(damage);

    }
    // Both start at 30hp, player heals 10hp after each kill
    public void DamageEnemy(int wordScore)
    {
        enemyHealthBar.TakeDamage(wordScore);
        UpdateScore(wordScore);

        if (enemyHealthBar.hp >= 1)
        {
            DamagePlayer();
        }
        else
        {
            playerHealthBar.Heal(10);
            //preventing overheal
            if(playerHealthBar.hp >= 30)
            {
                playerHealthBar.hp = 30;
            }
            

        }
        
    }

    void UpdateScore(int wordScore)
    {
        scoreCount += wordScore;
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }

    void CycleSprite()
    {
        
        // Increment the sprite index
        currentSpriteIndex++;
        
        if (currentSpriteIndex >= sprites.Length)
        {
            currentSpriteIndex = 0; 
        }

        UpdateSprite();

    }

    void UpdateSprite()
    {

        animator.SetInteger("SpriteIndex", currentSpriteIndex);
    }
}
