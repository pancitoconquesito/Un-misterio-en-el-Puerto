using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rotar : MonoBehaviour
{
    private enum TipoRect
    {
        transform, rectTransform
    }
    [SerializeField] private bool _X, _Y,_Z;
    [SerializeField] private float speed;
    [SerializeField] private bool activar;
    private TipoRect m_TipoRect;
    private RectTransform m_RectTransform;
    private Transform m_Transform;
    private bool activo = false;
    public bool Activo
    {
        get
        {
            return activo;
        }
        set
        {
            activo = value;
        }
    }
    private void Awake()
    {
        m_TipoRect = TipoRect.rectTransform;
        m_RectTransform = GetComponent<RectTransform>();
        if (m_RectTransform == null)
        {
            m_Transform = transform;
            m_TipoRect = TipoRect.transform;
        }
        activo = activar;

        speed *= 60;
    }
    void Update()
    {
        if (activo)
        {
            rotarObjeto();
        }
    }

    private void rotarObjeto()
    {
        if (m_TipoRect == TipoRect.rectTransform)
        {
            Vector3 currentRotation = m_RectTransform.rotation.eulerAngles;
            if (_X) currentRotation.x += speed*Time.deltaTime;
            if (_Y) currentRotation.y += speed * Time.deltaTime;
            if (_Z) currentRotation.z += speed * Time.deltaTime;
            m_RectTransform.rotation = Quaternion.Euler(currentRotation);
        }
        else
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            if (_X) currentRotation.x += speed * Time.deltaTime;
            if (_Y) currentRotation.y += speed * Time.deltaTime;
            if (_Z) currentRotation.z += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

}
