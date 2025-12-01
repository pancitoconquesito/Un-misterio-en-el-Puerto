using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio_FX_Actions 
{
    static Audio_FX_BASE m_base;

    static Sound_FX_BANK.Sound_FX_Actions m_Conversacion_start;
    static Sound_FX_BANK.Sound_FX_Actions m_Conversacion_exit;
    static Sound_FX_BANK.Sound_FX_Actions m_Conversacion_continue;
    static Sound_FX_BANK.Sound_FX_Actions m_Conversacion_endParrafo;
    static Sound_FX_BANK.Sound_FX_Actions m_Conversacion_endConversacion;

    public static void Configure(SO_Audio_FX_Actions config, string subFolder)
    {
        m_base = new Audio_FX_BASE(subFolder);

        m_Conversacion_start = config.Actions_Conversacion_start;
        m_Conversacion_exit = config.Actions_Conversacion_exit;
        m_Conversacion_continue = config.Actions_Conversacion_continue;
        m_Conversacion_endParrafo = config.Actions_Conversacion_endParrafo;
        m_Conversacion_endConversacion = config.Actions_Conversacion_endConversacion;
    }
    public static void Play_Conversacion_Start() => m_base.TriggerPlay(m_Conversacion_start);
    public static void Play_Conversacion_Exit() => m_base.TriggerPlay(m_Conversacion_exit);
    public static void Play_Conversacion_Continue() => m_base.TriggerPlay(m_Conversacion_continue);
    public static void Play_Conversacion_EndParrafo() => m_base.TriggerPlay(m_Conversacion_endParrafo);
    public static void Play_Conversacion_EndConversacion() => m_base.TriggerPlay(m_Conversacion_endConversacion);
    public static void Play_Conversacion_Sonido(string value)=>m_base.PlaySounrByString<Sound_FX_BANK.Sound_FX_Actions>(value);

    //static Sound_FX_BANK.Sound_FX_Actions GetEnumByName(string valor)
    //{
    //    if (Enum.TryParse<Sound_FX_BANK.Sound_FX_Actions>(valor, out Sound_FX_BANK.Sound_FX_Actions resultado))
    //    {
    //        return resultado;
    //    }
    //    else
    //    {
    //        return default(Sound_FX_BANK.Sound_FX_Actions);
    //    }
    //}

}
