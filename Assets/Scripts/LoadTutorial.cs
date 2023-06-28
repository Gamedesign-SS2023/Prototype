using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour
{
    public void Tutorial()
    {
        Debug.Log("Load Tutorial.");
        SceneManager.LoadScene("TutorialStage");
    }
}
