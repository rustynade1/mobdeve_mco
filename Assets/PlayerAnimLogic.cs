using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimLogic : MonoBehaviour
{
    public RandomLetters randomLetters;

    public void OnButtonClick()
    {
        if (randomLetters != null)
        {
            randomLetters.PlayValidWordAnimation(true);
        }
        else
        {
            Debug.LogError("RandomLetters instance is not assigned!");
        }
    }
}
