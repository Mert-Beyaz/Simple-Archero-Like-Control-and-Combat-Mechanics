public class BurnDamageSkill : ISkill
{
    public bool IsActive { get; private set; } = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    private float burnDuration = 3f;
    private float burnDamagePerSecond = 2f;


    public void Apply(Projectile projectile, bool rageMode = false)
    {
        if(rageMode) burnDuration = 6f;
        projectile.SetBurnEffect(burnDamagePerSecond, burnDuration);
    }
}
