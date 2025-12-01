using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio_FX_PJ
{
    static Audio_FX_BASE m_base;
    //basicos
    static Sound_FX_BANK.Sound_FX_Names m_salto_start;
    static Sound_FX_BANK.Sound_FX_Names m_salto_cancel;
    static Sound_FX_BANK.Sound_FX_Names m_dash;
    static Sound_FX_BANK.Sound_FX_Names m_attack;
    static Sound_FX_BANK.Sound_FX_Names m_attack_col;
    static Sound_FX_BANK.Sound_FX_Names m_landed;
    static Sound_FX_BANK.Sound_FX_Names m_danio;
    static Sound_FX_BANK.Sound_FX_Names m_death;
    static List<Sound_FX_BANK.Sound_FX_Names> m_l_pisadas = new List<Sound_FX_BANK.Sound_FX_Names>(); 
    
    //poderes
    static Sound_FX_BANK.Sound_FX_Names m_disparo;

    public static void Configure(SO_Audio_FX_PJ config, string subFolder)
    {
        m_base = new Audio_FX_BASE(subFolder);
        m_base.UpdateStaticFields(config, typeof(SO_Audio_FX_PJ));

        m_l_pisadas = config.PJ_l_pisadas;
    }
    public static void PlaySound(Sound_FX_BANK.Sound_FX_Names key)
    {
        m_base.TriggerPlay(key);
    }
    public static void Play_R_List() => m_base.R_TriggerPlay(m_l_pisadas);


}
