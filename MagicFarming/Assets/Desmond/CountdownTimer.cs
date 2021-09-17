using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Image timerUI;


    [HeaderAttribute("Display Only")]
    public float timeRemaining;
    public bool timeIsRunning = false;

    private float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        timerUI.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timeRemaining > 0.0f)
            {
                timeRemaining -= Time.deltaTime;
                timerUI.fillAmount = timeRemaining / maxTime;
            }
            else
            {
                timeRemaining = 0.0f;
                timeIsRunning = false;
                timerUI.fillAmount = 0.0f;
            }
        }
    }

    public void StartTimer(float timeInSeconds)
    {
        maxTime = timeInSeconds;
        timeRemaining = maxTime;
        timeIsRunning = true;
        timerUI.fillAmount = timeRemaining / maxTime;
    }

    [ContextMenu("RestartTimer")]
    public void RestartTimer()
    {
        StartTimer(10.0f);
    }

}
