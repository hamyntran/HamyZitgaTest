using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.Rendering;

/// <summary>
/// Must set PooledObject var to null when call ReturnToPool if you want to check null
/// </summary>
public class PooledObject : MonoBehaviour
{

    [HideInInspector]
    public ObjectPool pool;

    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;

    public void InitPool<T>(int count) where T : PooledObject
    {
        poolInstanceForPrefab = ObjectPool.InitPool(this, count);
    }

    public T GetPooledInstance<T>() where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        return (T)poolInstanceForPrefab.GetObject();
    }

    public T GetPooledInstance<T>(Vector3 position)
      where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        var obj = poolInstanceForPrefab.GetObject();
        obj.transform.position = position;
        return (T)obj;
    }

    public T GetPooledInstance<T>(Vector3 localPosition, Transform parent)
      where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        var obj = poolInstanceForPrefab.GetObject();
        obj.transform.parent = parent;
        obj.transform.localPosition = localPosition;
        return (T)obj;
    }

    public T GetPooledInstance<T>(Vector3 position,
      Quaternion rotation) where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        var obj = poolInstanceForPrefab.GetObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return (T)obj;
    }
    /// <summary>
    /// Will call Object.OnDisable
    /// </summary>
    public virtual void ReturnToPool() { if (pool) pool.AddObject(this); }

}
