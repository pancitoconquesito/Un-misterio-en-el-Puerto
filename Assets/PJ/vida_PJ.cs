using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_PJ : MonoBehaviour, IDamageable
{
    private int totalVida=3;
    private int vidaActual;
    [SerializeField] private movementPJ m_movementPJ;
    [SerializeField] private float tiempoInvulnerable;
    [SerializeField] private ui_corazon m_ui_corazon;
    private float current_tiempoInvulnerable=0;
    private bool vivo;
    private DATA_SINGLETON m_DATA_SINGLETON;


    // Start is called before the first frame update
    void Start()
    {
        vivo = true;
        m_DATA_SINGLETON = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        vidaActual = m_DATA_SINGLETON.vidaPj;
        totalVida = m_DATA_SINGLETON.vidaMAXIMA_pj;
    }

    // Update is called once per frame
    void Update()
    {
        current_tiempoInvulnerable -= Time.deltaTime;
        if (current_tiempoInvulnerable < -100) current_tiempoInvulnerable = -100;
    }
    public bool addVida(int valor)
    {
        if (vivo && m_movementPJ.test_getEstado() != GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            vidaActual += valor;
            if (vidaActual > totalVida) vidaActual = totalVida;
            
            m_DATA_SINGLETON.vidaPj = vidaActual;
            m_ui_corazon.updateVida_UI(vidaActual);
            if (vidaActual <= 0)
            {
                vidaActual = 0;
                vivo = false;
                m_DATA_SINGLETON.vidaPj = vidaActual;
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
        m_DATA_SINGLETON.resetDataMorir();
        m_movementPJ.morir();
    }

    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        bool retorno = false;
        if (current_tiempoInvulnerable < 0)
        {
            retorno = true;
            m_movementPJ.swordFinished();
            if(m_dataDanio.tipo_danio!= GLOBAL_TYPE.TIPO_DANIO.lava)
            {
                current_tiempoInvulnerable = tiempoInvulnerable;
                if (addVida(-m_dataDanio.getDanio())) m_movementPJ.recibirDanio(m_dataDanio, false);
            }
            else
            {
                current_tiempoInvulnerable = tiempoInvulnerable*0.7f;
                if (addVida(-m_dataDanio.getDanio())) m_movementPJ.recibirDanio(m_dataDanio, false);
            }
        }
        else
        {
            if(m_dataDanio.tipo_danio==GLOBAL_TYPE.TIPO_DANIO.normal)
                m_movementPJ.recibirDanio(m_dataDanio, true);
        }
        return retorno;
    }
}
