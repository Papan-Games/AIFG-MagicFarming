using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public GameObject player;
    public GameObject landTarget;
    public float satisfiedRange = 1.0f;

    public List<SoilManager.Seeds> harvestObjective;

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
        if(landTarget != null)
        {
            if(CheckDistance(landTarget.transform))
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
            }
            else
            {
                landTarget.GetComponent<SoilManager>().HideSeedMenu();
                landTarget = null;
            }
        }
    }

    public bool CheckDistance(Transform target)
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

    //clear landtarget once prompt to plant is opened
}
