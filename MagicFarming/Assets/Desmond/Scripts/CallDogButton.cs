using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallDogButton : MonoBehaviour
{
    public Button callButton;
    public PetController petController;

    [HeaderAttribute("Display Only")]
    public float timeRemaining;
    public bool timeIsRunning = false;
    public float downTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timeRemaining > 0.0f)
            {
                callButton.interactable = false;
                timeRemaining -= Time.deltaTime;
                //timerUI.fillAmount = timeRemaining / maxTime;
            }
            else
            {
                timeRemaining = 0.0f;
                timeIsRunning = false;
                callButton.interactable = true;
            }
        }
    }

    [ContextMenu("StartTimer")]
    public void StartTimer()
    {
        if(!timeIsRunning)
        {
            timeRemaining = downTime;
            timeIsRunning = true;
        }
    }

    // use this one for the button
    public void CallDogOver()
    {
        StartTimer();
        petController.CallToPlayer();
    }


}
