﻿using System;
using _Main.Scripts.Controllers;
using _Main.Scripts.Hud.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
[RequireComponent(typeof(AudioController))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [field: SerializeField]public GameObject Player { get; private set;}
    [field: SerializeField] public AudioController AudioManager { get; private set; }
    public bool IsPaused { get; private set;}
    public event Action<bool> OnPaused;
    private GameInputs _inputs;
    [field: SerializeField] public PointCounter PointCounter;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        IsPaused = false;
        AudioManager = GetComponent<AudioController>();
    }
    public void SetPlayer(GameObject player)
    {
        Player = player;
    }
    public void PauseGame(bool showPauseMenue)
    {
        IsPaused = !IsPaused;

        Time.timeScale = IsPaused ? 0 : 1;
        if(showPauseMenue)
            OnPaused?.Invoke(IsPaused);
    }
    
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}