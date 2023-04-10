using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class PointVisual : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI auxiliarText;

        public void SuscribeEvents(PointCounter counter)
        {
            counter.OnUpdatePoints += UpdatePointText;
            counter.OnDeletePoints += DeletePointsAnimation;
            counter.OnAddPoints += AddPointsAnimation;
        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            pointsText.text = "0";
        }

        private void DeletePointsAnimation(int quantity)
        {
            _animator?.Play("Remove Points");
            auxiliarText.text = "-" +quantity;
        }
        public void UpdatePointText(int curr)
        {
            pointsText.text = curr.ToString();
        }

        public void AddPointsAnimation(int quantity)
        {
            _animator?.Play("Add Points");
            auxiliarText.text = "+"+quantity;
        }
    }
}