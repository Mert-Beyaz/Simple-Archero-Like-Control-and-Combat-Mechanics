public interface ISkill
{
    bool IsActive { get; }
    void Activate();
    void Deactivate();
    void Apply(Projectile projectile, bool rageMode = false);
}
