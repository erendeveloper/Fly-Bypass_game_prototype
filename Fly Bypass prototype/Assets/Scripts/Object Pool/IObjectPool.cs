using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool
{
    public bool IsPoolEmpty{ get; }

    public void AddToPool(Transform _transform);

    public Transform TakeFromPool();
}
