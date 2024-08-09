using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleScene : MonoBehaviour
{
    public void MenuReturn()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
