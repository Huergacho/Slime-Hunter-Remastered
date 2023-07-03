using System;
using _Main.Scripts.Utilities;
using Assets._Main.Thecnical.Scripts.Interactables;
using TMPro;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Interactables.Buy_Items
{
    public class BuyItem : Interactable
    {
        [SerializeField] protected BuyItemStats _stats;
        [SerializeField] private GameObject Outline;
        [SerializeField] private TextMeshProUGUI costText;
        private const string TextTemplate = "Buy for ";
        protected bool _showOutline = true;

        private void Awake()
        {
         costText =   costText.GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            GetTextInfo();
        }

        public override void OnInteract(MonoBehaviour model)
        {
            if (!GameManager.Instance.PointCounter.CanDeletePoints(_stats.ItemValue))
            {
                GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.SFX, _stats.NoMoneySound);

                return;
            }
            else
            {
                BuyActions();
            }
        }

        private void GetTextInfo()
        {
            costText.text = TextTemplate + _stats.ItemValue;
        }

        protected virtual void BuyActions()
        {
            actionToDo?.Invoke();
            GameManager.Instance.PointCounter.DeletePoints(_stats.ItemValue);
            GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.SFX,_stats.BuySound);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GameUtilities.IsGoInLayerMask(other.gameObject,_stats.ContactLayers))
            {
                if (_showOutline)
                {
                    Outline.SetActive(true);
                }
                print(other.gameObject.layer);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (GameUtilities.IsGoInLayerMask(other.gameObject, _stats.ContactLayers))
            {
                Outline.SetActive(false);
            }
        }
    }
}


