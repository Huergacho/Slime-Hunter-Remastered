using System;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class PointCounter : MonoBehaviour, IUI
    {
        private PointVisual _pointVisual;
        [SerializeField]private int _currentPoints;
        public Action<int> OnUpdatePoints;
        public Action<int> OnDeletePoints;
        public Action<int> OnAddPoints;

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
            OnAddPoints?.Invoke(quantity);
            UpdateInfo();
        }

        public bool CanDeletePoints(int quantity)
        {
            if (_currentPoints <= 0)
            {
                return false;
            }

            var data = _currentPoints - quantity;
            return (data) >= 0;
        }
        public void DeletePoints(int quantity)
        {
            if (!CanDeletePoints(quantity))
            {
                return;
            }
            _currentPoints -= quantity;
            OnDeletePoints?.Invoke(quantity);
            UpdateInfo();
        }
        public void UpdateInfo()
        {
            OnUpdatePoints?.Invoke(_currentPoints);
        }
    }
}