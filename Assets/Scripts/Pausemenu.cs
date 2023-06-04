using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Pausemanager Pausemanager;

    private void Awake() {
        Pausemanager = GetComponent<Pausemanager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(panel.activeInHierarchy == false)
            {
                OpenMenu();
            }else{
                CloseMenu();
            }
        }
    }
    public void CloseMenu(){
        Pausemanager.UnPauseGame();
        panel.SetActive(false);
    }

    public void OpenMenu(){
        Pausemanager.PauseGame();
        panel.SetActive(true);
    }
}
