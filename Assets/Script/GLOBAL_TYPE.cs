using System;
using System.Collections.Generic;
using UnityEngine;

public static class GLOBAL_TYPE 
{
    public enum DIRECCIONES_4
    {
        left,right,up,down
    }
    public enum LADO
    {
        iz,
        der
    }
    public enum ESTADOS
    {
        movementNormal,
        dash,
        sword,
        magnesis,
        inventario,
        danio,

        interactuar,

        muerto,
        herida,
        entrandoScene,

        cogerItem,
        //swordStart, swordHold, swordRelease

        POWER_Disparo,
        POWER_Bomba,
        POWER_Teletransportacion,
        POWER_Inmersoin,
        POWER_Quinto,

        Stair,
        CINEMATICA,

        InteraccionGenerica,
        Mapa,

        onWall,
        jumpWall,
    }

    public enum SUPER_ESTADO
    {
        NORMAL,
        NADANDO
    }


    public enum direccionShootEspada
    {
        arriba,
        frontal,
        abajo,
    }

    public enum TIPO_ENTRADA
    {
        nada, comenzarGameplay, iz_caminando, der_caminando, iz_cayendo, der_cayendo, iz_salto, der_salto, CAYENDO, SALTANDO,
        Inmersion
    }

    public enum TIPO_DANIO
    {
        normal, lava, vacio
    }

    public enum TIPO_PREFAB
    {
        ITEM
    }
    public static bool CanMapa(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal;
    }
    public static bool canDash(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal;
    }

    public static bool canChangeMirada(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal || currentEstado == GLOBAL_TYPE.ESTADOS.Stair;
    }

    public static bool canFall(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado != GLOBAL_TYPE.ESTADOS.dash;
    }

    public static bool canShoot(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal;
    }
    public static bool canInventario(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal ||
            (currentEstado != GLOBAL_TYPE.ESTADOS.interactuar && currentEstado != GLOBAL_TYPE.ESTADOS.Mapa)
            && currentEstado != GLOBAL_TYPE.ESTADOS.POWER_Disparo
            ;
        //return currentEstado != GLOBAL_TYPE.ESTADOS.muerto
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.herida
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.danio
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.magnesis
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.interactuar
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.cogerItem
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.Stair
        //    && currentEstado != GLOBAL_TYPE.ESTADOS.CINEMATICA;
    }
    public enum tipo_globo
    {
        hablandoNormal_IZ,
        hablandoNormal_DER,
        pensando_IZ,
        pensando_DER,
        leyendo
    }
    public enum anim_sp_EMOCIONES
    {
        normal,
        enojada,
        triste,
        leyendo,
        asombrado,
        preocupado,
        pensando
    }

    public enum anim_sp_NPC
    {
        testNormal,//
        cientifico,
        protector_corazon,
        alfonso,//
        andrea,//
        cuadro_A
    }

    public enum nombreNPC
    {
        PJ,
        testNPC,
        vacio,
        Alfonso,
        cientifico,
        protector_corazon,
        andrea,
        Valparaíso
    }

    public enum Tags_CONVERSACIONES
    {
        TEST, TEST_2
    }

    public static Color dialogoHablado=new Color32(255,255,255,255), dialogoSilenciado= new Color32(170, 170, 180, 200);

    public static string getNameNPC(GLOBAL_TYPE.nombreNPC nombre)
    {
        switch (nombre)
        {
            case GLOBAL_TYPE.nombreNPC.PJ: { return "Violeta"; }
            case GLOBAL_TYPE.nombreNPC.Alfonso: { return "Alfonso"; }
            case GLOBAL_TYPE.nombreNPC.andrea: { return "Andrea"; }
            case GLOBAL_TYPE.nombreNPC.cientifico: { return "Dr. Barba Rosa"; }
            case GLOBAL_TYPE.nombreNPC.protector_corazon: { return "Mago del Norte"; }
            case GLOBAL_TYPE.nombreNPC.testNPC: { return "Teodora Clay"; }
            case GLOBAL_TYPE.nombreNPC.Valparaíso: { return "Valparaíso 1906"; }
        }

        return "I AM ERROR.";
    }
    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    public enum IDIOMA
    {
        espanol, ingles
    }
    public static int parseIdioma(IDIOMA idioma)
    {
        int retorno = 0;
        switch (idioma)
        {
            case IDIOMA.espanol: { retorno = 0; break; }
            case IDIOMA.ingles: { retorno = 1;break; }
        }
        return retorno;
    }

