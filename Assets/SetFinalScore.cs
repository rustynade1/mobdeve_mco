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
        scoreText.text = "Final Score: " + StaticData.finalScore;
    }
}
