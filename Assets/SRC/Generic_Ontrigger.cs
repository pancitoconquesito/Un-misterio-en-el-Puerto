using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class Generic_Ontrigger : MonoBehaviour
{

    [SerializeField, Tag] List<string> m_tag;
    public UnityEvent onEjecutar;
    bool ejecutado = false;

    private void OnEnable()
    {
        ejecutado = false;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (ejecutado)
    //    {
    //        return;
    //    }
    //    foreach (var item in m_tag)
    //    {
    //        if (!string.IsNullOrEmpty(item) && collision.CompareTag(item))
    //        {
    //            ejecutado = true;
    //            onEjecutar?.Invoke();
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ejecutado)
        {
            return;
        }
        foreach (var item in m_tag)
        {
            if (!string.IsNullOrEmpty(item) && collision.CompareTag(item))
            {
                ejecutado = true;
                onEjecutar?.Invoke();
            }
        }
    }


    public void EjecutarEvento()
    {
        onEjecutar?.Invoke();
    }
}
