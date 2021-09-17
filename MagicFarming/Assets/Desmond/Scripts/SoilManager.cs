using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoilManager : MonoBehaviour
{
    public GameObject seedMenu;
    public bool isPlanting;

    // Start is called before the first frame update
    void Start()
    {
        HideSeedMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(seedMenu.activeSelf)
        {
            if(!GameManager.instance.CheckDistance(transform))
            {
                HideSeedMenu();
            }
        }
    }

    public void ShowSeedMenu()
    {
        seedMenu.SetActive(true);
    }

    public void HideSeedMenu()
    {
        seedMenu.SetActive(false);
    }

}
