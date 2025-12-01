using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

//[RequireComponent(Collider2D)]
public abstract class InterationBase : MonoBehaviour
{
    public enum States
    {
        Exit = 0,
        Stay = 1,
        Enter = 2,
    }
    [SerializeField][Tag] private string tag_pj;
    [SerializeField] List<GLOBAL_TYPE.ESTADOS> m_estadosPermitidos;
    [ShowNonSerializedField]States m_States;


    Animator m_animPj;
    Ui_Anim anim_ui;
    movementPJ m_movementPJ;
    NewControls m_controls;

    private void Awake()
    {
        m_States = States.Exit;
    }

    private void Start()
    {
        setControls(true);
        anim_ui = MASTER_REFERENCE.instance.Ui_Anim;
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
        m_animPj = m_movementPJ.AnimatorPJ;
    }
    private void setControls(bool value)
    {
        if (value)
        {
            if (m_controls == null)
            {
                m_controls = new NewControls();
                m_controls.Enable();
                m_controls.CONVERSACION.Enter.started += _ => EnterInteracion();
            }
        }
        else
        {
            if (m_controls != null)
            {
                m_controls.CONVERSACION.Enter.started -= _ => EnterInteracion();
                m_controls.Disable();
                m_controls = null;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag_pj) && m_States != States.Enter)
        {
            m_States = States.Stay;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag_pj))
        {
            m_States = States.Exit;
        }
    }

    void EnterInteracion()
    {
        if(m_States == States.Stay)
        {
            //verificar lista valida para interaccion
            bool estadosValidados = false;
            GLOBAL_TYPE.ESTADOS curr_state = m_movementPJ.GetState();
            foreach (GLOBAL_TYPE.ESTADOS item in m_estadosPermitidos)
            {
                if (item == curr_state)
                {
                    estadosValidados = true;
                    break;
                }
            }
            if (!estadosValidados)
            {
                Debug.Log($"Estado actual de PJ no permitido para la interaccion | estadoActualPJ: {curr_state}");
                return;
            }
            m_States = States.Enter;
            anim_ui.DesaparecerUI();
            //m_movementPJ cambiar estado
            m_movementPJ.SetState(GLOBAL_TYPE.ESTADOS.InteraccionGenerica);
            //detener al PJ
            //set anim PJ: anim

            StartInteraction();
        }
    }
    protected abstract void StartInteraction();
    protected void TerminarInteracion()
    {
        m_animPj.SetTrigger("End_Interaction");
        anim_ui.AparecerUI();
        if (m_States != States.Exit)
        {
            m_States = States.Stay;
        }

    }

}
