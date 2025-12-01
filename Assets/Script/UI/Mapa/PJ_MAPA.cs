using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_MAPA : MonoBehaviour
{
    [SerializeField] float m_cooldown;
    float curr_m_cooldown;
    MoveMapa m_moveMapa;
    Interpolacion_alpha_image inter_alpha;
    Ui_Anim ui_anim;
    void Start()
    {
       ui_anim = MASTER_REFERENCE.instance.Ui_Anim;
       m_moveMapa = MASTER_REFERENCE.instance.UI_Context.MoveMapa;
       inter_alpha = m_moveMapa.AlpahaSprite;
       curr_m_cooldown = m_cooldown;
    }
    public bool CanChangeState()=>curr_m_cooldown < 0;
    private void Update()
    {
        if (curr_m_cooldown > -1)
        {
            curr_m_cooldown -= Time.deltaTime;//!
        }
    }
    public void StartMapa()
    {
        curr_m_cooldown = m_cooldown;
        inter_alpha.InterpolateAlpha_asc();
        m_moveMapa.StartMapa();
        ui_anim.DesaparecerUI();
    }

    public void EndMapa(bool resetUI)
    {
        curr_m_cooldown = m_cooldown;
        inter_alpha.InterpolateAlpha_desc();
        m_moveMapa.EndMapa();
        if (resetUI)
        {
            ui_anim.AparecerUI();
        }
    }
}
