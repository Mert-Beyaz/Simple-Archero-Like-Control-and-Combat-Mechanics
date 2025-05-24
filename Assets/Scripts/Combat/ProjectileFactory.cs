using UnityEngine;

public static class ProjectileFactory
{
    public static void Create(Vector3 start, Vector3 target, SkillManager skillManager)
    {
        GameObject obj = Pool.Instance.GetObject(PoolType.Projectile);
        Projectile proj = obj.GetComponent<Projectile>();
        obj.transform.position = start;

        float flightTime = 0.5f;

        if (!CalculateParabolicVelocity(start, target, flightTime, out Vector3 velocity))
        {
            Debug.LogWarning("Velocity hesaplanamadı");
            return;
        }

        proj.Initialize(velocity);
        skillManager.ApplySkills(proj);
    }

    private static bool CalculateParabolicVelocity(Vector3 start, Vector3 end, float time, out Vector3 velocity)
    {
        velocity = Vector3.zero;

        Vector3 displacement = end - start;
        Vector3 horizontal = new Vector3(displacement.x, 0, displacement.z);
        float horizontalDistance = horizontal.magnitude;
        float vertical = displacement.y;

        Vector3 direction = horizontal.normalized;
        float g = -Physics.gravity.y;

        float vxz = horizontalDistance / time;
        float vy = (vertical + 0.5f * g * time * time) / time;

        velocity = direction * vxz;
        velocity.y = vy;

        return true;
    }
}

