using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DustCostPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum Action
    {
        ATTACK,
        HEAL,
        SPEEDUP
    }

    public Action action;
    public GameObject panel;
    public TextMeshProUGUI dustCost;

    private void Start()
    {
        panel.SetActive(false);

        switch (action)
        {
            case Action.ATTACK:
                {
                    dustCost.text = GameManager.instance.attackDustCost.ToString();
                    break;
                }
            case Action.HEAL:
                {
                    dustCost.text = GameManager.instance.healDustCost.ToString();
                    break;
                }
            case Action.SPEEDUP:
                {
                    dustCost.text = GameManager.instance.speedUpDustCost.ToString();
                    break;
                }
            default:
                {
                    dustCost.text = "0";
                    break;
                }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
