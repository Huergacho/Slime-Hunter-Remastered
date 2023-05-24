using System;
using _Main.Scripts.Upgrades;
using MyEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public  class BaseUpgrade : MonoBehaviour
    {
        [field:SerializeField] public BaseUpgradeSo Stats { get; private set; }

        [Header("Setted Values")]
        [SerializeField] protected Button button;
        [SerializeField]private Image reflectedImage;

        [SerializeField]protected TextMeshProUGUI descriptionText;
        protected float CurrentValue;

        private UpgradesMenu _controller;
        
        #if UNITY_EDITOR
        [ContextMenu("Randomize Value")]
        private void RandomizeValueInEditor()
        {
            var randomNumber = MyRandom.Range(Stats.MinValue,Stats.MaxValue);
            CurrentValue = Mathf.RoundToInt(randomNumber);
        }
        #endif
        protected virtual void Start()
        {
            button.onClick.AddListener(UpgradeStat);
            Initialize();
        }

        void Initialize()
        {
            RandomStat();
            SetVisuals();
        }
        protected virtual void SetVisuals()
        {
            reflectedImage.sprite = Stats.Image;
            descriptionText.text = Stats.Description + " " + $"<color=yellow>{Mathf.Round(CurrentValue)}</color>";

        }

        protected void UpdateVisuals()
        {
            descriptionText.text = Stats.Description + " " + $"<color=yellow>{Mathf.Round(CurrentValue)}</color>";
        }
        public void SetController(UpgradesMenu controller)
        {
            _controller = controller;
        } 
        protected void UpgradeStat()
        {
            RandomStat();
            UpdateVisuals();
            ActionToDo();
            _controller.SelectedUpdate();

        }

        protected virtual void ActionToDo()
        {
            
        }
        private void RandomStat()
        {
            CurrentValue = MyRandom.Range(Stats.MinValue, Stats.MaxValue);
            CurrentValue = Mathf.RoundToInt(CurrentValue);
        }
    }
}