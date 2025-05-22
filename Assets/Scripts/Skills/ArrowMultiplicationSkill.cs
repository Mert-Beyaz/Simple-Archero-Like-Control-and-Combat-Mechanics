using UnityEngine;

public class ArrowMultiplicationSkill : ISkill
{
    private int arrowCount = 2;

    public ArrowMultiplicationSkill(bool isRageMode = false)
    {
        if (isRageMode) arrowCount = 4;
    }

    public void Apply(Projectile projectile)
    {
        for (int i = 1; i < arrowCount; i++)
        {
            float spreadAngle = 10f * i;
            Quaternion rotation = Quaternion.Euler(0, spreadAngle, 0);
            Projectile clone = GameObject.Instantiate(projectile, projectile.transform.position, rotation * projectile.transform.rotation);
            //clone.Initialize(rotation * projectile.GetInitialVelocity());
        }
    }
}
