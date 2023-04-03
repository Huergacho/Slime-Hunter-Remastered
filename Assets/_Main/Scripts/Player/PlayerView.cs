using System;
using Assets._Main.Scripts.Characters.Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private ParticleSystem walkParticles;

    [field: SerializeField] public LifeUI Lifebar { get; private set; }
    private Animator _animator;
    private PlayerModel _model;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void AssignProperties(PlayerModel model)
    {
        _model = model;
        Lifebar?.Initialize(_model.LifeController);

    }

    public void AttackAnimation(bool value)
    {
        _animator.SetBool("IsShooting", value);
    }

    public void OnIdle()
    {
        walkParticles.Stop();
    }

    public void MoveAnimation(float value)
    {
        _animator?.SetFloat("Speed", value);

    }

    public void EquipAnimation(bool value)
    {
        _animator.SetBool("IsEquipped", value);
    }

}
