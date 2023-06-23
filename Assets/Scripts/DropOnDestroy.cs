    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject Drop;
    //[SerializeField][Range(0f, 1f)] float dropchance = 1f;

    public void OnDestroy()
    {
        //if(Random.value < dropchance)
        //{
            Transform t = Instantiate(Drop).transform;
            t.position = transform.position;
        //}
    }
}
