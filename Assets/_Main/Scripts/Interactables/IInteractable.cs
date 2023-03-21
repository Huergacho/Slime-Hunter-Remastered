using UnityEngine;

namespace Assets._Main.Thecnical.Scripts.Interactables
{
    public interface IInteractable
    {
        protected virtual void ActionsOnInteract()
        {
        }
        public void OnInteract(MonoBehaviour model);
    }
}