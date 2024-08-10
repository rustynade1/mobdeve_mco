using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetFinalScore : MonoBehaviour
{
    private Text scoreText;

    public void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("FinalScoreText").GetComponent<Text>();
        Debug.Log("test score   : "+ GlobalScore.totalScore);
        scoreText.text = "Final Score: " + GlobalScore.totalScore;
        Debug.Log("test score 2 :" + GlobalScore.totalScore);
    }
}
