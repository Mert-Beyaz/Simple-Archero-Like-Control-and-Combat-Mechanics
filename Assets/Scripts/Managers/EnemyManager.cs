using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] int enemyCount = 5;
    [SerializeField] Vector2 mapSize = new Vector2(10, 10);

    private List<Enemy> _enemies = new List<Enemy>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-mapSize.x, mapSize.x), 0, Random.Range(-mapSize.y, mapSize.y));
        var obj = Pool.Instance.GetObject(PoolType.Enemy);
        obj.transform.position = pos;
        _enemies.Add(obj.GetComponent<Enemy>());
    }

    public void Respawn(GameObject enemy)
    {
        Pool.Instance.ReturnObject(PoolType.Enemy, enemy);
        SpawnEnemy();
    }
}
