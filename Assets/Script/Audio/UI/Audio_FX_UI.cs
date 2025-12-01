using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Audio_FX_UI 
{
    static Audio_FX_BASE m_base;
    //Inventario
    static Sound_FX_BANK.Sound_FX_UI UI_start;
    static Sound_FX_BANK.Sound_FX_UI UI_exit;
    static Sound_FX_BANK.Sound_FX_UI UI_select;
    static Sound_FX_BANK.Sound_FX_UI UI_moveIn;
    static Sound_FX_BANK.Sound_FX_UI UI_changePanel;

    //Mapa
    static Sound_FX_BANK.Sound_FX_UI UI_MAPA_enter;
    static Sound_FX_BANK.Sound_FX_UI UI_MAPA_exit;
    static Sound_FX_BANK.Sound_FX_UI UI_MAPA_moving;
    static Sound_FX_BANK.Sound_FX_UI UI_MAPA_ZoomIn;
    static Sound_FX_BANK.Sound_FX_UI UI_MAPA_ZoomOut;


    public static void Configure(SO_Audio_FX_UI config, string subFolder)
    {
        m_base = new Audio_FX_BASE(subFolder);
        m_base.UpdateStaticFields(config, typeof(Audio_FX_UI));
    }

    public static void PlaySound(Sound_FX_BANK.Sound_FX_UI key)
    {
        m_base.TriggerPlay(key);
    }

}
