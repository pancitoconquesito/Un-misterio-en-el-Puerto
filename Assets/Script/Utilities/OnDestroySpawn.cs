using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroySpawn : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] float tiempoEmision = 1.0f;

    public void DestruirYEmitir()
    {
        objectPooling.emitirObj(tiempoEmision, transform.position, true, false);
        Destroy(gameObject);
    }
}
