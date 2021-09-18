using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public GameObject player;
    public List<SoilManager.Seeds> harvestObjective;

    [HeaderAttribute("Planting")]
    public GameObject landTarget;
    public float satisfiedPlantRange = 3.5f;

    [HeaderAttribute("Combat")]
    public GameObject enemyTarget;
    public float satisfiedEnemyRange = 1.5f;
    public float damage = 50.0f;

    private SoilManager s_Manager;
    private 

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
                    harvestObjective.Remove(s_Manager.HarvestPlant());
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
            Debug.Log(enemyTarget);
            if(CheckDistance(enemyTarget.transform, satisfiedEnemyRange))
            {
                EnemyController enemyController = enemyTarget.GetComponent<EnemyController>();
                if(enemyController != null)
                {
                    //check tag if its butterfly, if it is, clear butterfly
                    //enemyController.TakeDamage(damage);
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

}
