public class BounceDamageSkill : ISkill
{
    public bool IsActive { get; private set; } = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    private int bounceCount = 1;

    public void Apply(Projectile projectile, bool rageMode = false)
    {
        if (rageMode) bounceCount = 2;
        //projectile.EnableBounce(bounceCount);
    }
}
