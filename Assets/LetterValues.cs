using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterValues : MonoBehaviour
{
    public Sprite sprite;
    public int val;
    public char character;

    private RandomLetters randomLetters;

    private void Start()
    {
        randomLetters = FindAnyObjectByType<RandomLetters>();
    }
    private void OnMouseDown()
    {
        HandleClick();
    }
    public void SetLetterValues(Sprite newSprite, int newVal,char newChar)
    {
        sprite = newSprite;
        val = newVal;
        character = newChar;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    private void HandleClick()
    {
        Debug.Log($"Tile clicked: {character}, Value: {val}");
        //randomLetters.SaveTileDetail(sprite, val, character);
        //if (randomLetters != null)
        //{
        //    randomLetters.DuplicateTile(gameObject);
        //}
        if (randomLetters != null)
        {
            randomLetters.TileClicked(gameObject);
        }

    }

    
}
