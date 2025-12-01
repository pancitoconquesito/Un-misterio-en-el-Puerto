using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ReturnTRU : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] Collider2D collider;
    enum estados
    {
        none, cambiando
    }
    estados curr_estado = estados.none;
    void Update()
    {
        if (curr_estado==estados.none && collider.isTrigger)
        {
            curr_estado = estados.cambiando;
            StartCoroutine(RetornarRotacion());
        }
    }

    IEnumerator RetornarRotacion()
    {
        yield return new WaitForSecondsRealtime(delay);
        collider.isTrigger = false;
        curr_estado = estados.none;
    }
}
