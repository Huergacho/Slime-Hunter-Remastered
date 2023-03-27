using UnityEngine;

namespace Assets._Main.Scripts.Skills
{
    public class Dash : Skill
    {
        [SerializeField] private float dashSpeed;
        private Rigidbody _rigidbody;
        public override void Initialize(MonoBehaviour owner)
        {
            _rigidbody = owner.GetComponent<Rigidbody>();
        }

        protected override void SkillAction()
        {
            _rigidbody.velocity = _rigidbody.velocity * dashSpeed;
        }

        protected override void StopAction()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}