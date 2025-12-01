using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectForceObject : MonoBehaviour
{
    public void ForceReturnToPool()
    {
        if (objectPooling != null)
        {
            objectPooling.ForceReturnToPool(this.gameObject);
        }
        else
        {
            Debug.Log("objectPooling es NULL en este caso.");
        }
    }
    ObjectPooling objectPooling;
    internal void AddObjectPooling(ObjectPooling _objectPooling)
    {
        objectPooling = _objectPooling;
    }
}
