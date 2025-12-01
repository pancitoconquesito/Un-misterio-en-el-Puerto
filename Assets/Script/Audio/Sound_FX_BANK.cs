using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sound_FX_BANK 
{
    public enum Sound_FX_Names
    {
        NONE = 0,

        PJ_dash ,
        PJ_salto_start ,
        PJ_salto_cancel,
        PJ_landed_soft,
        PJ_attack_start,
        PJ_attack_colision,
        PJ_danio ,
        PJ_death ,
        
        //poderes
        PJ_disparo ,

        //pisadas
        PJ_l_pisadas_1 ,
        PJ_l_pisadas_2 ,
        PJ_l_pisadas_3 ,
        PJ_l_pisadas_4 ,
        PJ_l_pisadas_5 ,
        PJ_l_pisadas_6 ,
        PJ_l_pisadas_7 ,
        PJ_l_pisadas_8 ,
        PJ_l_pisadas_9 ,
        PJ_l_pisadas_10 ,
        PJ_l_pisadas_11 ,
        PJ_l_pisadas_12,

    }
    public enum Sound_FX_UI
    {
        NONE = 0,
        UI_start,
        UI_exit,
        UI_moveIn,
        UI_changePanel,//anim
        UI_select,


        UI_MAPA_enter,
        UI_MAPA_exit,
        UI_MAPA_moving,
        UI_MAPA_ZoomIn,
        UI_MAPA_ZoomOut,
    }
    public enum Sound_FX_Actions
    {
        NONE = 0,
        Actions_Conversacion_start,
        Actions_Conversacion_exit,
        Actions_Conversacion_continue,
        Actions_Conversacion_endParrafo,
        Actions_Conversacion_endConversacion,

        //sonidos
        sonidoConversacion_test,
    }
    public static string GetNameAudio<T>(T fxName) where T : Enum
    {
        if (!fxName.Equals(default(T)))
        {
            return fxName.ToString();
        }
        else
        {
            return "_default";
        }
    }

}
