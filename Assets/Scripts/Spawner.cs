using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour, ISpawnerInfo where T : MonoBehaviour
{		
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 15;

    private ObjectPool<T> _pool;
    private int _totalSpawnedCount = 0;

    public event Action<T> Spawned;
    public event Action InfoChanged;

    public int TotalSpawned => _totalSpawnedCount;
    public int CreatedCount => _pool is not null ? _pool.CountAll : 0;
    public int ActiveCount => _pool is not null ? _pool.CountActive : 0;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => OnGetFromPool(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void OnGetFromPool(T obj)
    {
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        obj.gameObject.SetActive(true);

        _totalSpawnedCount++;
        Spawned?.Invoke(obj);
        InfoChanged?.Invoke();
    }

    public T Get()
    {
        return _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
        InfoChanged?.Invoke();
    }
}
