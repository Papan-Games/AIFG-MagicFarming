using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public GameObject player;
    
    [HeaderAttribute("Level Harvest Objectives")]
    public int peaches;
    public int mushrooms;
    public int rafflesias;

    [HeaderAttribute("Stats")]
    public int dustAmt = 200;

    [HeaderAttribute("Planting")]
    public GameObject landTarget;
    public float satisfiedPlantRange = 3.5f;
    public int healDustCost = 40;
    public int harvestHQDust = 100;
    public int harvestLQDust = 50;
    public int speedUpDustCost = 50;
    public float speedUpAmt = 3.0f;


    [HeaderAttribute("Combat")]
    public int attackDustCost = 20;
    public float satisfiedButterflyRange = 3.0f;
    public float damage = 50.0f;
    public int maxEnemies = 5;

    [HeaderAttribute("Grade Panel")]
    public GameObject gradePanel;
    
    [HeaderAttribute("Combat Read Only")]
    public GameObject enemyTarget;
    public List<Transform> enemiesInRange;
    private GameObject attackTarget;


    private SoilManager s_Manager;
    private float d;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null && instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlantingStuff();
        CombatStuff();
    }

    public void HealDustCost()
    {
        dustAmt -= healDustCost;
    }

    public void SpeedUpDustCost()
    {
        dustAmt -= speedUpDustCost;
    }

    [ContextMenu("Attack")]
    public void AttackWithDust()
    {
        if(enemiesInRange.Count > 0)
        {
            for(int i = 0; i < enemiesInRange.Count; i++)
            {
                float temp = Vector3.Distance(enemiesInRange[i].position, player.transform.position);
                if(d <= 0)
                {
                    attackTarget = enemiesInRange[i].gameObject;
                    d = temp;
                }
                else if(temp <= d)
                {
                    attackTarget = enemiesInRange[i].gameObject;
                    d = temp;
                }
            }
            d = 0;
            attackTarget.GetComponentInChildren<EnemyController>().dustAttackEffect.PlayAll();
            attackTarget.GetComponentInChildren<EnemyController>().TakeDamage(damage);
            // Play particle effect
            dustAmt -= attackDustCost;
        }
    }

    void PlantingStuff()
    {
        if(landTarget != null)
        {
            if(CheckDistance(landTarget.transform, satisfiedPlantRange))
            {
                s_Manager = landTarget.GetComponent<SoilManager>();
                if(!s_Manager.isPlanting)
                {
                    s_Manager.ShowSeedMenu();
                }
                else if(s_Manager.GetHarvestState())
                {
                    ClearObjectives(s_Manager.HarvestPlant());
                    RewardDust(s_Manager.GetHarvestQuality());
                    s_Manager.HideHealMenu();
                    landTarget = null;
                }
                else
                {
                    s_Manager.ShowHealMenu();
                    //s_Manager.Heal();
                }
            }
            else
            {
                landTarget.GetComponent<SoilManager>().HideSeedMenu();
                landTarget.GetComponent<SoilManager>().HideHealMenu();
                landTarget = null;
            }
        }
    }

    void RewardDust(bool highQuality)
    {
        if(highQuality)
        {
            dustAmt += harvestHQDust;
        }
        else
        {
            dustAmt += harvestLQDust;
        }
    }

    void CombatStuff()
    {
        if(enemyTarget != null)
        {
            if(CheckDistance(enemyTarget.transform, satisfiedButterflyRange))
            {
                if(enemyTarget.CompareTag("Butterfly"))
                {
                    PetManager.instance.butterflyList.Remove(enemyTarget.transform);
                    Destroy(enemyTarget);
                }
                enemyTarget = null;
            }
        }
    }

    public bool CheckDistance(Transform target, float satisfiedRange)
    {
        float dist;

        dist = Vector3.Distance(target.position, player.transform.position);
        
        if(dist <= satisfiedRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopGame()
    {
        //Debug.Log("FINISHED");
        gradePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void ClearObjectives(SoilManager.Seeds harvested)
    {
        switch (harvested)
        {
            case (SoilManager.Seeds.Peach):
            {
                if(peaches > 0)
                {
                    peaches--;
                }
                CheckObjectives();
                break;
            }

            case (SoilManager.Seeds.GlowingMushroom):
            {
                if(mushrooms > 0)
                {
                    mushrooms--;
                }
                CheckObjectives();
                break;
            }

            case (SoilManager.Seeds.Rafflesia):
            {
                if(rafflesias > 0)
                {
                    rafflesias--;
                }
                CheckObjectives();
                break;
            }
            
            default:
            {
                break;
            }
        }

    }

    void CheckObjectives()
    {
        if(peaches == 0 && mushrooms == 0 && rafflesias == 0)
        {
            StopGame();
        }
    }

    public bool CheckAttackCost()
    {
        if(dustAmt >= attackDustCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckHealCost()
    {
        if(dustAmt >= healDustCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckSpeedUpCost()
    {
        if(dustAmt >= speedUpDustCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
