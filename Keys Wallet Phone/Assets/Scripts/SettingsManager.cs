using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public float MainVolume { get; private set; }
    public float MouseSensitivity { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        MainVolume = 0.5f;
        MouseSensitivity = 5f;
    }

    public void AssignNewVolume(float vol)
    {
        MainVolume = vol;
    }

    public void AssignNewMouseSensitivity(float sens)
    {
        Debug.Log("new sens : " + MouseSensitivity);
        MouseSensitivity = sens;
    }
}
