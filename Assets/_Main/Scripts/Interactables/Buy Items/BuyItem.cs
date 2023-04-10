using System;
using _Main.Scripts.Utilities;
using Assets._Main.Thecnical.Scripts.Interactables;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Interactables.Buy_Items
{
    public class BuyItem : Interactable
    {
        [SerializeField] protected BuyItemStats _stats;
        [SerializeField] private GameObject Outline;
        protected bool _showOutline = true;
        public override void OnInteract(MonoBehaviour model)
        {
            if (!GameManager.Instance.PointCounter.CanDeletePoints(_stats.ItemValue))
            {
                return;
            }
            else
            {
                BuyActions();
            }
        }

        protected virtual void BuyActions()
        {
            actionToDo?.Invoke();
            GameManager.Instance.PointCounter.DeletePoints(_stats.ItemValue);

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


