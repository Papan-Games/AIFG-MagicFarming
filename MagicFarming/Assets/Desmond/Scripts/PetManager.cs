using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public static PetManager instance {get; private set;}

    public List<Transform> targetList;



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
        
    }

    public void AddToTargetList(Transform enemy)
    {
        targetList.Add(enemy);
    }

    public void RemoveFromTargetList(Transform enemy)
    {
        targetList.Remove(enemy);
    }
}
