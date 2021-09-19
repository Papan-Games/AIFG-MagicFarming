using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollection : MonoBehaviour
{
    public List<ParticleSystem> particleList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAll()
    {
        for(int i = 0; i < particleList.Count; i++)
        {
            particleList[i].Play();
        }
    }
}
