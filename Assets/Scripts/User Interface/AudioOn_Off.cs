using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOn_Off : MonoBehaviour
{
    public void Mute(bool mute)
    {
        if (mute) {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
