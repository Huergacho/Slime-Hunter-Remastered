using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
[RequireComponent(typeof(AudioManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Player { get; private set;}
    [field: SerializeField] public AudioManager AudioManager { get; private set; }
    public bool IsPaused { get; private set;}
    public event Action<bool> OnPaused;
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
        AudioManager = GetComponent<AudioManager>();
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