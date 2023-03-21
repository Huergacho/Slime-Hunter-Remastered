using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Assets._Main.Scripts.Generic_Pool;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
public class Bullet : MonoBehaviour,IPooleable
{
    private Rigidbody2D _rb;
    private float _bulletSpeed;
    private int _damage;
    private LayerMask _contactLayers;
    [SerializeField]private float _lifeSpan = 3;
    private GameObject _owner;
    private TrailRenderer _bulletTrail;
    [SerializeField] private PoolObject bulletParticle;
    private void Awake()
    {
        _bulletTrail = GetComponentInChildren<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeStats(float bulletSpeed, int damage, float lifeSpan, LayerMask layerMask, Vector3 firePoint,
        GameObject owner)
    {
        _bulletSpeed = bulletSpeed;
        _damage = damage;
        _lifeSpan = lifeSpan;
        _contactLayers = layerMask;
        _owner = owner;
        transform.right = firePoint;
    }

    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (GameUtilities.IsGoInLayerMask(col.gameObject, _contactLayers))
        {
            if (col.GetComponent<LifeController>() == null)
            {
                Explode();
                return;
            }
            col.GetComponent<LifeController>().TakeDamage(_damage);
            Explode();
        }
    }

    protected virtual void Explode()
    {
        GenericPool.Instance.SpawnFromPool(bulletParticle, transform.position,
            transform.rotation);
        _bulletTrail.emitting = false;
        gameObject.SetActive(false);
    }

    // private async void LifeSpanCounter()
    // {
    //     if (gameObject == null)
    //     {
    //         return;
    //     }
    //     var  a = Task.Delay(TimeSpan.FromSeconds(_lifeSpan));
    //     await a;
    //     a.Dispose();
    //     if (gameObject != null)
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }
    private IEnumerator LifeSpanCounter()
    {
        yield return new WaitForSeconds(_lifeSpan);
        gameObject.SetActive(false);
    }

    protected virtual void Move()
    {
        _rb.velocity = transform.right * _bulletSpeed;
    }

    public void OnObjectSpawn()
    {
        gameObject.SetActive(true);
        _bulletTrail.emitting = true;
        _bulletTrail.Clear();
        StartCoroutine(LifeSpanCounter());

    }
}