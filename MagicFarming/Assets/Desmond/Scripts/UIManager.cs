using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    public TMP_Text peachText;
    public TMP_Text mushroomText;
    public TMP_Text rafflesiaText;
    public TMP_Text dustText;

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
        UpdateKPI();
        UpdateDust();
    }

    void UpdateKPI()
    {
        peachText.text = GameManager.instance.peaches.ToString();
        mushroomText.text = GameManager.instance.mushrooms.ToString();
        rafflesiaText.text = GameManager.instance.rafflesias.ToString();
    }

    void UpdateDust()
    {
        dustText.text = GameManager.instance.dustAmt.ToString();
    }
}
