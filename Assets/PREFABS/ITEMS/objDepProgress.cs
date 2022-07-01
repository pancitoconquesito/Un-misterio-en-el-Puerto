using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class objDepProgress : MonoBehaviour
{
    public enum ACCION
    {
        destruir, animacion, nada
    }
    [SerializeField] private GLOBAL_TYPE.TIPO_PREFAB tipoPrefab;
    [SerializeField] private int idPrefab;
    [SerializeField] private ACCION accion;
    [Header("-- Animacion --")]
    [SerializeField] private Animator m_animator;
    [SerializeField]private string nombreAnimacion;
    [SerializeField] private bool startWithTrigger;
    [SerializeField] private string tagTrigger;
    void Start()
    {
        if(DATA.instance.save_load_system.isGenericProgress(tipoPrefab, idPrefab))
        {
            switch (accion)
            {
                case ACCION.destruir:
                    {
                        Destroy(gameObject);
                        break;
                    }
                case ACCION.animacion:
                    {
                        if(!startWithTrigger)
                            m_animator.SetTrigger(nombreAnimacion);
                        break;
                    }
            }
        }
    }
    private bool complete=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startWithTrigger && !complete && collision.CompareTag(tagTrigger))
        {
            complete = true;
            m_animator.SetTrigger(nombreAnimacion);
        }
    }
}
