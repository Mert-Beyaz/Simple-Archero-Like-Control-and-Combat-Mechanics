using UnityEngine;

public class ArrowMultiplicationSkill : ISkill
{
    public bool IsActive { get; private set; } = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    private int arrowCount = 2;

    public void Apply(Projectile projectile, bool rageMode = false)
    {
        if (rageMode) arrowCount = 4;

        for (int i = 1; i < arrowCount; i++)
        {
            float spreadAngle = 10f * i;
            Quaternion rotation = Quaternion.Euler(0, spreadAngle, 0);
            var clone = Pool.Instance.GetObject(PoolType.Projectile);
            clone.transform.position = projectile.transform.position;
            clone.transform.rotation = rotation * projectile.transform.rotation;
            var projClone = clone.GetComponent<Projectile>();
            //projClone.Initialize(rotation * projectile.GetInitialVelocity());
        }
    }
}
