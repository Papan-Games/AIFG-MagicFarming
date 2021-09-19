using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PerformanceGrade : MonoBehaviour
{
    public TimeManager timeManager;
    public TextMeshProUGUI timer;
    public Image grade;
    public Sprite[] gradeSprite;
    public TextMeshProUGUI commentText;
    public string[] comment;
    public float delayLoadScene = 7;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = timeManager.timeElapsed;
        timer.text = timeManager.timerText.text;

        if (time < 180)
        {
            // Grade A
            grade.sprite = gradeSprite[0];
            commentText.text = comment[0];
        }
        else if (time >= 180 && time < 300)
        {
            // Grade B
            grade.sprite = gradeSprite[1];
            commentText.text = comment[1];
        }
        else if (time >= 300 && time < 420)
        {
            // Grade C
            grade.sprite = gradeSprite[2];
            commentText.text = comment[2];
        }
        else
        {
            // Fail
            grade.sprite = gradeSprite[3];
            commentText.text = comment[3];
        }

        StartCoroutine(LoadCredit());
    }

    IEnumerator LoadCredit()
    {
        yield return new WaitForSecondsRealtime(delayLoadScene);
        Time.timeScale = 1;
        SceneManager.LoadScene("CreditPage");
    }
}
