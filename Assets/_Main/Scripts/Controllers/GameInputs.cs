using UnityEngine;

namespace _Main.Scripts.Controllers
{
    public class GameInputs : MonoBehaviour
    {
        private MenuesInputs _inputs;
            public bool IsPaused { get; private set; }
            private void Awake()
            {
                _inputs = new MenuesInputs();
                _inputs.Game.Pause.performed += ctx => PauseGame();
            }

            void PauseGame()
            {
                GameManager.Instance.PauseGame(true);
            }
            private void OnEnable()
            {
                _inputs.Enable();
            }

            private void OnDisable()
            {
                _inputs.Disable();
            }
    }
}