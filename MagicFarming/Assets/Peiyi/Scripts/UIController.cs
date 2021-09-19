 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] AudioSource SoundEffect;
    [SerializeField] AudioClip mouseEnter;
    [SerializeField] AudioClip mouseClick;
    [SerializeField] GameObject FadePanel;

    [Header("For Chage Scene Use")]
    [SerializeField] int InstructionSceneNo = 1;
    [SerializeField] int CreditSceneNo = 3;
    [SerializeField] int GameSceneNo = 2;
    [SerializeField] int HomePageSceneNo = 0;

    // Start is called before the first frame update
    void Start()
    {
        //FadePanel.SetActive(false);
    }

    public void MouseEnter(Button bttn)
    {
        if (bttn.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            bttn.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
        }
        SoundEffect.PlayOneShot(mouseEnter);
    }

    public void MouseExit(Button bttn)
    {
        if (bttn.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            bttn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }

    public void MouseClick()
    {
        SoundEffect.PlayOneShot(mouseClick);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadScene(InstructionSceneNo));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditPage()
    {
        StartCoroutine(LoadScene(CreditSceneNo));
    }

    IEnumerator LoadScene(int sceneNo)
    {
        FadePanel.SetActive(true);
        //if (FadePanel.GetComponent<GraphicRaycaster>() != null)
        //{
        //    if (FadePanel.GetComponent<GraphicRaycaster>().enabled == false)
        //    {
        //        FadePanel.GetComponent<GraphicRaycaster>().enabled = true;
        //    }
        //}
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNo);
    }

    public void StartLevel()
    {
        StartCoroutine(LoadScene(GameSceneNo));
    }

    public void BackHomePage()
    {
        StartCoroutine(LoadScene(HomePageSceneNo));
    }
}
