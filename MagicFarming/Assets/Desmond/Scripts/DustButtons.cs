using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustButtons : MonoBehaviour
{
    public Button attackButton;
    public Button healButton;
    public Button speedUpButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CheckAttackCost())
        {
            attackButton.interactable = true;
        }
        else
        {
            attackButton.interactable = false;
        }

        if(GameManager.instance.CheckHealCost())
        {
            healButton.interactable = true;
        }
        else
        {
            healButton.interactable = false;
        }

        if(GameManager.instance.CheckSpeedUpCost())
        {
            speedUpButton.interactable = true;
        }
        else
        {
            speedUpButton.interactable = false;
        }
    }

}
