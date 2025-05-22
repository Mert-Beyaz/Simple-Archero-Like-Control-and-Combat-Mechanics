public class BounceDamageSkill : ISkill
{
    private int bounceCount = 1;

    public BounceDamageSkill(bool isRageMode = false)
    {
        if (isRageMode) bounceCount = 2;
    }

    public void Apply(Projectile projectile)
    {
        //projectile.EnableBounce(bounceCount);
    }
}
