using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public TMP_Text text;

    public void Init(string damage,int type)
    {
        StartCoroutine(fade(damage,type));
    }

    IEnumerator fade(string damage, int type)
    {
        text.text = damage;

        Color c;
        switch(type)
        {
            case 1: //Pacifist
                c = Color.green;
                break;
            case 2: //Genocide
                c = Color.red;
                break;
            default: //Neutral
                c = Color.white;
                break;
        }

        c.a = text.color.a;
        gameObject.transform.localScale = new Vector3(0.5f,0.5f,1);

        //Fade In
        for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
        {
            c.a = alpha;
            text.color = c;
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + 0.05f,
                gameObject.transform.localScale.y + 0.05f,
                1);
            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(.5f);

        //Fade Out
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            c.a = alpha;
            text.color = c;
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x - 0.05f,
                gameObject.transform.localScale.y - 0.05f,
                1);
            yield return new WaitForSeconds(.02f);
        }

        Destroy(gameObject); //fix damage numbers remaining after fading out 
    }
}
