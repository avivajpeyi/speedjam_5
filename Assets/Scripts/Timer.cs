using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    void Update()
    {
        String t = TimerSystem.Instance.FormattedTime;
        // remove miliseconds
        t = t.Substring(0, t.Length - 1);
        //
        timerText.text = t;
        //
    }
}