using UnityEngine;

public static class GLOBAL_TYPE 
{
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
    }

    public enum direccionShootEspada
    {
        arriba,
        frontal,
        abajo,
    }

    public enum TIPO_ENTRADA
    {
        nada, comenzarGameplay, iz_caminando, der_caminando, iz_cayendo, der_cayendo, iz_salto, der_salto, CAYENDO, SALTANDO
    }

    public enum TIPO_DANIO
    {
        normal, lava
    }

    public enum TIPO_PREFAB
    {
        ITEM
    }
    public static bool canDash(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal;
    }

    public static bool canChangeMirada(GLOBAL_TYPE.ESTADOS currentEstado)
    {
        return currentEstado == GLOBAL_TYPE.ESTADOS.movementNormal;
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
        return currentEstado != GLOBAL_TYPE.ESTADOS.muerto 
            && currentEstado != GLOBAL_TYPE.ESTADOS.magnesis 
            && currentEstado != GLOBAL_TYPE.ESTADOS.interactuar 
            && currentEstado != GLOBAL_TYPE.ESTADOS.cogerItem;
    }
    public enum tipo_globo
    {
        hablandoNormal_IZ, hablandoNormal_DER,
        pensando_IZ, pensando_DER,
        leyendo
    }
    public enum anim_sp_PJ
    {
        normal,
        enojada,
        triste,
        leyendo
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
        PJ, testNPC, vacio,
        Alfonso,
        cientifico,
        protector_corazon,
        andrea,
        Valparaíso
    }

    public static Color dialogoHablado=new Color32(255,255,255,255), dialogoSilenciado= new Color32(170, 170, 180, 220);

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
}
