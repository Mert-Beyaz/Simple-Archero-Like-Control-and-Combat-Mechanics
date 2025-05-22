using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Transform GetNearestTarget(Vector3 fromPosition)
    {
        List<GameObject> enemies = new List<GameObject>();
        Queue<GameObject> enemiesTemp = new Queue<GameObject>();

        enemiesTemp = Pool.Instance.GetAllObject(PoolType.Enemy);
        //aktif pool objelerini listeye ekle poolda

        foreach (GameObject enemy in enemiesTemp)
        {
            if (enemy.activeSelf) enemies.Add(enemy);
            Debug.LogError(enemy.activeSelf);
        }

        Debug.LogError(enemies.Count);

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
        Debug.LogError(nearest.position);
        return nearest;
    }
}
