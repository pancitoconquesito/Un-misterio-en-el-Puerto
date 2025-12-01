using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class staminaPsiquica : MonoBehaviour
{
    [SerializeField] private Image m_imageFill;
    
    [SerializeField] private float cantidadTotalPoder;
    [SerializeField] private float cantidadActual;

    [SerializeField] private float factorRecuperacion;
    [SerializeField] private float delayRecuperacion;
    private float delayActual_recuperacion;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float delayOcultar;
    private float delayActual_ocultar;


    [Header("-- Colores --")]
    [SerializeField] private Color colorFull;
    [SerializeField] private Color colorRecuperar;
    [SerializeField] private Color colorDetenido;
    private movementPJ m_movementPJ;
    bool m_activeScript = true;
    private float coste_dash;
    private float coste_espadazo;
    private float coste_magnesis;
    private float coste_inicialCapturaMagnesis;
    private float coste_Disparo;
    private float coste_Bomba;
    private float coste_Teletransportacion;
    private float coste_Inmersion;
    private float coste_Quinto;
    public void setCosteDash(float valor) { coste_dash = valor; }
    public void setCosteEspadazo(float valor) { coste_espadazo = valor; }
    public void setCosteMagnesis(float valor) { coste_magnesis = valor; }
    public void setCosteInicialMagnesis(float valor) { coste_inicialCapturaMagnesis = valor; }
    public bool puedeDash() { return cantidadActual > coste_dash; }
    public bool puedeEspadazo() { return cantidadActual > coste_espadazo; }
    public bool puedeMagnesis() { return cantidadActual > coste_magnesis; }
    public bool puedeCapturarConMagnesis() { return cantidadActual > coste_inicialCapturaMagnesis; }
    public float getCantidadStaminaPorcentaje() { return cantidadActual / cantidadTotalPoder * 100f; }

    private bool full = true;
    private bool visible = false;

    public float Coste_Disparo { get => coste_Disparo; set => coste_Disparo = value; }
    public float Coste_Bomba { get => coste_Bomba; set => coste_Bomba = value; }
    public float Coste_Teletransportacion { get => coste_Teletransportacion; set => coste_Teletransportacion = value; }
    public float Coste_Inmersion { get => coste_Inmersion; set => coste_Inmersion = value; }
    public float Coste_Quinto { get => coste_Quinto; set => coste_Quinto = value; }

    private void Start()
    {
        //cantidadTotalPoder = 100;
        cantidadActual = cantidadTotalPoder;
        factorRecuperacion += 0.1f;
        delayActual_recuperacion = -1;
        delayActual_ocultar = -1;
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
    }
    public void addStamina(float cantidad)
    {
        cantidadActual += cantidad;
        if (cantidadActual >= cantidadTotalPoder *.99) cantidadActual = cantidadTotalPoder;
        if (cantidadActual <= 0.01 && delayActual_recuperacion > 0) cantidadActual = 0;
        if (cantidad < 0) {
            if (!visible)
            {
                visible = true;
                m_animator.SetTrigger("aparecer");
            }
            full = false;
            delayActual_recuperacion = delayRecuperacion;
        }  

        m_imageFill.fillAmount = cantidadActual / cantidadTotalPoder;
    }
    
    public void DesactivarScript()
    {
        m_activeScript = false;
        visible = false;
        m_animator.SetTrigger("ocultar");
    }
    private void Update()
    {
        if (m_activeScript)
        {
            recuperar();
            completar();
            ocultar();
        }
    }

    private void completar()
    {
        if (cantidadActual >= cantidadTotalPoder * .99 && !full)
        {
            full = true;
            cantidadActual = cantidadTotalPoder;
            m_imageFill.fillAmount = 1;

            delayActual_ocultar = delayOcultar;


            m_imageFill.color = colorFull;
        }
    }

    private void recuperar()
    {
        if (!m_movementPJ.PuedeRecuperarStamina())
        {
            return;
        }
        delayActual_recuperacion -= Time.deltaTime;
        if (cantidadActual <= cantidadTotalPoder * .99 && delayActual_recuperacion <= 0)
        {
            delayActual_recuperacion = -1;
            addStamina(factorRecuperacion * Time.deltaTime);

            m_imageFill.color = colorRecuperar;
        }
        else if(!full)
            m_imageFill.color = colorDetenido;
    }
    
    private void ocultar()
    {
        if (full && visible)
        {
            delayActual_ocultar -= Time.deltaTime;
            if (delayActual_ocultar < 0)
            {
                visible = false;
                m_animator.SetTrigger("ocultar");
            }
        }
    }

    public float getCantidadStamina()
    {
        return cantidadActual;
    }


    [ContextMenu("quitar25")]
    public void quitar25()
    {
        addStamina(-25f);
    }


}
