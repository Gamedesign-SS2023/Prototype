using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public void GameOver()
    {
        Debug.Log("Game Over");
        GetComponent<Player>().enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

    }
}
