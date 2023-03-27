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
    [SerializeField]private PoolObject bulletParticle;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void InitializeStats(float bulletSpeed, int damage, float lifeSpan, LayerMask layerMask, Vector3 firePoint,
        GameObject owner)
    {
        _bulletSpeed = bulletSpeed;
        _damage = damage;
        _lifeSpan = lifeSpan;
        _contactLayers = layerMask;
        _owner = owner;
        transform.forward = firePoint;
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
        GenericPool.Instance.SpawnFromPool(bulletParticle,transform.position,transform.rotation);
        bulletTrail.emitting = false;
        gameObject.SetActive(false);
    }
    private IEnumerator LifeSpanCounter()
    {
        yield return new WaitForSeconds(_lifeSpan);
        gameObject.SetActive(false);
    }

    protected virtual void Move()
    {
        _rb.velocity = transform.forward * _bulletSpeed;
    }

    public void OnObjectSpawn()
    {
        gameObject.SetActive(true);
         bulletTrail.emitting = true;
         bulletTrail.Clear();
        StartCoroutine(LifeSpanCounter());

    }
}