namespace Archero
{
    public class BurnDamageSkill : ISkill
    {
        public bool IsActive { get; private set; } = false;
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        private float _burnDuration = 3f;
        private float _burnDamagePerSecond = 2f;


        public void Apply(Projectile projectile, bool rageMode = false)
        {
            float duration = rageMode ? _burnDuration * 2 : _burnDuration;
            projectile.SetBurnEffect(_burnDamagePerSecond, duration);
        }
    }
}