using System;
using static UnityEditor.Progress;

public class ButtonController : BaseSkillButton
{
    public override void OnClick()
    {
        base.OnClick();
        Type _skill = typeof(AttackSpeedSkill);

        switch (buttonType)
        {
            case SkillButtonType.AttackSpeed:
                _skill = typeof(AttackSpeedSkill);
                break;
            case SkillButtonType.Bounce:
                _skill = typeof(BounceDamageSkill);
                break;
            case SkillButtonType.Burn:
                _skill = typeof(BurnDamageSkill);
                break;
            case SkillButtonType.Multiplication:
                _skill = typeof(ArrowMultiplicationSkill);
                break;
            case SkillButtonType.Rage:
                _skill = typeof(RageModeSkill);
                break;
        }

        EventBroker.Publish("OnChangeSkill", (_skill, IsActive));
    }
}
