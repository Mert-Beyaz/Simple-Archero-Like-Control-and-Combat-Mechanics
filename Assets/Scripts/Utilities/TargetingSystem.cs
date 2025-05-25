using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public static TargetingSystem Instance;
    void Awake()
    {
        Instance = this;
    }

    public Transform GetNearestTarget(Vector3 fromPosition, bool isSecondNearestEnemy = false, GameObject exclude = null)
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies = Pool.Instance.GetAllEnemys();

        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var enemy in enemies)
        {
            if (isSecondNearestEnemy && exclude != null && enemy == exclude) continue;
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
