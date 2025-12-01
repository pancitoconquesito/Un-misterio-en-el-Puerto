using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setData : MonoBehaviour
{
    private Transform[] posicionInicio;
    private GameObject pj_OBJ;
    private movementPJ m_movementPJ;
    
    private void Awake()
    {
        SetIniPosition();
    }
    void Start()
    {
        pj_OBJ = GameObject.FindGameObjectWithTag("Player");
        m_movementPJ = pj_OBJ.GetComponent<movementPJ>();
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        int posicion = 0;
        //if (DATA.instance.IsSceneNormal_noNeko)
        //{
            posicion = singleton.Id_entrada_siguienteEtapa;
        //}
        //else
        //{
        //    posicion = singleton.Id_entrada_siguienteEtapa_NEKO;

        //}
        //Debug.Log("ID entrada: "+ posicion);
        Audio_backgroundPlayer audioBack_script = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer;
        singleton.CurrAudioBACK = audioBack_script.GetCurrNameBACK();

        pj_OBJ.transform.position = posicionInicio[posicion].position;
        m_movementPJ.movimientoEntradaStage(singleton.TipoEntrada);
        //setTipoEntrada_function(m_DATA_SINGLETON.m_tipoEntrada);
        m_movementPJ.setTipoEntrada(singleton.TipoEntrada);
        //obtener CurrentPower
        DATA.instance.UpdateCurrentPower_DATA();
        Invoke("activarMovimiento", .9f);
    }
    private void activarMovimiento()=>m_movementPJ.activarMovimiento();
    public void SetIniPosition()
    {
        int childCount = transform.childCount;
        posicionInicio = new Transform[childCount];
        for (int i = 0; i < posicionInicio.Length; i++)
        {
            posicionInicio[i] = transform.GetChild(i).transform;
        }
    }
}
