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
    private Rigidbody _rb;
    private float _bulletSpeed;
    private int _damage;
    private LayerMask _contactLayers;
    [SerializeField]private float _lifeSpan = 3;
    private GameObject _owner;
    [SerializeField]private TrailRenderer bulletTrail;
    private ParticleSystem _bulletParticle;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _bulletParticle = GetComponent<ParticleSystem>();
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
    protected virtual void OnTriggerEnter(Collider col)
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
        _bulletParticle.Play();
        bulletTrail.emitting = false;
        // gameObject.SetActive(false);
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
         bulletTrail.emitting = true;
         bulletTrail.Clear();
        StartCoroutine(LifeSpanCounter());

    }
}