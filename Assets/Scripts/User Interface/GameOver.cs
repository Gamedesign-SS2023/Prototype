using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    //public TextMeshProUGUI highScore;
    public AudioSource gameOverAudio;
    public AudioSource victoryAudio;

    public void GameOverPanel(bool death)
    {
        AudioSource popUpSound = death ? gameOverAudio : victoryAudio;
        popUpSound.Play();

        GetComponent<Pausemanager>().PauseGame();

        StartCoroutine("greyOut");
    }

    IEnumerator greyOut()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Color c = GameObject.Find("GreyOut").GetComponent<Image>().color;
        for (float alpha = 0.0f; alpha <= 0.5f; alpha += 0.1f)
        {
            c.a = alpha;
            GameObject.Find("GreyOut").GetComponent<Image>().color = c;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        gameOverPanel.SetActive(true);
    }
}
