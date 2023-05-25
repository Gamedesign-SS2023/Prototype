using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
        SceneManager.LoadScene("Main Menu");
    }
}
