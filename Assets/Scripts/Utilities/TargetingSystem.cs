using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Transform GetNearestTarget(Vector3 fromPosition)
    {
        List<GameObject> enemies = new List<GameObject>();
        Queue<GameObject> enemiesTemp = new Queue<GameObject>();

        enemies = Pool.Instance.GetAllEnemys();

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
