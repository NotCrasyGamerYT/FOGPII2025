using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;

    void Start()
    {
        menuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuUI.SetActive(!menuUI.activeSelf);
        }

    }

    public void ResumeGame()
    {
        menuUI.SetActive(false);
        PauseController.SetPauseState(false);
    }

    public void PauseGame()
    {
        menuUI.SetActive(true);
        PauseController.SetPauseState(true);
    }

    public void LoadMainMenu()
    {
        PauseController.SetPauseState(false);
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
