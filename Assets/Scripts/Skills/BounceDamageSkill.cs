public class BounceDamageSkill : ISkill
{
    public bool IsActive { get; private set; } = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    private int _bounceCount = 1;

    public void Apply(Projectile projectile, bool rageMode = false)
    {
        int count = rageMode ? _bounceCount * 2 : _bounceCount;
        projectile.EnableBounce(count);
    }
}
