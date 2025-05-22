using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Transform GetNearestTarget(Vector3 fromPosition)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var enemy in enemies)
        {
            float dist = (enemy.transform.position - fromPosition).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }
        return nearest;
    }
}
