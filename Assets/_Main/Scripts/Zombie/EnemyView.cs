
using System;
using _Main.Scripts.Gun;
using Assets._Main.Scripts.Generic_Pool;
using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Animator _animator;
    private Weapon _currWeapon;
    [SerializeField] private ParticleSystem respawnParticles;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void AssignProperties(EnemyModel model)
    {
        model.OnAttack += Attack;
        model.OnTakeDamage += TakeDamageAnimation;
    }

    private void Attack(Weapon curr)
    {
        if (curr == null)
        {
            return;
        }

        if (_currWeapon != curr)
        {
            _currWeapon = curr;
        }

        _animator.Play("Z_attack_A");
        ModifyAnimatorSpeed(_currWeapon.Stats.AttackRate * 10);
    }

    public void ModifyAnimatorSpeed(float speed)
    {
        _animator.speed = speed;
    }

    private void TakeDamageAnimation()
    {
        _animator.Play("Z_TakeDamage");
    }

    public void AttackAnimationEvent()
    {
        _currWeapon.Attack();
    }

    public void Walk(Vector3 a)
    {
    }

    public void OnMove(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }

    public void DieAnimationEvent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void PlayRespawnParticles()
    {
        respawnParticles.Play();
    }
    public void Die()
    {
        _animator.Play("Z_death_A");
    }

    
}