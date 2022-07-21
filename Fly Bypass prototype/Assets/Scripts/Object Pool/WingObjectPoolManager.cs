using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingObjectPoolManager : MonoBehaviour
{

    private IObjectPool objectPool = new StackObjectPool();

    public bool IsItemAtPool { get => !objectPool.IsPoolEmpty; }

    public Transform Take()
    {
        return objectPool.TakeFromPool();
    }
    public void Add(Transform _transform)
    {
        objectPool.AddToPool(_transform);
    }

}
