using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoilManager : MonoBehaviour
{
    public enum Seeds {Peach, GlowingMushroom, Rafflesia};

    public GameObject seedMenu;
    public bool isPlanting;
    public CountdownTimer timer;
    public Seeds growingSeed;

    [HeaderAttribute("Peach Variables")]
    public float p_growTime;

    [HeaderAttribute("Glowing Mushroom Variables")]
    public float m_growTime;
    
    [HeaderAttribute("Rafflesia Variables")]
    public float r_growTime;

    private bool harvestState;

    // Start is called before the first frame update
    void Start()
    {
        harvestState = false;
        HideSeedMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(seedMenu.activeSelf)
        {
            if(!GameManager.instance.CheckDistance(transform))
            {
                HideSeedMenu();
            }
        }
    }

    public void ShowSeedMenu()
    {
        if(!isPlanting)
        {
            seedMenu.SetActive(true);
        }
    }

    public void HideSeedMenu()
    {
        seedMenu.SetActive(false);
    }


    // FOR BUTTONS
    public void PlantPeach()
    {
        if(!isPlanting)
        {
            isPlanting = true;
            timer.StartTimer(p_growTime);
            growingSeed = Seeds.Peach;
            GameManager.instance.landTarget = null;
            HideSeedMenu();
        }
    }

    public void PlantMushroom()
    {
        if(!isPlanting)
        {
            isPlanting = true;
            timer.StartTimer(m_growTime);
            growingSeed = Seeds.GlowingMushroom;
            GameManager.instance.landTarget = null;
            HideSeedMenu();
        }
    }

    public void PlantRafflesia()
    {
        if(!isPlanting)
        {
            isPlanting = true;
            timer.StartTimer(r_growTime);
            growingSeed = Seeds.Rafflesia;
            GameManager.instance.landTarget = null;
            HideSeedMenu();
        }
    }

    public void ChangeToHarvestState()
    {
        harvestState = true;
        // Show the Grown Plant
    }

    public bool GetHarvestState()
    {
        return harvestState;
    }

    public Seeds HarvestPlant()
    {
        isPlanting = false;
        harvestState = false;
        return growingSeed;
    }

}
