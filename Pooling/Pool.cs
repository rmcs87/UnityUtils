﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();

    private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

    private PooledMonoBehaviour prefab;

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (pools.ContainsKey(prefab))
            return pools[prefab];

        var pool = new GameObject("Pool-" + prefab.name).AddComponent<Pool>();

        pool.prefab = prefab;

        pools.Add(prefab, pool);

        return pool;
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (objects.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = objects.Dequeue();

        return pooledObject as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(prefab) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.gameObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.gameObject.transform.SetParent(this.transform);
        objects.Enqueue(pooledObject);
    }
}

