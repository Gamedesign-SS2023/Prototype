using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialStage");
    }

    public void StartGameplay()
    {
        SceneManager.LoadScene("GameplayStage");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
