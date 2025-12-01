using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField]Rigidbody2D m_rb;
    [SerializeField]float potencia;
    [SerializeField]GameObject m_go;
    [SerializeField]GameObject m_content;
    Vector3 direccion;
    bool activado;

    //bool lineal = true;
    SpawnerDisparo.ModeMove modeMove;
    float amplitud;  
    float frecuencia;
    float tiempo;
    Vector3 posicionInicial;


    float expansion;
    float radio;
    private void Update()
    {
        switch (modeMove)
        {
            case SpawnerDisparo.ModeMove.Lineal:
            {
                return;
            }
            case SpawnerDisparo.ModeMove.Sine:
            {
                tiempo += Time.deltaTime;
                Vector3 direccionPrincipal = transform.right; // forward si estás en 3D, right si estás en 2D
                Vector3 direccionPerpendicular = Vector3.Cross(direccionPrincipal, Vector3.forward).normalized;
                Vector3 desplazamientoLineal = direccionPrincipal * potencia * tiempo;
                Vector3 desplazamientoSeno = direccionPerpendicular * Mathf.Sin(tiempo * frecuencia) * amplitud;
                transform.position = posicionInicial + desplazamientoLineal + desplazamientoSeno;
                break;
            }
            case SpawnerDisparo.ModeMove.Circular:
            {
                tiempo += Time.deltaTime;
                Vector3 direccionAvance = transform.right.normalized; // avanza en la dirección de su rotación (Z)
                Vector3 desplazamientoLineal = direccionAvance * potencia * tiempo;
                float angulo = tiempo * frecuencia; // ángulo de rotación creciente
                float radioActual = radio + tiempo * expansion;
                Vector3 direccionPerpendicular = Vector3.Cross(direccionAvance, Vector3.forward).normalized;
                Vector3 desplazamientoCircular =
                    (Mathf.Cos(angulo) * direccionAvance + Mathf.Sin(angulo) * direccionPerpendicular) * radioActual;
                transform.position = posicionInicial + desplazamientoLineal + desplazamientoCircular;
                break;
            }
        }

    }


    public void SetValues(Vector3 direccion, bool setAngleIqualDirection)
    {
        m_content.SetActive(true);
        if (setAngleIqualDirection)
        {
            //m_go.transform.forward = this.direccion.normalized;
            RotateToDirection(new Vector2(direccion.x, direccion.y));
            //transform.rotation = Quaternion.LookRotation(direccion.normalized);
        }
        this.direccion = direccion;
        
    }
    void RotateToDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    internal void SetPotencia(float potenciaProyectil)
    {
        potencia = potenciaProyectil;
    }

    internal void SetLinealMove()
    {
        this.activado = true;
        if (this.activado)
        {
            this.modeMove = SpawnerDisparo.ModeMove.Lineal;
            m_rb.velocity = this.direccion.normalized * potencia;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
        }
    }

    internal void SetSineMove(float amplitud, float frecuencia)
    {
        this.activado = true;
        if (this.activado)
        {
            this.modeMove = SpawnerDisparo.ModeMove.Sine;
            posicionInicial = transform.position;
            this.amplitud = amplitud;
            this.frecuencia = frecuencia;
            this.tiempo = 0f;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
        }
    }

    internal void SetCircularMove(float frecuencia, float radio, float expansion)
    {
        this.activado = true;
        if (this.activado)
        {
            this.modeMove = SpawnerDisparo.ModeMove.Circular;
            posicionInicial = transform.position;
            this.radio = radio;
            this.expansion = expansion;
            this.frecuencia = frecuencia;
            this.tiempo = 0f;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
        }
    }
}
