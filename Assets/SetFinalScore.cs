using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetFinalScore : MonoBehaviour
{
    [SerializeField] Text scoreText;

    public void Start()
    {
        //int getScore = ;
        scoreText.text = "Final Score: " + StaticData.finalScore;
    }
}
