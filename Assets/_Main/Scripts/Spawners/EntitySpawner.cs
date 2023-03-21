using System.Collections;
using System.Collections.Generic;
using Assets._Main.Scripts.Generic_Pool;
using Assets._Main.Scripts.Spawners;
using Unity.Mathematics;
using UnityEngine;

public class EntitySpawner : MonoBehaviour, ISpawner
{
    [SerializeField] public PoolObject objectToSpawn;
    
    public Transform posToSpawn;
    public GameObject SpawnObject()
    {
        return GenericPool.Instance.SpawnFromPool(objectToSpawn, posToSpawn.position, quaternion.identity);
    }

    public void OnSpawn()
    {
        SpawnObject();
    }
}
