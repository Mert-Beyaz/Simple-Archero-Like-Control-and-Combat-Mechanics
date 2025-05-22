using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<ISkill> activeSkills = new List<ISkill>();
    public bool rageMode = false;

    public void ActivateSkill(ISkill skill)
    {
        if (!activeSkills.Contains(skill))
        {
            activeSkills.Add(skill);
        }
    }

    public void DeactivateSkill(ISkill skill)
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
                if (skill is ArrowMultiplicationSkill) new ArrowMultiplicationSkill(true).Apply(projectile);
                if (skill is BounceDamageSkill) new BounceDamageSkill(true).Apply(projectile);
                if (skill is BurnDamageSkill) new BurnDamageSkill(true).Apply(projectile);
            }
        }
    }

    public float GetAttackSpeedMultiplier()
    {
        bool attackSpeedActive = activeSkills.Exists(s => s is AttackSpeedSkill);
        return rageMode && attackSpeedActive ? 4f : attackSpeedActive ? 2f : 1f;
    }
}
