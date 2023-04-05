using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class PointVisual : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private TextMeshProUGUI pointsText;

        public void SuscribeEvents(PointCounter counter)
        {
            counter.OnUpdatePoints += UpdatePointText;
        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            pointsText.text = "0";
        }
        public void UpdatePointText(int curr)
        {
            _animator?.Play("Add Points");
            pointsText.text = curr.ToString();
        }
    }
}