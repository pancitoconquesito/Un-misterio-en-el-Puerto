using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Perseguir_25 : MonoBehaviour
{
    [SerializeField] LookAt2D_Rotator m_LookAt2D_Rotator;
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] float velocidad;
    [SerializeField] bool ignoreY;
    [SerializeField] bool hasGravity;
    [SerializeField] bool hasAlert;
    [SerializeField] bool hasReturn;
    [SerializeField] Animator animator;
    [SerializeField] float distanciaMinima;
    [SerializeField] NS_Salto m_NS_Salto;

    [ShowNonSerializedField] bool activo=false;

    GameObject m_go_target;
    Vector2 direccion;


    public bool HasReturn { get => hasReturn; set => hasReturn = value; }
    public bool Activo { get => activo; set => activo = value; }

    NS_States_v2026_1 m_NS_States_v2026_1_MASTER;
    internal void Config(NS_States_v2026_1 nS_States_v2026_1)
    {
        m_NS_States_v2026_1_MASTER = nS_States_v2026_1;
    }
    private void Awake()
    {
        m_NS_Salto.OnSalto += StartSalto;
        m_NS_Salto.EndSalto += EndSalto;
        
    }
    // Start is called before the first frame update
    void StartSalto() => saltando = true;
    void EndSalto() { 
        curr_delaySalto = delaySalto;
        saltando = false;
    }
    bool saltando = false;
    // Update is called once per frame
    void Update()
    {
        if (!activo)
        {
            return;
        }
        if (curr_delaySalto > -1)
        {
            curr_delaySalto -= Time.deltaTime;
        }
        //m_NS_Salto.Activo = true;
        Vector2 vectorReserva = m_go_target.transform.position;
        if (ignoreY)
        {
            vectorReserva.y = transform.position.y;
        }
        float distancia = Vector2.Distance(vectorReserva, transform.position);
        if (distancia < distanciaMinima)
        {
            m_NS_States_v2026_1_MASTER.DistanciaMinimaPerseguir();
            return;
        }
        Vector2 posTarget = m_go_target.transform.position;
        if (ignoreY)
        {
            posTarget.y = transform.position.y;
        }
        direccion = (posTarget - (Vector2)transform.position).normalized;
        m_LookAt2D_Rotator.LookAtDirection(direccion);
    }

    public bool StartPerseguir(GameObject m_go_target, bool desdeAlert=false)
    {
        m_NS_Salto.SetGoTarget(m_go_target);
        this.m_go_target = m_go_target;
        if (hasAlert && !desdeAlert)
        {
            animator.SetTrigger("tr_alert");
        }
        else
        {
            animator.SetTrigger("tr_perseguir");
            activo = true;
        }
        return hasAlert;
    }

    public void StopPerseguir(bool desdeAnim=false)
    {
        activo = false;
        if (hasReturn && !desdeAnim)
        {
            //Debug.Log("tr_return");
            animator.SetTrigger("tr_return");
        }
    }
    float delaySalto = 1.5f;
    float curr_delaySalto = 0;
    private void FixedUpdate()
    {
        if (!activo || saltando || curr_delaySalto>0)
        {
            return;
        }

        if (m_NS_States_v2026_1_MASTER.RecibiendoDanio)
        {
            m_rb.velocity = m_NS_States_v2026_1_MASTER.VelocidadImpacto;
        }
        else
        {
            if (hasGravity)
            {
                m_rb.velocity = direccion * velocidad + Vector2.down * m_rb.gravityScale;
            }
            else
            {
                m_rb.velocity = direccion * velocidad;
            }

        }


    }

}
