using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    public RawImage soundIcon;

    void Update()
    {
        SetSoundState();
    }
    public void SoundToggle()
    {
        if(PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlayerPrefs.SetInt("sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        SetSoundState();
    }
    private void SetSoundState()
    {
        if(PlayerPrefs.GetInt("sound", 1) == 1)
        {
            AudioListener.volume = 1.0f;
            soundIcon.color = Color.white;
        }
        else
        {
            AudioListener.volume = 0;
            soundIcon.color = Color.red;
        }
    }
}

