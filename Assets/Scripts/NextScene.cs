using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void NextScenes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
