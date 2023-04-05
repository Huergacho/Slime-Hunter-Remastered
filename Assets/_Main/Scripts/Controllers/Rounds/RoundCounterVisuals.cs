using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class RoundCounterVisuals : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI roundText;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SuscribeEvents(RoundCounterController controller)
        {
            controller.OnUpdateRound += UpdateRoundText;
            controller.IsWaiting += Wait;
        }

        private void Wait(bool isWaiting)
        {
            StartCoroutine(WaitVisuals(isWaiting));
        }

        private void UpdateRoundText(int content)
        {
            _animator.Play("New Round");
            roundText.text = content.ToString();
        }
        private IEnumerator WaitVisuals(bool isWaiting)
        {
            roundText.color = Color.white;
            yield return new WaitUntil(() => !isWaiting);
            roundText.color = Color.red;

        }
    }
}