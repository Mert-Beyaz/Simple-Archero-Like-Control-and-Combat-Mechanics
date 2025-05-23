using UnityEngine;

public static class ProjectileFactory
{
    public static void Create(Vector3 start, Vector3 target, SkillManager skillManager) //burayı düzelt
    {
        var obj = Pool.Instance.GetObject(PoolType.Projectile);
        var proj = obj.GetComponent<Projectile>();
        obj.transform.position = start;

        //Vector3 dir = (target - start);
        //float distance = dir.magnitude;
        //float angle = 45f * Mathf.Deg2Rad;
        //float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
        //Vector3 vel = dir.normalized * velocity;

        Vector3 dir = target - start;
        Vector3 flatDir = new Vector3(dir.x, 0, dir.z);
        Vector3 upDir = Vector3.up * Mathf.Tan(45f * Mathf.Deg2Rad) * flatDir.magnitude;
        Vector3 finalDir = (flatDir + upDir).normalized;

        proj.Initialize(finalDir * proj.speed);

        skillManager.ApplySkills(proj);
    }
}
