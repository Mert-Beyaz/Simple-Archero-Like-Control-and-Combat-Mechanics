public class BurnDamageSkill : ISkill
{
    private float burnDuration = 3f;
    private float burnDamagePerSecond = 2f;

    public BurnDamageSkill(bool isRageMode = false)
    {
        if (isRageMode) burnDuration = 6f;
    }

    public void Apply(Projectile projectile)
    {
        //projectile.SetBurnEffect(burnDamagePerSecond, burnDuration);
    }
}
