using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DATA_GAME 
{
    public DATA_TEST m_DATA_TEST;
    public DATA_ITEMS m_DATA_ITEMS;
    public DATA_PROGRESS m_DATA_PROGRESS;
    public DATA_GAME()
    {
        m_DATA_TEST = new DATA_TEST();
        m_DATA_ITEMS = new DATA_ITEMS();
        m_DATA_PROGRESS = new DATA_PROGRESS();
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
