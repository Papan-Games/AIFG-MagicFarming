 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoundSettingManager : MonoBehaviour
{
    public Slider volumeSlider;
    public static bool isSoundOn;

    string newVolume = "VOLUME_SLIDER";


    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        string soundOn = PlayerPrefs.GetString("Sound On", "true");
        volumeSlider.value = PlayerPrefs.GetFloat(newVolume, 1);

        // Get boolean using PlayerPrefs
        isSoundOn = PlayerPrefs.GetInt("isMute") == 1 ? true : false;
        //muteUnmuteToggle.isOn = !isSoundOn;
        if (isSoundOn == false)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetString("Sound On", "true");

            //Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("isMute", isSoundOn ? 1 : 0);
            isSoundOn = true;
        }

        else
        {
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetString("Sound On", "false");
            PlayerPrefs.SetInt("isMute", isSoundOn ? 1 : 0);
            isSoundOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSoundOn)
        {
            volumeSlider.interactable = false;
        }
        else
        {
            volumeSlider.interactable = true;
        }
    }

    //Adjust the game volume
    public void AdjustVolume(float _volume)
    {
        AudioListener.volume = _volume;
        PlayerPrefs.SetFloat("VOLUME_SLIDER", _volume);
    }



    //Sound On or unmute the game
    //public void OnClickMuteUnmute()
    //{
    //    if (!isSoundOn)
    //    {
    //        AudioListener.volume = 0; ;
    //        MuteUnmuteText.text = "Sound Off";
    //        PlayerPrefs.SetString("Sound On", "true");
    //        PlayerPrefs.SetInt("isMute", isSoundOn ? 1 : 0);
    //        muteUnmuteToggle.isOn = false;
    //        isSoundOn = true;
    //    }

    //    else
    //    {
    //        AudioListener.volume = volumeSlider.value;
    //        MuteUnmuteText.text = "Sound On";
    //        PlayerPrefs.SetString("Sound On", "false");
    //        PlayerPrefs.SetInt("isMute", isSoundOn ? 1 : 0);
    //        isSoundOn = false;
    //        muteUnmuteToggle.isOn = true;
    //    }
    //}
}
