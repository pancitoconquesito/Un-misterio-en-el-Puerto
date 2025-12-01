using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NS_ReturnPatrulla_SECTION : MonoBehaviour
{
    public enum TIPO
    {
        Caminando, Volando, Teletransportando
    }
    //public enum ProcesoSalto
    //{
    //    sinSaltar, preparandoSalto, saltando, 
    //}
    //ProcesoSalto procesoSalto = ProcesoSalto.sinSaltar;
    [SerializeField] GameObject m_go_InitialPosition;
    [SerializeField] LookAt2D_Rotator m_LookAt2D_Rotator;
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] float m_velocidad;
    [SerializeField] bool hasGravity;
    [SerializeField] TIPO tipo;
    [SerializeField] Animator animacion;

    [Header("Teletransportacion")]
    [SerializeField] float delay_Teletransportacion;
    [SerializeField] GameObject go_NS;

    [Header("Caminando")]
    [SerializeField] float minDistancia_return=1f;
    [SerializeField] bool incluyeSalto;
    [SerializeField] NS_Salto m_NS_Salto;
    //[SerializeField] CheckerRayCast CheckerRayCast_paredSalto;
    //[SerializeField] CheckerRayCast CheckerRayCast_suelo;
    //[SerializeField] Vector2 potenciaSalto;
    //[SerializeField] float tiempoSalto = 2f;
    //float curr_tiempoSalto;
    //bool saltando = false;
    Vector2 initialPositionVector;
    Vector2 movimientoFinal = Vector2.zero;
    bool activo = false;
    private void Awake()
    {
        initialPositionVector = m_go_InitialPosition.transform.position;
        m_NS_Salto.OnSalto += StartSalto;
        m_NS_Salto.EndSalto += EndSalto;
        m_NS_Salto.SetGoTarget(m_go_InitialPosition);
        //CheckerRayCast_paredSalto.OnHitEnter += Saltar;
    }

    void Start()
    {
        if (!activo)
        {
            return;
        }
        movimientoFinal = Vector2.zero;
    }

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
        switch (tipo)
        {
            case TIPO.Teletransportando:
                {
                    break;
                }
            case TIPO.Caminando:
                {
                    Vector2 rev_originalPosition = initialPositionVector;
                    rev_originalPosition.y = transform.position.y;
                    Vector2 dir = (rev_originalPosition - (Vector2)transform.position).normalized;
                    movimientoFinal = m_velocidad * dir;
                    m_LookAt2D_Rotator.LookAtDirection(dir);
                    
                    Draw_Raycast.DibujarFlechaCompleta((Vector2)transform.position, dir, Color.yellow, 3f);
                    Draw_Raycast.DibujarPuntoFinal(rev_originalPosition, dir, Color.white);

                    float distancia = Vector2.Distance(transform.position, rev_originalPosition);
                    if(distancia < minDistancia_return)
                    {
                        TerminoRetorno();
                    }
                    break;
                }
        }




        //if (curr_tiempoSalto > -1f)
        //{
        //    curr_tiempoSalto -= Time.deltaTime;
        //}

    }

    //private void Saltar(GameObject obj)
    //{
    //    if (/*procesoSalto == ProcesoSalto.sinSaltar &&*/ incluyeSalto && CheckerRayCast_suelo.IsColisionando)
    //    {
    //        procesoSalto = ProcesoSalto.preparandoSalto;
    //    }
    //}
    bool saltando = false;
    void StartSalto()
    {
        saltando = true;
    }
    void EndSalto()
    {
        curr_delaySalto = delaySalto;
        saltando = false;
    }
    float delaySalto = 1.5f;
    float curr_delaySalto = -0.5f;
    private void FixedUpdate()
    {
        if (!activo || saltando || curr_delaySalto > 0)//|| tipo == TIPO.Teletransportando)
        {
            return;
        }



        //if (procesoSalto == ProcesoSalto.preparandoSalto)
        //{
        //    Debug.Log("Salto! 3");
        //    curr_tiempoSalto = tiempoSalto;
        //    procesoSalto = ProcesoSalto.saltando;
        //}
        //if(procesoSalto == ProcesoSalto.saltando)
        //{
        //    float lado = potenciaSalto.x;
        //    if (movimientoFinal.x < 0)
        //    {
        //        lado *= -1;
        //    }
        //    m_rb.AddForce(Vector2.up * potenciaSalto.y + Vector2.right * lado, ForceMode2D.Force);
            

        //    if (curr_tiempoSalto<0f)
        //    {
        //        procesoSalto = ProcesoSalto.sinSaltar;
        //    }
        //    return;
        //}

        if (hasGravity)
        {
            m_rb.velocity = movimientoFinal + new Vector2(0, -m_rb.gravityScale);
        }
        else
        {
            m_rb.velocity = movimientoFinal;
        }
    }

    NS_States_v2026_1 nS_States_v2026_1;
    internal void InicarRetorno(NS_States_v2026_1 nS_States_v2026_1)
    {
        this.nS_States_v2026_1 = nS_States_v2026_1;
        activo =true;
        if(tipo == TIPO.Teletransportando)
        {
            StopAllCoroutines();
            StartCoroutine(TareaTeletransportar());
        }
    }
    internal void TerminoRetorno()
    {
        activo = false;
        nS_States_v2026_1.TerminoRetorno();
    }
    internal void DetenerRetorno()
    {
        activo = false;
        StopAllCoroutines();
        StopAllCoroutines();
    }
    IEnumerator TareaTeletransportar()
    {
        yield return new WaitForSeconds(delay_Teletransportacion);
        animacion.SetTrigger("tr_teletransportar");
        yield return new WaitForSeconds(delay_Teletransportacion/2f);
        go_NS.transform.position = initialPositionVector;
        animacion.SetTrigger("tr_teletransportar_move");
        yield return new WaitForSeconds(delay_Teletransportacion);
        nS_States_v2026_1.Alert_RetornoCompletado();
    }
    public void Detener_Retorno() => StopAllCoroutines();
    private void OnDisable() => StopAllCoroutines();
    private void OnDestroy()=>StopAllCoroutines();
}
