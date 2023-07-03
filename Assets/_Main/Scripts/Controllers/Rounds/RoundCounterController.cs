using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class RoundCounterController : MonoBehaviour, IUI
    {
        [SerializeField] private EntitySpawnerPerTime[] enemySpawners;
        private int _currentEnemies;

        [SerializeField] private int currRounds;
        public int CurrentRound => currRounds;

        [SerializeField] private int enemySpawnQuantity;
        
        [SerializeField] private float roundRestTime;
        private Coroutine _waitTimeRoutine = null;
        private Coroutine _spawnRoutine = null;
        public  static RoundCounterController Instance;

        public Action<int> OnUpdateRound;
        public Action<bool> IsWaiting;
        [SerializeField] private RoundCounterVisuals _visuals;
        [SerializeField] private float spawnRate;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (_visuals != null)
            {
                _visuals.SuscribeEvents(this);
            }
            _waitTimeRoutine = StartCoroutine(WaitTime());

        }

        private void Update()
        {
            if (_currentEnemies == 0 && _waitTimeRoutine == null)
            {
                AddRound();
            }
        }
        

        protected virtual IEnumerator WaitTime()
        {
            IsWaiting?.Invoke(true);
            currRounds++;
            OnUpdateRound?.Invoke(currRounds);
            yield return new WaitForSeconds(roundRestTime);
            IsWaiting?.Invoke(false);
            SpawnEnemies();
            _waitTimeRoutine = null;
            StopCoroutine(WaitTime());
        }
        private void SpawnEnemies()
        {
            int enemiesToSpawn = currRounds * enemySpawnQuantity;

            if (_currentEnemies <= 0)
            {
                _spawnRoutine = StartCoroutine(SpawnEnemiesConcatenated());
                _currentEnemies = enemiesToSpawn * (enemySpawners.Length);
            }
        }

        IEnumerator SpawnEnemiesConcatenated()
        {
            int enemiesToSpawn = currRounds * enemySpawnQuantity;
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawners[i].StartSpawning(enemiesToSpawn);
                yield return new WaitForSeconds(spawnRate);
            }
            print("Listo");

            _spawnRoutine = null; 


        }

        private void AddRound()
        {
            if (_waitTimeRoutine != null)
            {
                return;
            }
            _waitTimeRoutine = StartCoroutine(WaitTime());


        }
        public void RemoveEnemy()
        {
            _currentEnemies--;
        }


        public void UpdateInfo()
        {
            
        }
    }
}