using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance {get; private set;}

    public GameObject foxPrefab;
    public GameObject monkeyPrefab;
    public float phase1Interval;
    public float phase2Interval;
    public float phase3Interval;

    public List<Transform> targetList;

    public List<Transform> spawnPoints;

    private bool phase1;
    private float timer;
    private float timeRemaining;

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
        timeRemaining = phase1Interval;
        phase1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            if(phase1)
            {
                timeRemaining = phase1Interval;
            }
            // else if(phase2)
            // else if(phase3)
        }
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0,3);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        GameObject temp;

        if(randomEnemy == 0) // spawn fox
        {
            temp = Instantiate(foxPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            //temp.GetComponent<EnemyController>().FindTargetToPursue();
        }
        else if (randomEnemy == 1) // spawn monkey
        {
            temp = Instantiate(monkeyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            //temp.GetComponent<EnemyController>().FindTargetToPursue();
        }
        else
        {
            // CHANGE TO SPAWN BUTTERFLY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            temp = Instantiate(foxPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            //temp.GetComponent<EnemyController>().FindTargetToPursue();
        }

        PetManager.instance.AddToTargetList(temp.transform);
    }
}
