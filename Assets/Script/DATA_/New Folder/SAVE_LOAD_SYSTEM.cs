using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAVE_LOAD_SYSTEM : MonoBehaviour
{
    public DATA_GAME m_dataGame;
    private void Awake()
    {
        load_();
    }

    

    [ContextMenu("load")]
    public void load_()
    {
        DATA_GAME dataGame = new DATA_GAME();
        m_dataGame = dataGame.Load_DATA();
        if (m_dataGame == null
            || m_dataGame.m_DATA_ITEMS == null
            || m_dataGame.m_DATA_PROGRESS == null
            )
            erase_();
    }

    [ContextMenu("save")]
    public void save_()
    {
        m_dataGame.Save_DATA(m_dataGame);
    }

    [ContextMenu("erase")]
    public void erase_()
    {
        m_dataGame = null;
        m_dataGame = new DATA_GAME(); 
        m_dataGame.Save_DATA(m_dataGame);
    }


    [ContextMenu("show")]
    public void mostrarData()
    {
        print("valor de valorData_test : " + m_dataGame.m_DATA_TEST.valorData_test);
        print("valor de valorData_test_b : " + m_dataGame.m_DATA_TEST.valorData_test_b);
    }




    [ContextMenu("mod_1")]
    public void modificarVar_1()
    {
        m_dataGame.m_DATA_TEST.valorData_test = 1;
    }

    [ContextMenu("mod_2")]
    public void modificarVar_2()
    {
        m_dataGame.m_DATA_TEST.valorData_test = 2;
    }

    [ContextMenu("mod_3")]
    public void modificarVar_3()
    {
        m_dataGame.m_DATA_TEST.valorData_test = 3;
    }


 


    /***********************************************/
    public void saveItem(int idItem)
    {
        switch (idItem)
        {
            case 0://pika key
                {
                    m_dataGame.m_DATA_ITEMS.key_pika = true;
                    break;
                }
        }

        save_();
    }


    public bool isGenericProgress(GLOBAL_TYPE.TIPO_PREFAB tipoPrefab, int idPrefab)
    {
        bool retorno = false;

        switch (tipoPrefab)
        {
            case GLOBAL_TYPE.TIPO_PREFAB.ITEM:
                {
                    retorno = isItemObtenido(idPrefab);
                    break;
                }
        }
        return retorno;
    }

    public bool isItemObtenido(int idItem)
    {
        bool retorno = false;

        switch (idItem)
        {
            case 0://pika key
                {
                    retorno = m_dataGame.m_DATA_ITEMS.key_pika;
                    break;
                }
        }
        return retorno;
    }
}
