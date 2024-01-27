using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExtendedObjectPool<T> where T : MonoBehaviour
{
    private ObjectPool<T> pool;
    private List<T> activeObjects = new List<T>();

    public ExtendedObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, int maxSize = 10)
    {
        pool = new ObjectPool<T>(
            createFunc: createFunc,
            actionOnGet: (obj) =>
            {
                actionOnGet?.Invoke(obj);
                activeObjects.Add(obj);
            },
            actionOnRelease: (obj) =>
            {
                actionOnRelease?.Invoke(obj);
                activeObjects.Remove(obj);
            },
            actionOnDestroy: actionOnDestroy,
            maxSize: maxSize
        );
    }

    public T Get()
    {
        return pool.Get();
    }

    public void Release(T obj)
    {
        pool.Release(obj);
    }

    public List<T> GetActiveObjects()
    {
        return activeObjects;
    }
}