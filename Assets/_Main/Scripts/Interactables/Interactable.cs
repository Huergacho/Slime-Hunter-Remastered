using Assets._Main.Scripts.Sounds;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Main.Thecnical.Scripts.Interactables
{
    public class Interactable : MonoBehaviour,IInteractable
    {
        public UnityEvent actionToDo;
        protected virtual void ActionsOnInteract()
        {
            actionToDo?.Invoke();
        }

        public virtual void OnInteract(MonoBehaviour model)
        {
            ActionsOnInteract();
        }
    }
}