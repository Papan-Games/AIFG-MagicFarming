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

    [SerializeField] int PlayBttnSceneNo;
    [SerializeField] int CreditBttnSceneNo;

    // Start is called before the first frame update
    void Start()
    {
        FadePanel.SetActive(false);
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
        StartCoroutine(LoadScene(PlayBttnSceneNo));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditPage()
    {
        StartCoroutine(LoadScene(CreditBttnSceneNo));
    }

    IEnumerator LoadScene(int sceneNo)
    {
        FadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneNo);
    }
}
