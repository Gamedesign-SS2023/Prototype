using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform playerTf;
    //map size
    int x,y;
    public void Init(Transform target, int x, int y)
    {
        playerTf = target;
        this.x = x;
        this.y = y;
    }

    void LateUpdate()
    {
        float x = Mathf.Lerp(transform.position.x, playerTf.position.x, 0.2f);
        float y = Mathf.Lerp(transform.position.y, playerTf.position.y, 0.2f);
        //screen position
        Vector2 pxV2 = Camera.main.WorldToScreenPoint(new Vector2(0,0));
        //distance from start postion to screen position
        Vector2 screenV2 = new Vector2(Screen.width + pxV2.x, Screen.height + pxV2.y);
        //screen position to world position
        Vector2 wpScreenV2 = Camera.main.ScreenToWorldPoint(screenV2);

        Vector3 cameraPos = new Vector3(0,0, transform.position.z);

        //map weight
        if(wpScreenV2.x > this.x - 1)
        {
            cameraPos.x = (this.x -1 )/ 2f;
        }else{
            float minX = wpScreenV2.x / 2f;
            float maxX = this.x - 1 - minX;
            cameraPos.x = Mathf.Clamp(x, minX,maxX);
        }

        //map height
        if(wpScreenV2.y > this.y -1)
        {
            cameraPos.y = (this.y - 1) /2f;
        }else
        {
            float minY = wpScreenV2.y / 2f;
            float maxY = this.y - 1 - minY;
            //cameraPos.y = y;
            cameraPos.y = Mathf.Clamp(y, minY, maxY);
        }
        
        transform.position = cameraPos;
    }


}
