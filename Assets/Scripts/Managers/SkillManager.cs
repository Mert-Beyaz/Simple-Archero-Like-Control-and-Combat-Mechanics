using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Archero
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance;
        private List<ISkill> _activeSkills = new List<ISkill>();
        private Dictionary<Type, ISkill> _skills = new();
        private bool _rageMode = false;


        public bool RageMode { get => _rageMode; set => _rageMode = value; }

        void Awake()
        {
            Instance = this;
            _skills[typeof(RageModeSkill)] = new RageModeSkill();
            _skills[typeof(AttackSpeedSkill)] = new AttackSpeedSkill();
            _skills[typeof(BurnDamageSkill)] = new BurnDamageSkill();
            _skills[typeof(BounceDamageSkill)] = new BounceDamageSkill();
            _skills[typeof(ArrowMultiplicationSkill)] = new ArrowMultiplicationSkill();
        }
        private void OnEnable()
        {
            SetSubscriptions();
        }

        private void ActivateSkill(ISkill skill)
        {
            if (!_activeSkills.Contains(skill))
            {
                _activeSkills.Add(skill);
            }
        }

        private void DeactivateSkill(ISkill skill)
        {
            if (_activeSkills.Contains(skill))
            {
                _activeSkills.Remove(skill);
            }
        }

        public void ApplySkills(Projectile projectile)
        {
            foreach (var skill in _activeSkills)
            {
                skill.Apply(projectile, _rageMode);
            }
        }

        public void ApplySkillsExcept<T>(Projectile projectile, bool rageMode = false) where T : ISkill
        {
            foreach (var skill in _activeSkills)
            {
                if (skill is T) continue;
                skill.Apply(projectile, rageMode);
            }
        }

        public float GetAttackSpeedMultiplier()
        {
            bool attackSpeedActive = _activeSkills.OfType<AttackSpeedSkill>().FirstOrDefault()?.IsActive == true;
            return _rageMode && attackSpeedActive ? 4f : attackSpeedActive ? 2f : 1f;
        }

        private void ChangeSkill((Type type, bool isActive) item)
        {
            ISkill skill = _skills[item.type];
            if (item.isActive)
            {
                skill.Activate();
                ActivateSkill(skill);
            }
            else
            {
                skill.Deactivate();
                DeactivateSkill(skill);
            }
        }

        private void SetSubscriptions()
        {
            EventBroker.Subscribe<(Type, bool)>("OnChangeSkill", ChangeSkill);
        }

        private void SetUnsubscriptions()
        {
            EventBroker.UnSubscribe<(Type, bool)>("OnChangeSkill", ChangeSkill);
        }

        private void OnDisable()
        {
            SetUnsubscriptions();
        }
    }
}