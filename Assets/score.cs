using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    [Header("Statistics")]
    public TextMeshProUGUI statistics;
    public int kills = 0;


    public void Update()
    {
        statistics.text = ""+kills;
    }
}
