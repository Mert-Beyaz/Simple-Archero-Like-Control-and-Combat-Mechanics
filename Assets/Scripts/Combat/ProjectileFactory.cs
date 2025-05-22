using UnityEngine;

public static class ProjectileFactory
{
    public static GameObject projectilePrefab;

    public static void Initialize(GameObject prefab)
    {
        projectilePrefab = prefab;
    }

    public static void Create(Vector3 start, Vector3 target, SkillManager skillManager)
    {
        var obj = GameObject.Instantiate(projectilePrefab, start, Quaternion.identity);
        var proj = obj.GetComponent<Projectile>();

        Vector3 dir = (target - start);
        float distance = dir.magnitude;
        float angle = 45f * Mathf.Deg2Rad;
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
        Vector3 vel = dir.normalized * velocity;

        proj.Initialize(vel);
        skillManager.ApplySkills(proj);
    }
}
