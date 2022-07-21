using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingObjectPoolManager
{

    private static IObjectPool objectPool = new StackObjectPool();

    public static bool IsItemAtPool { get => !objectPool.IsPoolEmpty; }

    public static Transform Take()
    {
        return objectPool.TakeFromPool();
    }
    public static void Add(Transform _transform)
    {
        objectPool.AddToPool(_transform);
    }

}
