using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayoMagnesis : MonoBehaviour
{
    [SerializeField] private Magnesis m_magnesis;
    [SerializeField] private LayerMask includeLayers;
    [SerializeField] private Animator m_animator;
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & includeLayers) != 0)
        {
            m_magnesis.targetLogrado(collision.gameObject);
            m_animator.SetTrigger("end");
        }
    }*/
    private bool complete = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!complete && ((1 << collision.gameObject.layer) & includeLayers) != 0)
        {
            complete = true;
            m_magnesis.targetLogrado(collision.gameObject);
            m_animator.SetTrigger("end");
        }
    }
    public void setComplete(bool value)
    {
        complete = value;
    }
}
