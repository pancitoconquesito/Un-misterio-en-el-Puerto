using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSword : MonoBehaviour
{
    [SerializeField] private float limiteAxis;
    [SerializeField] private Animator m_aniamtorPJ;
    public void startSword( float valorY)
    {
        /*
        switch (direccion)
        {
            case GLOBAL_TYPE.direccionShootEspada.arriba:
            {
                if (ground) m_aniamtorPJ.SetTrigger("sword_g_arriba");
                else m_aniamtorPJ.SetTrigger("sword_ng_arriba");
                break;
            }
            case GLOBAL_TYPE.direccionShootEspada.frontal:
            {
                if (ground) m_aniamtorPJ.SetTrigger("sword_g_frontal");
                else m_aniamtorPJ.SetTrigger("sword_ng_frontal");
                break;
            }
            case GLOBAL_TYPE.direccionShootEspada.abajo:
            {
                if (ground) m_aniamtorPJ.SetTrigger("sword_g_abajo");
                else m_aniamtorPJ.SetTrigger("sword_ng_abajo");
                break;
            }
        }
        */
        /*
        if(valorY < -limiteAxis)
        {
            //abajo
        }else if(valorY> limiteAxis)
        {
            //arriba
        }
        else
        {
            //frontal
        }
        */
    }
}
