using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeSine : MonoBehaviour
{
    private Transform m_transform;
    private float x_ini,y_ini;
    void Start()
    {
        m_transform = transform;
        x_ini = m_transform.localScale.x;
        y_ini = m_transform.localScale.y;
        //m_transform.LeanScaleX();
    }
    //private bool mas= false, menos=false;
    private enum DIR { iz, der};
    private DIR direccion = DIR.der;

    private float count = 0;
    [SerializeField]private float factorCrecimento = 3;
    [SerializeField] private float limite;
    void Update()
    {
        if (direccion == DIR.der)
        {
            count+=Time.deltaTime*factorCrecimento;
            if (m_transform.localScale.x > limite + x_ini)
            {
                direccion = DIR.iz;
            }
        }
        else
        {
            count -= Time.deltaTime * factorCrecimento;
            if (m_transform.localScale.x < x_ini- limite)
            {
                direccion = DIR.der;
            }
        }
    }
}
