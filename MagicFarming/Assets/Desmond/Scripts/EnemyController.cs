using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [HeaderAttribute("Navigation")]
    public NavMeshAgent agent;
    public float satisfiedRange;
    public float wanderInterval = 3.0f;

    [HeaderAttribute("Stats")]
    public float health = 100;
    public float damage = 20;
    public float damageDelay = 3.0f;
    private float maxHealth = 100;

    [HeaderAttribute("Components")]
    public Image healthFill;
    public ParticleCollection dustAttackEffect;

    [HeaderAttribute("Display Only")]
    [SerializeField] private Transform target;
    private float distance;
    private bool isDead;
    private bool canAttack;

    private float timeRemaining;
    private Vector3 tempWander;


    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 0.0f;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            FindTargetToPursue();
            if(target.GetComponent<SoilManager>().isPlanting)
            {
                if(CheckDistance(target) < satisfiedRange)
                {
                    if(canAttack && target.GetComponent<SoilManager>().isPlanting)
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
                Wander();
                FindTargetToPursue();
            }
        }
        else
        {
            Wander();
            FindTargetToPursue();
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

    void Wander()
    {
        if(timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
            //timerUI.fillAmount = timeRemaining / maxTime;
        }
        else
        {
            timeRemaining = wanderInterval;
            float x = Random.Range(-5.0f, 5.0f);
            float z = Random.Range(-5.0f, 5.0f);
            tempWander = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        }
        //Transform playerT = GameManager.instance.player.transform;
        //Vector3 temp = new Vector3(playerT.position.x + x, playerT.position.y, playerT.position.z + z);
        agent.SetDestination(tempWander);
    }

    float CheckDistance(Transform checkTarget)
    {
        return Vector3.Distance(checkTarget.position, transform.position);
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

    void UpdateHealthBar()
    {
        healthFill.fillAmount = health / maxHealth;
    }

    void Attack()
    {
        //Debug.Log("Attacking");
        target.GetComponent<SoilManager>().TakeDamage(damage);
    }

    void Death()
    {
        if(isDead)
        {
            PetManager.instance.RemoveFromTargetList(transform);
            GameManager.instance.enemiesInRange.Remove(transform);
            Destroy(this.transform.parent.gameObject);
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(damageDelay);
        canAttack = true;
    }
}
