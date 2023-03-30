using System;
using System.Collections;
using Assets._Main.Scripts.Sounds;
using Assets._Main.Thecnical.Scripts.Interactables;
using TMPro;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.PickUps
{

    public class Pickeable : Interactable,ISound
    {
        [SerializeField] protected PickupStats stats;
        private Coroutine disapearRoutine;
        [SerializeField] private CanvasFiller timer;
        private float currCooldown;
        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            if (disapearRoutine == null && stats.canDisappear)
            {
                currCooldown = stats.lifeTime;
                disapearRoutine = StartCoroutine(Disappear());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (GameUtilities.IsGoInLayerMask(other.gameObject, stats.contactLayer))
            {
                if (!stats.magnetize)
                {
                    return;
                }
                var distance = (transform.position - other.transform.position);
                transform.position = Vector3.Lerp(transform.position,other.transform.position, stats.magnetizeSpeed * Time.deltaTime);
                if (distance.magnitude <= stats.pickUpRadius)
                {
                    ActionsOnPickUp();
                }
            }
        }

        private void Update()
        {
            if (!stats.canDisappear)
            {
                return;
            }
            currCooldown -= Time.deltaTime;
            if (timer == null)
            {
                return;
            }
            timer.UpdateCanvas(currCooldown,stats.lifeTime);
        }

        private IEnumerator Disappear()
        {
            DisappearAnimation();
            yield return new WaitUntil(() => currCooldown <= stats.lifeTime / 2);
            ActionBeforeDisappear();
            yield return new WaitUntil(() => currCooldown <= 0);
            disapearRoutine = null;
            DisappearAction();
        }

        protected virtual void ActionBeforeDisappear()
        {

        }

        protected virtual void DisappearAction()
        {
        }

        protected virtual void ActionsOnPickUp()
        {
            actionToDo?.Invoke();
            DisappearAction();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,stats.pickUpRadius);
        }

        public override void OnInteract(MonoBehaviour model)
        {
            if (!stats.magnetize)
            {
                ActionsOnPickUp();
            }
        }

        private void DisappearAnimation()
        {
        }

        [field:SerializeField]public AudioClip Sound { get; set; }
    }
}