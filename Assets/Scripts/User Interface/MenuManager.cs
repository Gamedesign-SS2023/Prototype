using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject Pause;
    //[SerializeField] GameObject gameOverMenu;
    Pausemanager Pausemanager;
    public AudioSource clickAudio;

    private void Awake()
    {
        Pausemanager = GetComponent<Pausemanager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause.activeInHierarchy == false)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    public void CloseMenu()
    {
        Pausemanager.UnPauseGame();
        Pause.SetActive(false);
    }

    public void OpenMenu()
    {
        Pausemanager.PauseGame();
        Pause.SetActive(true);
    }

    public void BackToMainMenu()
    {
        clickAudio.PlayOneShot(clickAudio.clip);
        Pausemanager.UnPauseGame();
        SceneManager.LoadScene("Main Menu");
    }

    public void Reload()
    {
        clickAudio.PlayOneShot(clickAudio.clip);
        Pausemanager.UnPauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
