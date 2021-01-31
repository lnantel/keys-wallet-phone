using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<SimplePlayerController> playerLoaded;

    public SimplePlayerController CurrentPlayer { get; private set; }

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AssignPlayer;
        SceneManager.sceneLoaded += AssignCursor;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AssignPlayer;
        SceneManager.sceneLoaded -= AssignCursor;
    }

    private void AssignPlayer(Scene scene, LoadSceneMode loadSceneMode)
    {
        CurrentPlayer = FindObjectOfType<SimplePlayerController>();

        if (CurrentPlayer != null)
        {
            if (playerLoaded != null)
                playerLoaded(CurrentPlayer);
        }
    }

    private void AssignCursor(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Main")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
