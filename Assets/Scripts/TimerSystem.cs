using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : StaticInstance<TimerSystem>
{
    public float elapsedTime = 0;
    public bool timerOn=true;


    void Update()
    {
        if (!timerOn) return;
        elapsedTime += Time.deltaTime;
    }

    private void OnEnable()
    {
        GameManager.OnGameCompleted += StopTimer;
    }


    private void OnDisable()
    {
        GameManager.OnGameCompleted -= StopTimer;
    }

    public void StopTimer() => timerOn = false;

    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000) / 10;
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public string FormattedTime
    {
        get { return FormatTime(elapsedTime); }
    }
    
    
    
}