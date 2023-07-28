using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public AudioSource gameOverAudio;
    public AudioSource victoryAudio;
    public TextMeshProUGUI title;

    [Header("Statistics")]
    public TextMeshProUGUI statistics;
    public float highScore = 0;
    public int friends = 0;
    public int foes = 0;

    public void GameOverPanel(bool death)
    {
        AudioSource popUpSound = death ? gameOverAudio : victoryAudio;
        popUpSound.Play();

        title.text = death ? "Game Over" : "Glückwunsch!";
        statistics.text = "High Score: " + highScore + " | Freunde: " + friends + " | Futter: " + foes;

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
