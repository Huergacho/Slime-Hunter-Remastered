using System;
using MyEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public  class BaseUpgrade : MonoBehaviour
    {
        [Header("Customizable")]
        [SerializeField] protected int maxValue;
        [SerializeField] protected int minValue;
        [SerializeField, Multiline]protected string description;
        [SerializeField]protected Sprite image;

        [Header("Setted Values")]
        [SerializeField] protected Button button;
        [SerializeField]private Image reflectedImage;

        [SerializeField]private TextMeshProUGUI descriptionText;
        protected float currentValue;

        private UpgradesMenu _controller;
        
        #if UNITY_EDITOR
        [ContextMenu("Randomize Value")]
        private void RandomizeValueInEditor()
        {
            var randomNumber = MyRandom.Range(minValue, maxValue);
            currentValue = Mathf.RoundToInt(randomNumber);
        }
        #endif
        protected virtual void Start()
        {
            button.onClick.AddListener(UpgradeStat);
            SetVisuals();
        }

        protected virtual void SetVisuals()
        {
            reflectedImage.sprite = image;
            descriptionText.text = description;
        }

        public void SetController(UpgradesMenu controller)
        {
            _controller = controller;
        }
        public virtual void UpgradeStat()
        {
            print("Mejoramos");
            _controller.SelectedUpdate();

        }
        protected float RandomStat()
        {
            var randomNumber = MyRandom.Range(minValue, maxValue);
            currentValue = Mathf.RoundToInt(randomNumber);
            return currentValue;
        }
    }
}