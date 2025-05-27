namespace Archero
{
    public class RageModeSkill : ISkill
    {
        public bool IsActive { get; private set; } = false;

        public void Activate()
        {
            IsActive = true;
            SkillManager.Instance.RageMode = true;
        }

        public void Deactivate()
        {
            IsActive = false;
            SkillManager.Instance.RageMode = false;
        }

        public void Apply(Projectile projectile, bool rageMode = false)
        {

        }
    }
}