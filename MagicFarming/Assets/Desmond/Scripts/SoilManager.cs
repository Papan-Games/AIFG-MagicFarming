using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoilManager : MonoBehaviour
{
    public enum Seeds {Peach, GlowingMushroom, Rafflesia};

    [HeaderAttribute("Stats")]
    public float health = 100;
    private float maxHealth = 100;
    private bool isDead;
    private float lowQualityHealth = 50;

    [HeaderAttribute("Components")]
    public GameObject seedMenu;
    public GameObject healMenu;
    public bool isPlanting;
    public CountdownTimer timer;
    public Seeds growingSeed;
    public GameObject healthBar;
    public Image healthFill;
    public ParticleSystem healEffect;

    [HeaderAttribute("Peach Variables")]
    public float p_growTime;
    public GameObject peachOBJ;

    [HeaderAttribute("Glowing Mushroom Variables")]
    public float m_growTime;
    public GameObject mushroomOBJ;
    
    [HeaderAttribute("Rafflesia Variables")]
    public float r_growTime;
    public GameObject rafflesiaOBJ;

    private bool harvestState;
    private bool returnHQ;

    // Start is called before the first frame update
    void Start()
    {
        harvestState = false;
        isDead = false;
        isPlanting = false;
        HideSeedMenu();
        HideHealthBar();
        HideHealMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(seedMenu.activeSelf)
        {
            if(!GameManager.instance.CheckDistance(transform, GameManager.instance.satisfiedPlantRange))
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

    public void TakeDamage(float damageAmt)
    {
        if(health > 0)
        {
            health -= damageAmt;
            UpdateHealthBar();
            if(health <= 0)
            {
                isDead = true;
                Death();
            }
        }
    }


    private void Death()
    {
        HideHealthBar();
        timer.EndTimer();
        isPlanting = false;
        harvestState = false;
        HideFruit();
    }

    public void ShowHealMenu()
    {
        if(isPlanting)
        {
            healMenu.SetActive(true);
            // check whether dust > cost
            // make button uninteractable
        }
    }
    
    public void HideHealMenu()
    {
        healMenu.SetActive(false);
    }

    public void SpeedUpGrowth()
    {
        timer.timeRemaining -= GameManager.instance.speedUpAmt;
        GameManager.instance.SpeedUpDustCost();
    }

    public void Heal()
    {
        if(health < maxHealth)
        {
            GameManager.instance.HealDustCost();
            health = maxHealth;
            UpdateHealthBar();
            //Play heal particle
            //healEffect.Play();
        }
        HideHealMenu();
    }

    public void UpdateHealthBar()
    {
        healthFill.fillAmount = health / maxHealth;
    }

    void ShowHealthBar()
    {
        healthBar.SetActive(true);
        health = maxHealth;
        UpdateHealthBar();
    }

    void HideHealthBar()
    {
        healthBar.SetActive(false);
    }

    
    public Seeds HarvestPlant()
    {
        isPlanting = false;
        harvestState = false;
        QualityControl();
        HideHealthBar();
        HideFruit();
        return growingSeed;
    }

    void QualityControl()
    {
        if(health > lowQualityHealth)
        {
            returnHQ = true;
        }
        else
        {
            returnHQ = false;
        }
    }

    public bool GetHarvestQuality()
    {
        return returnHQ;
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
            ShowHealthBar();
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
            ShowHealthBar();
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
            ShowHealthBar();
        }
    }

    public void ChangeToHarvestState()
    {
        if(isPlanting)
        {
            harvestState = true;
            HideHealMenu();
            GameManager.instance.landTarget = null;
            if(growingSeed == Seeds.Peach)
            {
                peachOBJ.SetActive(true);
            }
            else if (growingSeed == Seeds.GlowingMushroom)
            {
                mushroomOBJ.SetActive(true);
            }
            else
            {
                rafflesiaOBJ.SetActive(true);
            }
        }
    }

    void HideFruit()
    {
        rafflesiaOBJ.SetActive(false);
        mushroomOBJ.SetActive(false);
        peachOBJ.SetActive(false);
    }

    public bool GetHarvestState()
    {
        return harvestState;
    }

    public bool GetIsDead()
    {
        return isDead;
    }


}
