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
            var data = col.gameObject.GetComponent<IInteractable>();
            if (data == null)
            {
                return;
            }
            if (GameUtilities.IsGoInLayerMask(col.gameObject, stats.AutomaticPickUpLayer))
            {
                data.OnInteract(this);
                return;
            }


        }
        public void ManualPickUp()
        {
            Collider[] data =  Physics.OverlapSphere(transform.position, stats.PickUpRadius,stats.ManualPickUpLayer);
            if(data == null){return;}

            foreach (var item in data)
            {
                var dataPickUp = item.GetComponent<IInteractable>();
                dataPickUp?.OnInteract(this);
                break;
            }

        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,stats.PickUpRadius);
        }
    
    }}
