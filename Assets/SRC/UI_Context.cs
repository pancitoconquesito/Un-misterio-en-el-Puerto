using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Context : MonoBehaviour
{
    [SerializeField] CargaDatos_PROGRESO m_CargaDatos_PROGRESO;
    [SerializeField] ui_corazon m_UI_corazon;
    [SerializeField] Animator m_at_transiciones;
    [SerializeField] MoveMapa m_MoveMapa;
    [SerializeField] GameObject go_UIConversacion;
    [SerializeField] GameObject go_UIMapa;
    [SerializeField] ui_itemObtenido m_ui_itemObtenido;
    //[SerializeField] UI_InputUpdate_Context m_UI_InputUpdate;

    public CargaDatos_PROGRESO CargaDatos_PROGRESO { get => m_CargaDatos_PROGRESO; set => m_CargaDatos_PROGRESO = value; }
    public ui_corazon UI_corazon { get => m_UI_corazon; set => m_UI_corazon = value; }
    public Animator At_transiciones { get => m_at_transiciones; set => m_at_transiciones = value; }
    public MoveMapa MoveMapa { get => m_MoveMapa; set => m_MoveMapa = value; }
    public ui_itemObtenido Ui_itemObtenido { get => m_ui_itemObtenido; set => m_ui_itemObtenido = value; }

    //public UI_InputUpdate_Context UI_InputUpdate { get => m_UI_InputUpdate; set => m_UI_InputUpdate = value; }
    private void Awake()
    {
        go_UIConversacion.SetActive(true);
        go_UIMapa.SetActive(true);
    }
}
