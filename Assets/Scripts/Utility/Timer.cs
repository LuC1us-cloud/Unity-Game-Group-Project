using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public void Initialize(float timeToRun)
    {
        this.time = 0;
        this.timeToRun = timeToRun;
        isRunning = false;
        isDone = false;
    }
    public void Initialize(float timeToRun, float startTime)
    {
        this.time = startTime;
        this.timeToRun = timeToRun;
        isRunning = false;
        isDone = false;
    }
    public float time;
    public float timeToRun;
    public bool isRunning;
    public bool isDone;
    public void Run()
    {
        isRunning = true;
        isDone = false;
    }

    public void Stop()
    {
        isRunning = false;
        isDone = true;
    }

    public void Reset()
    {
        time = 0;
        isRunning = false;
        isDone = false;
    }
    public void Rerun()
    {
        time = 0;
        isRunning = true;
        isDone = false;
    }

    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            if (time >= timeToRun)
            {
                isDone = true;
                isRunning = false;
            }
        }
    }
}
