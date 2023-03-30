using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenue : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenueCanvas;

    private void Start()
    {
        GameManager.Instance.OnPaused += Show;
    }

    private void Show(bool status)
    {
        pauseMenueCanvas.SetActive(status);
    }
}
