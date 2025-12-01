using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_PJ : MonoBehaviour, IDamageable
{
    private int totalVida=3;
    private int vidaActual;
    [SerializeField] private movementPJ m_movementPJ;
    [SerializeField] private Magnesis _magnesis;
    [SerializeField] private float tiempoInvulnerable;
    [SerializeField] private ui_corazon m_ui_corazon;
    [SerializeField] private dashPJ m_dashPJ;
    [SerializeField] InventaryChangePanels m_InventaryChangePanels;
    private float current_tiempoInvulnerable=0;
    private bool vivo;
    private bool puedeRecibirDanio = true;
    private DATA_SINGLETON m_DATA_SINGLETON;
    CameraController m_CameraController;
    TimeManager m_timeManager;
    [SerializeField] ReSpawn_InGame m_ReSpawn_InGame;
    [SerializeField] PJ_MAPA m_PJ_MAPA;
    [SerializeField] FlashSprite flash;
    public bool PuedeRecibirDanio { get => puedeRecibirDanio; set => puedeRecibirDanio = value; }

    Ui_Anim m_Ui_Anim;
    public void ActualziarValoresVida(int totalVida, int vidaActual)
    {
        this.totalVida = totalVida;
        this.vidaActual = vidaActual;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_CameraController = MASTER_REFERENCE.instance.CameraController;
        m_timeManager = MASTER_REFERENCE.instance.TimeManager;
        m_Ui_Anim = MASTER_REFERENCE.instance.Ui_Anim;

        vivo = true;
        m_DATA_SINGLETON = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        vidaActual = m_DATA_SINGLETON.VidaPj;
        totalVida = m_DATA_SINGLETON.VidaMAXIMA_pj;
        if (vidaActual==0)
        {
            //totalVida = DATA.instance.getVidaActual();
            totalVida = DATA.instance.GetMaxCantidadVida();
            vidaActual = 3;
            m_ui_corazon.updateVida_UI(vidaActual);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current_tiempoInvulnerable > -1)
        {
            current_tiempoInvulnerable -= Time.deltaTime;
        }
    }
    public bool addVida(int valor)//Modificar si recibe vida positiva
    {
        if (vivo && m_movementPJ.GetState() != GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            if (valor < 0 && !puedeRecibirDanio) return false;
            vidaActual += valor;
            if (vidaActual > totalVida) vidaActual = totalVida;
            
            m_DATA_SINGLETON.VidaPj = vidaActual;
            m_movementPJ.GetDanio();
            m_PJ_MAPA.EndMapa(false);
            m_ui_corazon.updateVida_UI(vidaActual);
            m_Ui_Anim.StartDolor();
            Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_danio);
            m_timeManager.SetTime(0.005f, .4f);
            m_CameraController.ShakeCamera(15,2, 0.6f);
            if (vidaActual <= 0)
            {
                Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_death);
                vidaActual = 0;
                vivo = false;
                m_DATA_SINGLETON.VidaPj = vidaActual;
                m_ui_corazon.updateVida_UI(vidaActual);
                morir();
                return false;
            }
            return true;
        }
        return false;
    }

    private void morir()
    {
        print("me mori!!!");
        MASTER_REFERENCE.instance.Ui_Anim.DesaparecerUI_Bloquear();
        m_CameraController.SetCameraMuerte();
        m_InventaryChangePanels.desactivarInventario(false, false);
        m_DATA_SINGLETON.resetDataMorir();
        //acercar camera
        //MASTER_REFERENCE.instance.CameraController.SetCameraMuerte();
        m_movementPJ.morir();
    }

    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        //Debug.Log("RECIBO DANIO!!!!: "+ current_tiempoInvulnerable);
        bool retorno = false;
        if (current_tiempoInvulnerable < 0)
        {
            if (!vivo)
            {
                return false;
            }
            flash.Flashear();
            retorno = true;
            //detener otras cosas
            m_InventaryChangePanels.desactivarInventario_Si_estaActivado();
            _magnesis.detenerRayo();
            m_dashPJ.StopParticles_delay();
            m_movementPJ.swordFinished();
            //m_CameraController.SetCameraGameplay_normal();



            switch (m_dataDanio.tipo_danio)
            {
                case GLOBAL_TYPE.TIPO_DANIO.lava:
                {
                    current_tiempoInvulnerable = tiempoInvulnerable;
                    if (addVida(-m_dataDanio.getDanio())) m_movementPJ.recibirDanio(m_dataDanio, false);
                    break;
                }
                case GLOBAL_TYPE.TIPO_DANIO.normal:
                {
                    current_tiempoInvulnerable = tiempoInvulnerable * 0.7f;
                    if (addVida(-m_dataDanio.getDanio())) m_movementPJ.recibirDanio(m_dataDanio, false);
                    break;
                }
                case GLOBAL_TYPE.TIPO_DANIO.vacio:
                {
                    retorno = false;
                        //Debug.Break();
                        current_tiempoInvulnerable = 0;
                        m_ReSpawn_InGame.RespawnearPJ(m_dataDanio, this);
                    //if (addVida(-m_dataDanio.getDanio())) m_movementPJ.recibirDanio(m_dataDanio, false);
                    break;
                }
            }

        }
        else
        {
            //!
            //if (m_dataDanio.tipo_danio == GLOBAL_TYPE.TIPO_DANIO.normal)
            //    m_movementPJ.recibirDanio(m_dataDanio, true);
        }
        return retorno;
    }
    public bool IsEnemy()
    {
        return false;
    }
}
