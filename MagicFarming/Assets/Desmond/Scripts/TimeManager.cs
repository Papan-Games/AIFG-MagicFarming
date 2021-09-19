using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timerText;
    public float maxTime = 420f;
    private int minutes;
    private int seconds;

    [HeaderAttribute("Display Only")]
    public float timeElapsed;
    private bool timeIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timeElapsed < maxTime)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeIsRunning = false;
                GameManager.instance.StopGame();
            }
        }
    }



    public void UpdateTimerUI()
    {
        minutes = (int)(timeElapsed / 60);
        seconds = (int)(timeElapsed % 60);
        if(seconds < 10)
        {
            timerText.text = "0" + minutes + ":" + "0" + seconds;
        }
        else
        {
            timerText.text = "0" + minutes + ":" + seconds;
        }
    }


}
