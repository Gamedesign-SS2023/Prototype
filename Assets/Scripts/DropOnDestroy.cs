    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject Drop;

    public void OnDestroy()
    {
        Transform t = Instantiate(Drop).transform;
        t.position = transform.position;
    }
}
