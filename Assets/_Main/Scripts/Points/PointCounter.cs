using System;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class PointCounter : MonoBehaviour, IUI
    {
        private PointVisual _pointVisual;
        private int _currentPoints;
        public Action<int> OnUpdatePoints;

        private void Awake()
        {
            _pointVisual = GetComponent<PointVisual>();
        }
        private void Start()
        {
            GameManager.Instance.PointCounter = this;
            _pointVisual?.SuscribeEvents(this);
        }
        public void AddPoints(int quantity)
        {
            _currentPoints += quantity;
            UpdateInfo();
        }

        public void DeletePoints(int quantity)
        {
            if (_currentPoints < quantity) { return;}
            if((_currentPoints -= quantity)>= 0) {return;}
            _currentPoints -= quantity;
            UpdateInfo();
        }
        public void UpdateInfo()
        {
            OnUpdatePoints?.Invoke(_currentPoints);
        }
    }
}