using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustButtons : MonoBehaviour
{
    //public Button attackButton;
    public Button healButton;
    public Button speedUpButton;
    public GameObject healCover;
    public GameObject speedUpCover;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(GameManager.instance.CheckAttackCost())
        // {
        //     attackButton.interactable = true;
        // }
        // else
        // {
        //     attackButton.interactable = false;
        // }

        if(GameManager.instance.CheckHealCost())
        {
            healButton.interactable = true;
            healCover.SetActive(false);
        }
        else
        {
            healButton.interactable = false;
            healCover.SetActive(true);
        }

        if(GameManager.instance.CheckSpeedUpCost())
        {
            speedUpButton.interactable = true;
            speedUpCover.SetActive(false);
        }
        else
        {
            speedUpButton.interactable = false;
            speedUpCover.SetActive(true);
        }
    }

}
