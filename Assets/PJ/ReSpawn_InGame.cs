using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ReSpawn_InGame : MonoBehaviour
{
    GameObject GO_PJ;
    Ui_Anim m_Ui_Anim;
    movementPJ movementPJ;
    vida_PJ m_vida_PJ;
    CameraController m_CameraController;
    Animator at_transiciones;

    // Start is called before the first frame update
    void Start()
    {
        MASTER_REFERENCE m_ref = MASTER_REFERENCE.instance;
        GO_PJ = m_ref.GO_PJ;
        m_Ui_Anim = m_ref.Ui_Anim;
        movementPJ = m_ref.MovementPJ;
        m_vida_PJ = m_ref.VidaPJ;
        m_CameraController = m_ref.CameraController;
        at_transiciones = m_ref.UI_Context.At_transiciones;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool respawneando = false;
    public void RespawnearPJ(dataDanio m_dataDanio, vida_PJ vidaScript)
    {
        StartCoroutine(CoureotineRespawnearPJ(m_dataDanio, vidaScript));
    }

    IEnumerator CoureotineRespawnearPJ(dataDanio m_dataDanio, vida_PJ vidaScript)
    {
        if (!respawneando)
        {
            respawneando = true;
            m_Ui_Anim.DesaparecerUI();
            movementPJ.SetState(GLOBAL_TYPE.ESTADOS.CINEMATICA);
            movementPJ.StopMovement();
            m_vida_PJ.PuedeRecibirDanio = false;
            m_CameraController.ResetCamPivote();
            at_transiciones.SetTrigger("finishStage");
        
            yield return new WaitForSeconds(0.8f);
        
            GO_PJ.transform.position = m_dataDanio.PositionCollision;
            m_CameraController.ReactiveCamera();
        
            yield return new WaitForSeconds(0.5f);
        
            at_transiciones.SetTrigger("start");
        
            yield return new WaitForSeconds(0.8f);
            m_Ui_Anim.AparecerUI();


            //if vivo
            movementPJ.SetState(GLOBAL_TYPE.ESTADOS.movementNormal);
            m_vida_PJ.PuedeRecibirDanio = true;
            m_dataDanio.tipo_danio = GLOBAL_TYPE.TIPO_DANIO.normal;
            vidaScript.RecibirDanio_I(m_dataDanio);
            //-> morir
            yield return null;
            m_dataDanio.tipo_danio = GLOBAL_TYPE.TIPO_DANIO.vacio;
            respawneando = false;
        }
    }

}
