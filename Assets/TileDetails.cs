using UnityEngine;

//Data model for tiles
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
