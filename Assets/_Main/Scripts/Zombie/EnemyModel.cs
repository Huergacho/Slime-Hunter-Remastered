using System;
using _Main.Scripts.Gun;
using _Main.Scripts.Hud.UI;
using Assets._Main.Scripts.Characters;
using Assets._Main.Scripts.Characters.ScriptableObjects;
using Assets._Main.Scripts.Generic_Pool;
using UnityEngine;
using Characters.Enemy;
using MyEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
[RequireComponent(typeof(EnemyView),typeof(LifeController),typeof(WeaponHandler))]
public class EnemyModel : MonoBehaviour, IPooleable
{
    [SerializeField] private EnemyStatsSO stats;
    [SerializeField]private Transform attackPoint;
    public EnemyStatsSO Stats => stats;
    private EnemyView _view;
    [SerializeField] private PoolObject pointDrop;
    [SerializeField] private Dropper dropper;
    #region Components
    private WeaponHandler _handler;
    public LifeController LifeController { get; private set;}
    #endregion
    private void Awake()
    {
        _handler = GetComponent<WeaponHandler>();
        LifeController = GetComponent<LifeController>();
        _view = GetComponent<EnemyView>();
    }

    private void Start()
    {
        _view.AssignProperties(this);
        LifeController.AssignMaxLife(stats.MaxLife);
        _handler.Initialize(attackPoint);
        
    }

    public void SuscribeEvents(EnemyController controller)
    {
        controller.OnMove += Move;
        LifeController.OnDie += Die;
        LifeController.OnModifyHealth += TakeDamage;
    }
    public void Attack()
    {
        if (_handler.CurrentWeapon == null)
        {
            return;
        }
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
        RoundCounter.Instance.RemoveEnemy();
        gameObject.SetActive(false);
    }

    private void Move(float desiredSpeed)
    {
        _view.OnMove(desiredSpeed);
    }
    public void OnObjectSpawn()
    {
        gameObject.SetActive(true);
        if (RoundCounter.Instance.CurrentRound > 1)
        {
            ModifyMaxHealth();
        }
        LifeController.Respawn();
    }

    private void TakeDamage(float currentLife, float maxLife)
    {
        AddPoints();
        _view.TakeDamage();
    }

    private void AddPoints()
    {
       // var instance = GenericPool.Instance.SpawnFromPool(pointDrop, transform.position, Quaternion.identity);
        GameManager.Instance.PointCounter.AddPoints(stats.PointValue);
    }
    private void ModifyMaxHealth()
    {
        LifeController.AssignMaxLife(LifeController.MaxLife + stats.PerRoundLifeUpgrade);   
    }
}