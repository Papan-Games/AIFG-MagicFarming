using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                // if(hit.collider.gameObject.layer != LayerMask.NameToLayer("UI"))
                // {
                //     agent.SetDestination(hit.point);
                // }
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Land"))
                {
                    GameManager.instance.landTarget = hit.collider.gameObject;
                }
                else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    GameManager.instance.enemyTarget = hit.collider.gameObject;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(!other.CompareTag("Butterfly"))
            {
                GameManager.instance.enemiesInRange.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(!other.CompareTag("Butterfly"))
            {
                GameManager.instance.enemiesInRange.Remove(other.transform);
            }
        }    
    }
}
