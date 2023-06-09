using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public TMP_Text text;
    float alpha = 1;

    private void FixedUpdate()
    {
        ChangeAlpha();
    }

    public void Init(string damage,int type)
    {
        if(type == 0)//normal
        {
            text.color = Color.white;
        }
        else if(type == 1)//Pacifist
        {
            text.color = Color.green;
        }
        else if (type == 2)//Genocide
        {
            text.color = Color.red;
        }
        text.text = damage;
        alpha = 1;
    }

    void ChangeAlpha()
    {
        if(alpha <= 0)
        {
            ObjectPool.Instance.Push(gameObject);
            return;
        }
        alpha -= 0.01f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
