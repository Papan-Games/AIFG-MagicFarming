using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustAttackButton : MonoBehaviour
{
    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CheckAttackCost() && GameManager.instance.enemiesInRange.Count > 0)
        {
            attackButton.interactable = true;
        }
        else
        {
            attackButton.interactable = false;
        }
    }
}
