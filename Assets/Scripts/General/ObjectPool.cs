using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private PooledObject prefab;
    public Queue<PooledObject> availableObjects = new Queue<PooledObject>();

    private static readonly Object _lock = new Object();

    private Transform cachedTransform;

    void Awake()
    {
        cachedTransform = gameObject.transform;
        //StartCoroutine(DestroyUnUsedObjectTask());
    }


    //public static int AUTO_DESTROY_POOL_INTERVAL = 600;
    //int _destroyIntervalCount = 0;
    //IEnumerator DestroyUnUsedObjectTask()
    //{
    //    while (true)
    //    {
    //        yield return null;
    //        _destroyIntervalCount++;
    //        if (_destroyIntervalCount > AUTO_DESTROY_POOL_INTERVAL)
    //        {
    //            if (availableObjects.Count > 0)
    //            {
    //                var objNeedDestroy = availableObjects.Dequeue();
    //                if (objNeedDestroy != null) //&& !objNeedDestroy.isActiveAndEnabled)
    //                {
    //                    Destroy(objNeedDestroy.gameObject);
    //                }
    //            }
    //            _destroyIntervalCount = 0;
    //        }
    //    }
    //}

    public static ObjectPool GetPool(PooledObject prefab)
    {
        //Debug.Log("Name Prefab: " + prefab.name);
        GameObject obj = new GameObject(prefab.name + " Pool");
        ObjectPool pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }

    public static ObjectPool InitPool(PooledObject prefab, int count)
    {
        ObjectPool pool = GetPool(prefab);
        for (int i = 0; i < count; i++)
        {
            PooledObject obj = Instantiate<PooledObject>(prefab);
            obj.transform.SetParent(pool.transform, false);
            obj.pool = pool;
            pool.AddObject(obj);
        }
        return pool;
    }

    public PooledObject GetObject()
    {
        PooledObject obj;
        if (availableObjects.Count > 0)
        {
            obj = availableObjects.Dequeue();
            if (obj == null || obj.isActiveAndEnabled) obj = Instantiate(prefab);
            obj.gameObject.SetActive(true);
        }
        else obj = Instantiate(prefab);
        obj.transform.SetParent(cachedTransform, false);
        obj.pool = this;
        return obj;
    }

    public void AddObject(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(cachedTransform);
        availableObjects.Enqueue(obj);
    }
}
