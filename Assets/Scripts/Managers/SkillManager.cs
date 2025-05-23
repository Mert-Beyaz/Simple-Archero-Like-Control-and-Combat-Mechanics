using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<ISkill> activeSkills = new List<ISkill>();
    public bool rageMode = false;

    private Dictionary<Type, ISkill> skills = new();

    void Awake()
    {
        skills[typeof(AttackSpeedSkill)] = new AttackSpeedSkill();
        skills[typeof(BurnDamageSkill)] = new BurnDamageSkill();
        skills[typeof(BounceDamageSkill)] = new BounceDamageSkill();
        skills[typeof(ArrowMultiplicationSkill)] = new ArrowMultiplicationSkill();
    }
    private void OnEnable()
    {
        SetSubscriptions();
    }

    private void ActivateSkill(ISkill skill)
    {
        if (!activeSkills.Contains(skill))
        {
            activeSkills.Add(skill);
        }
    }

    private void DeactivateSkill(ISkill skill)
    {
        if (activeSkills.Contains(skill))
        {
            activeSkills.Remove(skill);
        }
    }

    public void ApplySkills(Projectile projectile)
    {
        foreach (var skill in activeSkills)
        {
            skill.Apply(projectile);
        }

        if (rageMode)
        {
            foreach (var skill in activeSkills)
            {
                //if (skill is ArrowMultiplicationSkill) new ArrowMultiplicationSkill(true).Apply(projectile);
                //if (skill is BounceDamageSkill) new BounceDamageSkill(true).Apply(projectile);
                //if (skill is BurnDamageSkill) new BurnDamageSkill(true).Apply(projectile);
            }
        }
    }

    public float GetAttackSpeedMultiplier()
    {
        bool attackSpeedActive = activeSkills.OfType<AttackSpeedSkill>().FirstOrDefault()?.IsActive == true;
        return rageMode && attackSpeedActive ? 4f : attackSpeedActive ? 2f : 1f;
    }

    private void ChangeSkill(Type type, bool isActive)
    {
        ISkill skill = skills[type];
        if (isActive) 
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
        EventBroker.OnChangeSkill += ChangeSkill;
    }

    private void SetUnsubscriptions()
    {
        EventBroker.OnChangeSkill -= ChangeSkill;
    }

    private void OnDestroy()
    {
        SetUnsubscriptions();
    }
}
