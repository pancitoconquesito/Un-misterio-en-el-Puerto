using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAVE_LOAD_SYSTEM : MonoBehaviour
{
    DATA_GAME m_dataGame;
    public DATA_GAME DataGame { get => m_dataGame; set => m_dataGame = value; }

    private void Awake()
    {
        load_();
    }

    

    //[ContextMenu("load")]
    public void load_()
    {
        DATA_GAME dataGame = new DATA_GAME();
        DataGame = dataGame.Load_DATA();
        if (DataGame == null
            || DataGame.DATA_ITEMS == null
            || DataGame.DATA_PROGRESS == null
            || DataGame.DATA_OBJ_Persistentes == null
            )
            erase_();
        print("------------------------------ Data Cargada");
    }

    //[ContextMenu("save")]
    public void save_()
    {
        DataGame.Save_DATA(DataGame);
        //print("Data Guardada");
    }
    public void save_(DATA_GAME dt)
    {
        DataGame = dt;
        DataGame.Save_DATA(DataGame);
        //print("Data Guardada");
    }
    [ContextMenu("ERASE")]
    public void erase_()
    {
        DataGame = null;
        DataGame = new DATA_GAME(); 
        DataGame.Save_DATA(DataGame);
        print("Data Borrada");
    }


    [ContextMenu("show")]
    public void mostrarData()
    {
        print("valor de valorData_test : " + DataGame.DATA_TEST.valorData_test);
        print("valor de valorData_test_b : " + DataGame.DATA_TEST.valorData_test_b);
    }




    //[ContextMenu("mod_1")]
    //public void modificarVar_1()
    //{
    //    DataGame.DATA_TEST.valorData_test = 1;
    //}

    //[ContextMenu("mod_2")]
    //public void modificarVar_2()
    //{
    //    DataGame.DATA_TEST.valorData_test = 2;
    //}

    //[ContextMenu("mod_3")]
    //public void modificarVar_3()
    //{
    //    DataGame.DATA_TEST.valorData_test = 3;
    //}


 


    /***********************************************/
    public void SaveItem(string key, bool value)
    {
        m_dataGame.DATA_ITEMS.Changevalue(key, value);
        //item.Changevalue(m_item.ToString(), true);
        //switch (idItem)
        //{
        //    case 0://pika key
        //        {
        //            DataGame.DATA_ITEMS.Key_pika = true;
        //            break;
        //        }
        //}

        save_();
    }


    public bool GetItemProgress(DATA_ITEMS.ITEMS item)
    {
        return m_dataGame.DATA_ITEMS.GetValueByKey(item.ToString());
        //switch (tipoPrefab)
        //{
        //    case GLOBAL_TYPE.TIPO_PREFAB.ITEM:
        //        {
        //            retorno = isItemObtenido(idPrefab);
        //            break;
        //        }
        //}
        //return retorno;
    }

    public bool isItemObtenido(int idItem)
    {
        bool retorno = false;

        switch (idItem)
        {
            case 0://pika key
                {
                    retorno = DataGame.DATA_ITEMS.Key_pika;
                    break;
                }
        }
        return retorno;
    }
}
