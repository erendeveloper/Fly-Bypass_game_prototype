using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjectPool :IObjectPool
{
    Stack<Transform> pool = new Stack<Transform>();

    public bool IsPoolEmpty
    {
        get
        {
            if (pool.Count > 0)
                return false;
            else
                return true;
        }
    }

    public void AddToPool(Transform _transform)
    {
        pool.Push(_transform);
    }
    public Transform TakeFromPool()
    {
        return pool.Pop();
    }


}