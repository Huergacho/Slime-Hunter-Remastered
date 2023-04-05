using System;
using _Main.Scripts.Gun;
using _Main.Scripts.Hud.UI;
using Assets._Main.Scripts.Characters;
using Assets._Main.Scripts.Characters.ScriptableObjects;
using Assets._Main.Scripts.Generic_Pool;
using UnityEngine;
using Characters.Enemy;
using MyEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
[RequireComponent(typeof(EnemyController),typeof(WeaponHandler))]
public class EnemyModel : MonoBehaviour
{
    [SerializeField]private Transform attackPoint;
    [SerializeField]private EnemyView view;
    // [SerializeField] private PoolObject pointDrop;
    // [SerializeField] private Dropper dropper;
    private EnemyController _controller;
    private NavMeshAgent _agent;

    #region Events

    public event Action<Weapon> OnAttack;
    public event Action OnTakeDamage;

    #endregion
    #region Components
    private WeaponHandler _handler;
    #endregion
    private void Awake()
    {
        _handler = GetComponent<WeaponHandler>();
        _agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        view?.AssignProperties(this);
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _handler.Initialize(attackPoint);
        
    }

    public void SuscribeEvents(EnemyController controller)
    {
        _controller = controller;
        _controller.OnDie += Die;
        _controller.OnMove += Move;
        _controller.OnTakeDamage += TakeDamage;
        _controller.OnAttack += Attack;
        _controller.OnRespawn += Respawn;
    }

    private void Respawn()
    {
        _controller.OnTakeDamage += TakeDamage;
        _controller.OnDie += Die;


    }
    public void Attack()
    {
        if (_handler.CurrentWeapon == null)
        {

            return;
        }
        OnAttack?.Invoke(_handler.CurrentWeapon);
    }

    public void AttackAnimationEvent()
    {
        _handler.CurrentWeapon.Attack();

    }
    private void Die()
    {
        // var chance = Random.Range(0, 100);
        // if (chance < 50)
        // {
        //     var random = MyRandom.GetRandomWeight(_dropper.Align);
        //     GenericPool.Instance.SpawnFromPool(random, transform.position, Quaternion.identity);
        // }
        view.Die();
        _agent.speed = 0;
        _controller.OnTakeDamage -= TakeDamage;
        _controller.OnDie -= Die;

        RoundCounterController.Instance.RemoveEnemy();
    }

    private void Move(float desiredSpeed)
    {
        _agent.speed = desiredSpeed;
        if (desiredSpeed != 0)
        {
            _agent.SetDestination(_controller.Target.transform.position);
        }
        view.OnMove(desiredSpeed);
    }
    private void TakeDamage()
    {
        AddPoints();
        OnTakeDamage?.Invoke();
    }
    private void AddPoints()
    {
       // var instance = GenericPool.Instance.SpawnFromPool(pointDrop, transform.position, Quaternion.identity);
        GameManager.Instance.PointCounter.AddPoints(_controller.Stats.PointValue);
    }
}