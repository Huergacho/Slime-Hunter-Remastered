
using System;
using Assets._Main.Scripts.Generic_Pool;
using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private LifeUI _lifeUI;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void Respawn()
    {
        _lifeUI.Reset();
    }
    public void AssignProperties(EnemyModel model)
    {
        model.LifeController.OnRespawn += Respawn;
        _lifeUI.Initialize(model.LifeController);
    }
    public void Attack()
    {
        _animator.Play("Attack");
    }
    public void Walk(Vector3 a)
    {
    }

    public void TakeDamage()
    {
        _animator.Play("Take Damage");
    }

    public void OnMove(float speed)
    {
        _animator.SetFloat("Speed",speed);
    }
    public void Die()
    {
    }

    
}