using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSensSlider;

    public void PlayGame()
    {
        LevelManager.instance.LoadScene("TutorialScreen", Color.black);
    }

    public void ExitGame()
    {
        Application.Quit();
        //A supprimer pour le final build
        print("Application Quit");
    }

    public void OptionsMenuOpened()
    {
        volumeSlider.value = SettingsManager.instance.MainVolume;
        mouseSensSlider.value = SettingsManager.instance.MouseSensitivity;
    }

    public void OnVolumeSliderChange(System.Single vol)
    {
        SettingsManager.instance.AssignNewVolume(vol);
    }

    public void OnMouseSensSliderChange(System.Single sens)
    {
        SettingsManager.instance.AssignNewMouseSensitivity(sens);
    }
}
