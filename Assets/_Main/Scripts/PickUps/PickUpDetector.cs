using Assets._Main.Thecnical.Scripts.Interactables;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.PickUps
{
    public class PickUpDetector : MonoBehaviour
    {
        [SerializeField] private PickUpDetectorProperties stats;

        private void OnTriggerEnter(Collider col)
        {
            if (GameUtilities.IsGoInLayerMask(col.gameObject, stats.AutomaticPickUpLayer))
            {
                var data = col.gameObject.GetComponent<IInteractable>();
                data?.OnInteract(this);
            }
        }
        public void ManualPickUp()
        {
            Collider2D data =  Physics2D.OverlapCircle(transform.position, stats.PickUpRadius,stats.ManualPickUpLayer);
            if(data == null){return;}
            
            var dataPickUp = data.gameObject.GetComponent<IInteractable>();
            dataPickUp?.OnInteract(this);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,stats.PickUpRadius);
        }
    
    }}
