using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScore;
    public AudioSource gameOverAudio;
    public AudioSource victoryAudio;

    public void GameOver(bool death)
    {
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        gameOverPanel.SetActive(true);

        AudioSource popUpSound = death ? victoryAudio : victoryAudio;
        //popUpSound.Play();
        popUpSound.PlayOneShot(GetComponent<AudioSource>().clip);

        highScore.text = death ? "Game Over" : "Glückwunsch!";
        Time.timeScale = 0f;
    }
}
