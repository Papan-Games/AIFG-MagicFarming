using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    [HeaderAttribute("Navigation")]
    public NavMeshAgent agent;
    public float satisfiedRange;
    private Vector3 calledPosition;
    public List<Transform> patrolWaypoints;
    private int currentIndex;

    [HeaderAttribute("Stats")]
    public float health = 100;
    public float damage = 25;
    public float critDamage = 50;
    public int critRate = 30;
    public float damageDelay = 1.5f;

    [HeaderAttribute("Display Only")]
    [SerializeField] private Transform target;
    private float distance;
    private bool canAttack;
    private bool callToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        canAttack = true;
        callToPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PetManager.instance.butterflyList.Count <= 0)
        {
            if(callToPlayer)
            {
                if(CheckDistance(calledPosition) < satisfiedRange)
                {
                    callToPlayer = false;
                    FindTargetToPursue();
                }
                else
                {
                    agent.SetDestination(calledPosition);
                    return;
                }
            }

            if(target != null)
            {
                if(CheckDistance(target) < satisfiedRange)
                {
                    if(canAttack)
                    {
                        StartCoroutine("AttackCooldown");
                        Attack();
                    }
                }
                else
                {
                    agent.SetDestination(target.position);
                }
            }
            else
            {
                PetPatrol();
                FindTargetToPursue();
            }
        }
        else
        {
            agent.SetDestination(PetManager.instance.butterflyList[0].position);
        }
        
    }

    public void PetPatrol()
    {
        if(CheckDistance(patrolWaypoints[currentIndex].position) < satisfiedRange)
        {
            GoToNextWaypoint();
        }

        agent.SetDestination(patrolWaypoints[currentIndex].position);
    }

    void GoToNextWaypoint()
    {
        if(currentIndex > patrolWaypoints.Count - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }

    [ContextMenu("CallToPlayer")]
    public void CallToPlayer()
    {
        if(PetManager.instance.butterflyList.Count <= 0)
        {
            callToPlayer = true;
            calledPosition = GameManager.instance.player.transform.position;
        }
    }

    [ContextMenu("FindTarget")]
    public void FindTargetToPursue()
    {
        for(int i = 0; i < PetManager.instance.targetList.Count; i++)
        {
            bool canTarget = !PetManager.instance.targetList[i].GetComponentInChildren<EnemyController>().GetIsDead();
            if(canTarget)
            {
                float temp = CheckDistance(PetManager.instance.targetList[i].transform);
                if(distance <= 0)
                {
                    target = PetManager.instance.targetList[i].transform;
                    distance = temp;
                }
                else if(temp <= distance)
                {
                    target = PetManager.instance.targetList[i].transform;
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

    float CheckDistance(Vector3 checkPos)
    {
        return Vector3.Distance(checkPos, transform.position);
    }

    void Attack()
    {
        if(CheckCritical())
        {
            target.GetComponentInChildren<EnemyController>().TakeDamage(critDamage);
        }
        else
        {
            target.GetComponentInChildren<EnemyController>().TakeDamage(damage);
        }
    }

    bool CheckCritical()
    {
        int chance = Random.Range(0, 101);
        if(chance <= critRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(damageDelay);
        canAttack = true;
    }
}
