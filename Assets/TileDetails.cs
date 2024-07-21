using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetails : MonoBehaviour
{
    public Sprite sprite;
    public int value;
    public char character;

    public TileDetails(Sprite sprite, int value, char character)
    {
        this.sprite = sprite;
        this.value = value;
        this.character = character;
    }
}
