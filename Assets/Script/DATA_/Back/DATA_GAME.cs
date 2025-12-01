using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DATA_GAME 
{
    DATA_TEST m_DATA_TEST;
    DATA_ITEMS m_DATA_ITEMS;
    DATA_PROGRESS m_DATA_PROGRESS;
    DATA_PJ m_DATA_PJ;
    DATA_OBJ_Persistentes m_DATA_OBJ_Persistentes;
    //DataPrefabsConfigurations m_DataPrefabsConfigurations;
    public DATA_TEST DATA_TEST { get => m_DATA_TEST; set => m_DATA_TEST = value; }
    public DATA_ITEMS DATA_ITEMS { get => m_DATA_ITEMS; set => m_DATA_ITEMS = value; }
    public DATA_PROGRESS DATA_PROGRESS { get => m_DATA_PROGRESS; set => m_DATA_PROGRESS = value; }
    public DATA_PJ DATA_PJ { get => m_DATA_PJ; set => m_DATA_PJ = value; }
    public DATA_OBJ_Persistentes DATA_OBJ_Persistentes { get => m_DATA_OBJ_Persistentes; set => m_DATA_OBJ_Persistentes = value; }

    //public DataPrefabsConfigurations DataPrefabsConfigurations { get => m_DataPrefabsConfigurations; set => m_DataPrefabsConfigurations = value; }

    public DATA_GAME()
    {
        DATA_TEST = new DATA_TEST();
        DATA_ITEMS = new DATA_ITEMS();
        DATA_PROGRESS = new DATA_PROGRESS();
        DATA_PJ = new DATA_PJ();
        DATA_OBJ_Persistentes = new DATA_OBJ_Persistentes();
        //DataPrefabsConfigurations = new DataPrefabsConfigurations();
        //Debug.Log("-----------Get Data game");
    }

    public void Save_DATA(DATA_GAME data)
    {
        SAVE_LOAD.SAVE_DATA_GAME(data);
    }
    public DATA_GAME Load_DATA()
    {
        return SAVE_LOAD.LOAD_DATA_GAME();
    }
}
