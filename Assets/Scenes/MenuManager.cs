using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private enum ESTADO_
    {
        main, 
        audio,
        opcionB
    }
    private ESTADO_ estados;

    [Header("-- OBJS --")]
    [SerializeField]private GameObject m_Animator_OpecionesAudio;
    [SerializeField] private GameObject m_Opcion_B_OBJ;

    [Header("-- Animators --")]
    [SerializeField] private Animator at_OpecionesAudio;
    [SerializeField] private Animator at_Opeciones_B;

    [Header("-- Cambair scene --")]
    [SerializeField] private CambiarScene m_CambiarScene;
    void Start()
    {
        estados = ESTADO_.main;
    }



    public void BTN_FUNCTION(int valor)
    {
        switch (valor)
        {
            case 0:
                {
                    menuBase();
                    break;
                }
            case 1:
                {
                    subMenuAudio();
                    break;
                }
            case 2:
                {
                    subMenu_B();
                    break;
                }
        }
    }
    private void subMenu_B()
    {
        estados = ESTADO_.opcionB;
        m_Opcion_B_OBJ.SetActive(true);
    }
    private void menuBase()
    {
        estados = ESTADO_.main;
    }
    
    private void subMenuAudio()
    {
        estados = ESTADO_.audio;
        m_Animator_OpecionesAudio.SetActive(true);
    }

    public void regreso()
    {
        limpiarPantalla(estados);
        BTN_FUNCTION(0);
    }

    private void limpiarPantalla(ESTADO_ _estado)
    {
        switch (_estado)
        {
            case ESTADO_.audio:
            {
                at_OpecionesAudio.SetTrigger("exit");
                break;
            }
            case ESTADO_.opcionB:
            {
                at_Opeciones_B.SetTrigger("exit");
                at_Opeciones_B.SetBool("test", true);
                break;
            }
        }
    }
    public void BTN_CambiarSceneFunction(string _name)
    {
        //desactivar cosas
        m_CambiarScene.changeScene(_name);
    }
}
