using System;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class PointCounter : MonoBehaviour, IUI
    {
        [SerializeField] private TextMeshProUGUI pointsText;
        private Animator _animator;
        private int _currentPoints;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            pointsText.text = "0";
        }
        public void UpdatePointText()
        {
            _animator?.Play("Points");
            pointsText.text = _currentPoints.ToString();
        }

        public void UpdateInfo()
        {
            UpdatePointText();
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
        public bool CanDeletePoints(int quantity)
        {
            
            {
                DeletePoints(quantity);
                return true;
            }

            return false;
        }
    }
}