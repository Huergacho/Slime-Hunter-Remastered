using System;
using Assets._Main.Scripts.Pickeables;
using Assets._Main.Thecnical.Scripts.Interactables;
using UnityEngine;
using Utilities;
public class PickUpDetector : MonoBehaviour
    {
        [SerializeField]
        private PickUpDetectorProperties _stats;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (GameUtilities.IsGoInLayerMask(col.gameObject, _stats.AutomaticPickUpLayer))
            {
                var data = col.gameObject.GetComponent<IInteractable>();
                if (data != null)
                {
                    data.OnInteract(this);
                }
            }

        }

        public void ManualPickUp()
        {
            Collider2D data =  Physics2D.OverlapCircle(transform.position, _stats.PickUpRadius,_stats.ManualPickUpLayer);
            if (data != null)
            {
                var dataPickUp = data.gameObject.GetComponent<IInteractable>();
                if (dataPickUp != null)
                {
                    dataPickUp.OnInteract(this);
                    return;
                }
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,_stats.PickUpRadius);
        }
    
}