﻿using System;
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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AssignPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AssignPlayer;
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
}