    internal static int GetLayerAnim(SUPER_ESTADO estado)
    {
        int retorno=0;
        switch (estado)
        {
            case SUPER_ESTADO.NORMAL: { retorno = 0; break; }
            case SUPER_ESTADO.NADANDO: { retorno = 1; break; }
        }
        return retorno;
    }


    //15, 70
    static string colorSombraPJ_azulOscuro_color_ini_midd = "#090522";
    static string colorSombraPJ_azulOscuro_color_lejos = "#050311";
    static float colorSombraPJ_azulOscuro_alpha_lejos = 0.7f;
    static float colorSombraPJ_azulOscuro_alpha_ini_midd = 0.15f;


    private static Color HexToColor(string hex, float alpha = 1f)
    {
        hex = hex.Replace("#", "");
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        Color color = new Color32(r, g, b, 255);
        color.a = Mathf.Clamp01(alpha); // asegurar que quede entre 0 y 1
        return color;
    }
    public enum TipoSombra
    {
        none,
        azulOscuro,
        menu
    }
    public enum Sombra
    {
        ini, lejos
    }
    public static Color GetColor_sombra(TipoSombra tipoSombra, Sombra sombra)
    {
        switch (tipoSombra)
        {
            case TipoSombra.none:
                {
                    return HexToColor(colorSombraPJ_azulOscuro_color_ini_midd, 0);
                    break;
                }
            case TipoSombra.azulOscuro:
                {
                    return (sombra == Sombra.ini) ?
                        HexToColor(colorSombraPJ_azulOscuro_color_ini_midd, colorSombraPJ_azulOscuro_alpha_ini_midd) :
                        HexToColor(colorSombraPJ_azulOscuro_color_lejos, colorSombraPJ_azulOscuro_alpha_lejos);
                    break;
                }
            default:
                {
                    return HexToColor(colorSombraPJ_azulOscuro_color_ini_midd, 0);
                    break;
                }
        }
    }

    public static List<Color> lista_Colores_A = new List<Color>
    {
        new Color(1f, 0f, 0f),        // Rojo
        new Color(1f, 0.5f, 0f),      // Naranja
        new Color(1f, 1f, 0f),        // Amarillo
        new Color(0.5f, 1f, 0f),      // Lima
        new Color(0f, 1f, 0f),        // Verde
        new Color(0f, 1f, 0.5f),      // Verde-azulado
        new Color(0f, 1f, 1f),        // Cyan
        new Color(0f, 0.5f, 1f),      // Azul claro
        new Color(0f, 0f, 1f),        // Azul
        new Color(0.5f, 0f, 1f),      // Violeta
        new Color(1f, 0f, 1f),        // Magenta
        new Color(1f, 0f, 0.5f)       // Rosa fuerte
    };

    public static List<Color> lista_Colores_B = new List<Color>
    {
        new Color(0f, 1f, 1f),        // Cyan
        new Color(1f, 0.5f, 0f),      // Naranja
        new Color(0f, 0f, 1f),        // Azul
        new Color(1f, 0f, 1f),        // Magenta
        new Color(0.5f, 1f, 0f),      // Lima
        new Color(1f, 1f, 0f),        // Amarillo
        new Color(0f, 1f, 0.5f),      // Verde-azulado
        new Color(1f, 0f, 0.5f),      // Rosa fuerte
        new Color(0.5f, 0f, 1f),      // Violeta
        new Color(0f, 1f, 0f),        // Verde
        new Color(1f, 0f, 0f),        // Rojo 
        new Color(0f, 0.5f, 1f)       // Azul claro
    };

}
