using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class RoundCounter : MonoBehaviour, IUI
    {
        [SerializeField] private EntitySpawnerPerTime[] enemySpawners;
        [SerializeField] private TextMeshProUGUI roundText;
        private Coroutine waitRest;
        private int _currentEnemies;

        [SerializeField] private int currRounds;
        public int CurrentRound => currRounds;

        [SerializeField] private int enemySpawnQuantity;
    

        [SerializeField] private float roundRestTime;
        private Coroutine _waitTimeRoutine = null;
        public  static RoundCounter Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _waitTimeRoutine = StartCoroutine(WaitTime());

        }

        private void Update()
        {
            if (_currentEnemies == 0 && _waitTimeRoutine == null)
            {
                AddRound();
                UpdateRoundText();
            }
        }

        public void UpdateRoundText()
        {
            roundText.text = currRounds.ToString();
            if (waitRest != null)
            {
                return;
            }

            waitRest = StartCoroutine(WaitVisuals());
        }

        protected virtual IEnumerator WaitTime()
        {
            currRounds++;
            yield return new WaitForSeconds(roundRestTime);
            SpawnEnemies();
            _waitTimeRoutine = null;
            StopCoroutine(WaitTime());
        }
        private void SpawnEnemies()
        {
            int enemiesToSpawn = currRounds * enemySpawnQuantity;
            _currentEnemies = enemiesToSpawn * (enemySpawners.Length);
            foreach (var spawner in enemySpawners)
            {
                spawner.StartSpawning(enemiesToSpawn);
            }
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
        private IEnumerator WaitVisuals()
        {
            var seq = DOTween.Sequence();
            seq.SetLoops(3);
            seq.Insert(0, roundText.DOColor(Color.white, roundRestTime / 4));
            seq.Insert(1, roundText.DOColor(Color.red, roundRestTime / 4));

            yield return new WaitForSeconds(roundRestTime);
            seq.Complete();
            roundText.color = Color.red;
            waitRest = null;

        }

        public void UpdateInfo()
        {
            
        }
    }
}