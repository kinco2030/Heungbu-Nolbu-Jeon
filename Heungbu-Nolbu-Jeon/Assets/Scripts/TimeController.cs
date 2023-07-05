using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private void Start()
    {
        Invoke("StopTime", 17.8f);
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }
}
