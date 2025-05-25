using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance;
    [SerializeField] private List<PoolItem> prefabs;
    [SerializeField] private int poolSize = 10;

    private Dictionary<PoolType, Queue<GameObject>> _poolDic = new Dictionary<PoolType, Queue<GameObject>>();
    private List<GameObject> _allActiveObj  = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    public void Initialize()
    {
        FillPool();
        SetSubscriptions();
    }

    private void FillPool()
    {
        foreach (var pool in prefabs)
        {
            _poolDic[pool.poolType] = new Queue<GameObject>();
            for (var i = 0; i < poolSize; i++)
            {
                var instance = Instantiate(pool.obj, transform);
                instance.gameObject.SetActive(false);
                _poolDic[pool.poolType].Enqueue(instance);
            }
        }
    }

    private GameObject GiveObject(PoolType _poolType)
    {
        if (_poolDic.TryGetValue(_poolType, out var queue) && queue.Count > 0)
        {
            var obj = queue.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        var prefabData = prefabs.FirstOrDefault(p => p.poolType == _poolType);
        if (prefabData == null) return null;
        if (!_poolDic.ContainsKey(_poolType)) _poolDic[_poolType] = new Queue<GameObject>();

        var newObj = Instantiate(prefabData.obj, transform);
        return newObj;
    }

    public GameObject GetObject(PoolType _poolType)
    {
        var obj = GiveObject(_poolType);
        if (_poolType == PoolType.Enemy && !_allActiveObj.Contains(obj)) 
            _allActiveObj.Add(obj);
        return obj;
    }

    public void ReturnObject(PoolType _poolType, GameObject obj)
    {
        if (_poolDic.TryGetValue(_poolType, out var queue))
        {
            _poolDic[_poolType].Enqueue(obj);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(false);
        }
    }

    public List<GameObject> GetAllEnemys()
    {
        return _allActiveObj.Where(p => p.activeSelf)
                .Select(p => p).ToList();
    }

    private void SetSubscriptions()
    {
       
    }

    private void SetUnsubscriptions()
    {
        
    }

    private void OnDisable()
    {
        SetUnsubscriptions();
    }
}


public enum PoolType
{
    Enemy,
    Projectile,

}

[System.Serializable]
public class PoolItem
{
    public GameObject obj;
    public PoolType poolType;
}