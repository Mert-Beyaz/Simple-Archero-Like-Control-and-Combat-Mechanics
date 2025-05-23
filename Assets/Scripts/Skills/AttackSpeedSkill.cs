public class AttackSpeedSkill : ISkill
{
    public bool IsActive { get; private set; } = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void Apply(Projectile projectile, bool rageMode = false)
    {
        
    }
}