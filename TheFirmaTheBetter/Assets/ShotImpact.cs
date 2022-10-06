using Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotImpact : MonoBehaviour
{
    private ObjectPool pool; 

    public void Init(ObjectPool pool)
    {
        this.pool = pool;
    }

    private void OnDisable()
    {
        pool.ReturnToPool(gameObject);
    }
}
