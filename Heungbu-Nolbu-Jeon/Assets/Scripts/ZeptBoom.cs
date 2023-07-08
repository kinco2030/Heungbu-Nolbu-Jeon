using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeptBoom : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestoryZetpBoom", 1.0f);
    }

    private void DestoryZetpBoom()
    {
        Destroy(gameObject);
    }
}
