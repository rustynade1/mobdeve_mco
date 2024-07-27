using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemyTurns : MonoBehaviour
{
    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;

    public string enemySpriteFolder = "Enemies/Dummy";
    private Sprite[] sprites;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;


    
    // Start is called before the first frame update
    void Start()
    {
        
        sprites = Resources.LoadAll<Sprite>(enemySpriteFolder);

        
        spriteRenderer = GetComponent<SpriteRenderer>();


        
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
    
    public void DamagePlayer()
    {
        int minDmg = 1;
        int maxDmg = 7;
        int damage = Random.Range(minDmg, maxDmg);
        playerHealthBar.TakeDamage(damage);

    }

    public void DamageEnemy(int wordScore)
    {
        enemyHealthBar.TakeDamage(wordScore);
        
        
        if(enemyHealthBar.hp >= 1)
        {
            DamagePlayer();
        }
        else
        {
            playerHealthBar.Heal(10);

        }
        
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
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

    }
}
