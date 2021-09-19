using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance {get; private set;}

    public GameObject foxPrefab;
    public GameObject monkeyPrefab;
    public GameObject butterflyPrefab;
    public float phase1Interval;
    public float phase2Interval;
    public float phase3Interval;
    public float butterflyInterval;

    public List<Transform> targetList;

    public List<Transform> spawnPoints;
    public List<Transform> butterflySpawnPoints;

    private bool phase1;
    private float timer;
    private float timeRemaining;
    private float butterflyTimeRemaining;

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
        butterflyTimeRemaining = butterflyInterval;
        phase1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnButterfly();
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
        int randomEnemy = Random.Range(0,2);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        GameObject temp;

        if(PetManager.instance.CheckNumberOfEnemies())
        {
            if(randomEnemy == 0) // spawn fox
            {
                temp = Instantiate(foxPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            }
            else // spawn monkey
            {
                temp = Instantiate(monkeyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            }
            PetManager.instance.AddToTargetList(temp.transform.GetChild(0).transform);
        }
    }

    void SpawnButterfly()
    {
        if(butterflyTimeRemaining > 0.0f)
        {
            butterflyTimeRemaining -= Time.deltaTime;
        }
        else
        {
            if(PetManager.instance.butterflyList.Count < 2)
            {
                GameObject temp;

                temp = Instantiate( 
                    butterflyPrefab,
                    butterflySpawnPoints[Random.Range(0, butterflySpawnPoints.Count)].position, 
                    Quaternion.identity
                    );

                PetManager.instance.AddToButterflyList(temp.transform);
                butterflyTimeRemaining = butterflyInterval;
            }
        }
    }
}
