using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxController : MonoBehaviour
{
    [HeaderAttribute("Navigation")]
    public NavMeshAgent agent;
    public float satisfiedRange;

    [HeaderAttribute("Stats")]
    public float health = 100;
    public float damage = 20;

    [HeaderAttribute("Display Only")]
    [SerializeField] private Transform target;
    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if(CheckDistance(target) < satisfiedRange)
            {
                //Attack
                //Debug.Log("Attack");
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
    }

    [ContextMenu("FindTarget")]
    public void FindTargetToPursue()
    {
        for(int i = 0; i < EnemyManager.instance.targetList.Count; i++)
        {
            bool canTarget = EnemyManager.instance.targetList[i].GetComponent<SoilManager>().isPlanting;
            if(canTarget)
            {
                float temp = CheckDistance(EnemyManager.instance.targetList[i].transform);
                if(distance <= 0)
                {
                    target = EnemyManager.instance.targetList[i].transform;
                    distance = temp;
                }
                else if(temp <= distance)
                {
                    target = EnemyManager.instance.targetList[i].transform;
                    distance = temp;
                }
            }
        }
        distance = 0;
    }

    float CheckDistance(Transform checkTarget)
    {
        return Vector3.Distance(checkTarget.position, transform.position);
    }
}
