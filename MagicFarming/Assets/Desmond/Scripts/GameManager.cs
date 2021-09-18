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


    [HeaderAttribute("Planting")]
    public GameObject landTarget;
    public float satisfiedPlantRange = 3.5f;

    [HeaderAttribute("Combat")]
    public GameObject enemyTarget;
    public float satisfiedButterflyRange = 3.0f;
    public float damage = 50.0f;

    private SoilManager s_Manager;

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
                    landTarget = null;
                }
                else if (s_Manager.GetIsDead())
                {
                    s_Manager.Heal();
                }
            }
            else
            {
                landTarget.GetComponent<SoilManager>().HideSeedMenu();
                landTarget = null;
            }
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

    void StopGame()
    {
        Debug.Log("FINISHED");
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


}
