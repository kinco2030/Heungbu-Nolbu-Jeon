using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zet : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("����!");

        Invoke("DestoryZet", 5.0f);
    }

    private void DestoryZet()
    {
        Destroy(gameObject);
    }
}
