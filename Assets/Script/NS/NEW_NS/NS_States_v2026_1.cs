using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class NS_States_v2026_1 : MonoBehaviour
{
    public enum NS_STATE
    {
        IDLE,
        PATRULLANDO,
        ALERT,
        PERSIGUIENDO,
        SALIENDO_PERSIGUIENDO,
        VOLVER_POSICION_INICIAL_PATRULLA,
        PRE_ESTRATEGIA,
        ESTRATEGIA,
        POSTESTRATEGIA,
    }

    [SerializeField] RecibiDanio_cb m_RecibiDanio_cb;
    [SerializeField] bool m_hasPatrulla;
    [SerializeField] bool m_hasPerseguir;
    [SerializeField] LookAt2D_Rotator m_lookAt2D_Rotator;
    [SerializeField] NS_Salto m_NS_Salto;
    [SerializeField] Animator anim;
    [SerializeField] CheckerRayCast m_checkerRayCast_suelo;
    [SerializeField] Rigidbody2D rb;


    [Header("---STATES CONTROLLERS ---")]
    [SerializeField] PatrullaState m_PatrullaState;
    [SerializeField] NS_ReturnPatrulla_SECTION m_NS_ReturnPatrulla_SECTION;

    [Header("--Perseguir")]
    [SerializeField] Perseguir_25 m_Perseguir_25;
    [SerializeField] Check_areaCollider m_Check_areaCollider_Exit;
    [SerializeField] bool m_debeEjecutarEstrategiaAlEstarCerca;
    [SerializeField] bool ignoreY;

    [Header("--Estrategias")]
    [SerializeField] Estrategias_Config m_estrategias_Config;
    [SerializeField] Vector2 m_v_esperaToEstrategia;
    [SerializeField] Vector2 m_v_esperaExitEstrategia;
    bool hasEstrategia;

    [ShowNonSerializedField] NS_STATE m_NS_STATE = NS_STATE.IDLE;
    RaycastCheck_GroupEvent RaycastCheck_GroupEvent;
    GameObject m_go_pj;
    Vector2 velocidadImpacto = Vector2.zero;
    bool recibiendoDanio = false;
    bool quitandoDanio = false;
    bool vivo =true;
    public Vector2 VelocidadImpacto { get => velocidadImpacto; set => velocidadImpacto = value; }
    public bool RecibiendoDanio { get => recibiendoDanio; set => recibiendoDanio = value; }

    private void Awake()
    {
        hasEstrategia = (m_estrategias_Config != null);
        if (m_hasPatrulla)
        {
            StartPatrulla();
        }

        if (m_Check_areaCollider_Exit != null)
        {
            m_Check_areaCollider_Exit.OnColision += EnterAreaPerseguir;
            m_Check_areaCollider_Exit.OnExitColision += ExitAreaPerseguir;
        }
        if (m_PatrullaState != null)
        {
            m_PatrullaState.HasPatrulla = m_hasPatrulla;
            m_PatrullaState.Config(this);
        }
        if (m_Perseguir_25 != null)
        {
            m_Perseguir_25.Config(this);
        }
        m_RecibiDanio_cb.OnRecibirDanio += OnRecibirDanio;
        m_RecibiDanio_cb.OnMorir += OnMorir;
        Debug.Log($"----------------------- {m_checkerRayCast_suelo.Nombre}");
    }

    internal void DistanciaMinimaPerseguir()
    {
        if (m_debeEjecutarEstrategiaAlEstarCerca)
        {
            esperandoEstrategia = true;
            startEstrategiaDesdeCercania = true;
            StopAllCoroutines();
            StartCoroutine(ComenzarEstrategia());
        }
    }

    void Start()
    {
        m_go_pj = MASTER_REFERENCE.instance.GO_PJ;
        RaycastCheck_GroupEvent = this.transform.GetComponent<RaycastCheck_GroupEvent>();
        RaycastCheck_GroupEvent.OnColision += OnColision_areaPerseguir;
    }

    bool esperandoEstrategia = false;
    private void Update()
    {
        if (!vivo)
        {
            return;
        }


        anim.SetFloat("velocity_y", rb.velocity.y);

        //
        anim.SetBool("isState_Patrulla", m_NS_STATE == NS_STATE.PATRULLANDO);
        anim.SetBool("isState_Perseguir", m_NS_STATE == NS_STATE.PERSIGUIENDO);

        //!pérmitir logica de salto
        m_NS_Salto.Activo = (m_NS_STATE == NS_STATE.PATRULLANDO
            || m_NS_STATE == NS_STATE.PERSIGUIENDO
            || m_NS_STATE == NS_STATE.VOLVER_POSICION_INICIAL_PATRULLA
            );
        //!si debe ahcer anims de salto caida
        //!si toca el suelo
        bool permiteAnimSalto = (m_NS_STATE == NS_STATE.PATRULLANDO
            || m_NS_STATE == NS_STATE.PERSIGUIENDO
            || m_NS_STATE == NS_STATE.VOLVER_POSICION_INICIAL_PATRULLA
            );
        anim.SetBool("permiteAnimSalto", permiteAnimSalto);

        anim.SetBool("ground", m_checkerRayCast_suelo.IsColisionando);

        //retroceso danio
        if (recibiendoDanio)
        {
            if (!quitandoDanio)
            {
                quitandoDanio = true;
                Invoke("QuitarDanio", 0.3f);
            }
        }
        if (hasEstrategia && m_checkerRayCast_suelo.IsColisionando)
        {
            if(enRangoAccion && !esperandoEstrategia && m_estrategias_Config.OnEstrategias) {
                StopAllCoroutines();
                StartCoroutine(ComenzarEstrategia());
            }
            if(esperandoEstrategia && !enRangoAccion)
            {
                esperandoEstrategia = false;
                m_PatrullaState.StopState();
                m_Perseguir_25.StopPerseguir(true);
                RetornarPatrulla();
            }
        }
    }
    bool startEstrategiaDesdeCercania = false;
    IEnumerator ComenzarEstrategia()
    {
        if(m_NS_STATE == NS_STATE.ESTRATEGIA)
        {
            yield return null;
        }
        esperandoEstrategia = true;

        Vector2 rev_originalPosition = m_go_pj.transform.position;
        if (ignoreY)
        {
            rev_originalPosition.y = transform.position.y;
        }
        Vector2 dir = (rev_originalPosition - (Vector2)transform.position).normalized;
        m_lookAt2D_Rotator.LookAtDirection(dir);

        float nRandom = UnityEngine.Random.Range(m_v_esperaToEstrategia.x, m_v_esperaToEstrategia.y);
        if (startEstrategiaDesdeCercania && m_debeEjecutarEstrategiaAlEstarCerca)
        {
            nRandom = 0;
        }
        yield return new WaitForSeconds(nRandom);
        startEstrategiaDesdeCercania = false;
        m_PatrullaState.StopState();
        m_Perseguir_25.StopPerseguir(true);
        m_NS_STATE = NS_STATE.ESTRATEGIA;
        m_estrategias_Config.StartEstrategia();
    }
    IEnumerator TerminarEstrategia()
    {
        float nRandom = UnityEngine.Random.Range(m_v_esperaExitEstrategia.x, m_v_esperaExitEstrategia.y);
        yield return new WaitForSeconds(nRandom);
        esperandoEstrategia = false;
    }
    internal void StartPerseguirDesdeAlert()
    {
        if (m_NS_STATE == NS_STATE.ESTRATEGIA
            || m_NS_STATE == NS_STATE.PRE_ESTRATEGIA
            )
        {
            return;
        }
        m_PatrullaState.StopState();
        m_Perseguir_25.StartPerseguir(m_go_pj, true);
        m_NS_STATE = NS_STATE.PERSIGUIENDO;
    }

    void OnColision_areaPerseguir()
    {
        if((m_NS_STATE == NS_STATE.PATRULLANDO 
            || m_NS_STATE == NS_STATE.VOLVER_POSICION_INICIAL_PATRULLA
            //|| m_NS_STATE == NS_STATE.POSTESTRATEGIA
            )
            && m_hasPerseguir)
        {
            m_NS_ReturnPatrulla_SECTION.DetenerRetorno();
            m_PatrullaState.StopState();
            bool hasAlertPerseguir = m_Perseguir_25.StartPerseguir(m_go_pj);
            if (hasAlertPerseguir)
            {
                m_NS_STATE = NS_STATE.ALERT;
            }
            else
            {
                m_NS_STATE = NS_STATE.PERSIGUIENDO;
            }
        }
    }
    bool enRangoAccion = false;
    private void EnterAreaPerseguir()
    {
        enRangoAccion = true;
    }
    void ExitAreaPerseguir()
    {
        enRangoAccion = false;
        if (m_NS_STATE == NS_STATE.PERSIGUIENDO
            || m_NS_STATE == NS_STATE.ALERT
            || m_NS_STATE == NS_STATE.SALIENDO_PERSIGUIENDO)
        {
            m_Perseguir_25.StopPerseguir();
            if (!m_Perseguir_25.HasReturn)
            {
                //m_NS_STATE = NS_STATE.SALIENDO_PERSIGUIENDO;
                //StartPAtrulla();
                ReturnExitPerseguir();
            }
        }
    }

    public void ReturnExitPerseguir(bool desdeAnim = false)
    {
        if (m_NS_STATE == NS_STATE.PRE_ESTRATEGIA
            || m_NS_STATE == NS_STATE.ESTRATEGIA
            )
        {
            return;
        }
        m_Perseguir_25.StopPerseguir(desdeAnim);
        if (m_hasPatrulla)
        {
            RetornarPatrulla();
        }
    }

    void RetornarPatrulla()
    {
        if (m_NS_STATE == NS_STATE.PRE_ESTRATEGIA
            || m_NS_STATE == NS_STATE.ESTRATEGIA
            )
        {
            return;
        }
        m_NS_STATE = NS_STATE.VOLVER_POSICION_INICIAL_PATRULLA;
        m_NS_ReturnPatrulla_SECTION.InicarRetorno(this);
    }
    internal void TerminoRetorno()
    {
        //!TODO
        StartPatrulla();
    }
    internal void Alert_RetornoCompletado()
    {
        //todo
        StartPatrulla();
    }

    void StartPatrulla()
    {
        m_NS_STATE = NS_STATE.PATRULLANDO;
        m_PatrullaState.StartState();
    }

    internal void SaliendoEstrategia()
    {
        Debug.Log("SaliendoEstrategia");
        m_NS_STATE = NS_STATE.POSTESTRATEGIA;
        if (enRangoAccion)
        {
            StartPerseguirDesdeAlert();
        }
        else{
            RetornarPatrulla();
        }
        StopAllCoroutines();
        StartCoroutine(TerminarEstrategia());
    }



    /// ///////////////////////////

    private void QuitarDanio()
    {
        if (recibiendoDanio) recibiendoDanio = false;
        quitandoDanio = false;
    }
    internal void OnRecibirDanio(Vector2 velocidadImpacto)
    {
        this.velocidadImpacto = velocidadImpacto;
        recibiendoDanio = true;
    }
    void OnMorir()
    {
        m_NS_ReturnPatrulla_SECTION.Detener_Retorno();
        vivo = false;
        QuitarDanio();
        m_PatrullaState.StopState();
        m_Perseguir_25.Activo = false;
        Debug.Log("NS murio! 26");
    }
}
