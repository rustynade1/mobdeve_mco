using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void choseYes()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void choseNo()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
