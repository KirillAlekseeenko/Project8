using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    
    private float time;
    private float timerValue;

    public bool IsSet { get; private set; }
    public float CurrentProgress
    {
        get
        {
            return time / timerValue;    
        }
    }

    public Timer(float timerValue)
    {
        this.time = 0.0f;
        this.timerValue = timerValue;
        IsSet = false;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (IsSet)
            return;
        time += deltaTime;
        if(time > timerValue)
        {
            time = timerValue;
            IsSet = true;
        }
    }
    public void Reset()
    {
        time = 0.0f;
        IsSet = false;
    }
}
