using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Skills
{
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private List<Skill> skills = new List<Skill>();
        [SerializeField] private MonoBehaviour owner;

        private void Start()
        {
            foreach (var skill in skills)
            {
                skill.Initialize(owner);
            }
        }

        public void RunSkill(int skillIndex)
        {
            skills[skillIndex].ActivateSkill();
        }
    }
}