using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public TMP_Text text;
    //float alpha = 1;

    private void FixedUpdate()
    {
        //ChangeAlpha();
    }

    public void Init(string damage,int type)
    {
        /*
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
        */
        StartCoroutine(fade(damage,type));
        //text.text = damage;
        //alpha = 1;
    }

    /*
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
    */

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
