using System.Collections;
using Assets._Main.Scripts.Spawners;
using UnityEngine;

public class EntitySpawnerPerTime : EntitySpawner
{
    [SerializeField] private float spawnRate;
    private Coroutine spawnCoroutine;
    private int maxSpawnEntity;

    private void Start()
    {
    }

    void Update()
    {

    }

    public void StartSpawning(int quantity)
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObjectsPerTimeRoutine(quantity));
        }
    }

    protected virtual IEnumerator SpawnObjectsPerTimeRoutine(int quantityToSpawn)
    {
        for (int i = 0; i < quantityToSpawn; i++)
        {
            SpawnObject(transform);
            yield return new WaitForSeconds(spawnRate);
        }
        spawnCoroutine = null;
    }
}