using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class DestruirOnTrigger : MonoBehaviour
{

    [SerializeField, Tag] string m_tag;
    [SerializeField]GameObject obj_destroy;
    bool activado=true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activado)
        {
            return;
        }
        if (collision.CompareTag(m_tag))
        {
            activado = false;
            Ejecutar();
        }
    }

    public void Ejecutar()
    {
        //sss
        Destroy(obj_destroy);
    }
}
