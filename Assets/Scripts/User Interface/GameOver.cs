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
    public GameObject ending;

    [Header("Statistics")]
    public TextMeshProUGUI statistics;
    public float highScore = 0;
    public int friends = 0;
    public int foes = 0;

    [Header("Endings")]
    public Sprite genocide;
    public Sprite pacifist;
    public Sprite gameover;
    public Sprite victory;

    public void GameOverPanel(bool death)
    {
        AudioSource popUpSound = death ? gameOverAudio : victoryAudio;
        popUpSound.Play();

        title.text = death ? "Game Over" : "Glückwunsch!";
        statistics.text = "High Score: " + highScore + "\nFreunde: " + friends + "\nFutter: " + foes;
        ending.GetComponent<Image>().sprite = death ? gameover : victory;
        if (GetComponent<LevelManager>().req != 0)
        {
            ending.GetComponent<Image>().sprite = (GetComponent<LevelManager>().req == 1) ? pacifist : genocide;
        }

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
