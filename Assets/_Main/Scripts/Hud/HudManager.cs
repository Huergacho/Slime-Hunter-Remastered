using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Gun;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]

public class HudManager : MonoBehaviour
{
    public  enum Huds
    {
        Weapon,
        Points,
        Rounds
    }
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.Instance.HudManager = this;

    }
}
