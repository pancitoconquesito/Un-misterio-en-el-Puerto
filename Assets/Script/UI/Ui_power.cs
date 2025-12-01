using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ui_power : MonoBehaviour
{
    public Image m_img_l, m_img_r,m_img_center;
    public Sprite[] L_AllspritesPowers;
    

    //public void updateListaPoderes(DATA_PJ _dataPJ)
    //{
    //    D_AllSprites = new Dictionary<string, Sprite>();
    //    for (int i = 0; i <= _dataPJ.totalPowers; i++)
    //    {
    //        D_AllSprites.Add(_dataPJ.GetPoder_Name(i), L_AllspritesPowers[i]);
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        //updateListaPoderes(DATA.instance.save_load_system.m_dataGame.m_DATA_PJ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //internal void UpdateUi(DATA_PJ _dataPJ, int select, PowerManager.ShiftPower shift)
    //{
    //    switch (_dataPJ.totalPowers)
    //    {
    //        case 0: { update_cero(_dataPJ, select, shift); break; }
    //        case 1: { update_uno(_dataPJ, select, shift); break; }
    //        case 2: { update_dos(_dataPJ, select, shift); break; }
    //        case 3: { update_tres(_dataPJ, select, shift); break; }
    //    }
    //}
    //void update_cero(DATA_PJ _dataPJ, int select, PowerManager.ShiftPower shift)
    //{
    //    Sprite sprite_null = D_AllSprites[_dataPJ.GetPoder_string(0)];

    //    m_img_l.sprite = sprite_null;
    //    m_img_center.sprite = sprite_null;
    //    m_img_r.sprite = sprite_null;
    //}
    //void update_uno(DATA_PJ _dataPJ, int select, PowerManager.ShiftPower shift)
    //{
    //    Debug.Log($"Uno! select:{select}");
    //    Sprite sprite_unique = D_AllSprites[_dataPJ.GetPoder_string(1)];

    //    m_img_l.sprite = sprite_unique;
    //    m_img_center.sprite = sprite_unique;
    //    m_img_r.sprite = sprite_unique;
    //}
    //void update_dos(DATA_PJ _dataPJ, int select, PowerManager.ShiftPower shift)
    //{
    //    Debug.Log($"Dos! select:{select}");
    //    Sprite sprite_unique = D_AllSprites[_dataPJ.GetPoder_string(1)];
    //    Sprite sprite_other = D_AllSprites[_dataPJ.GetPoder_string(2)];

    //    if (select == 0)
    //    {
    //        m_img_l.sprite = sprite_other;
    //        m_img_center.sprite = sprite_other;
    //        m_img_r.sprite = sprite_other;
    //    }
    //    else
    //    {
    //        m_img_l.sprite = sprite_unique;
    //        m_img_center.sprite = sprite_unique;
    //        m_img_r.sprite = sprite_unique;
    //    }
    //}

    internal void UpdateUi(List<string> listaPoderes_ANT, List<string> listaPoderes_NTX, PowerManager.ShiftPower shift)
    {
        //Debug.Log("listaPoderes_NTX[1](Selected): " + listaPoderes_NTX[1]);
        m_img_l.sprite = GetIntSpriteByName(listaPoderes_NTX[0]);
        m_img_center.sprite = GetIntSpriteByName(listaPoderes_NTX[1]);
        m_img_r.sprite = GetIntSpriteByName(listaPoderes_NTX[2]);
        //Debug.Log("Se busca: "+ listaPoderes_NTX[1]);
        ////update contextData
        //DATA.instance.save_load_system.m_dataGame.m_DATA_PJ.currentPower = listaPoderes_NTX[0];
    }
    internal Sprite GetIntSpriteByName(string v)
    {
        int indexByName = DATA.instance.save_load_system.DataGame.DATA_PJ.GetIndexByName(v);
        return L_AllspritesPowers[indexByName];
    }

    //Dictionary<string, Sprite> D_AllSprites;
    //Sprite sp_null, sp_disparo, sp_desdoblar, sp_bomba;
    //void update_tres(DATA_PJ _dataPJ, int select, PowerManager.ShiftPower shift)
    //{
    //    Debug.Log($"Tres! select:{select}");

    //    Sprite sprite_1 = D_AllSprites[_dataPJ.GetPoder_string(1)];
    //    Sprite sprite_2 = D_AllSprites[_dataPJ.GetPoder_string(2)];
    //    Sprite sprite_3 = D_AllSprites[_dataPJ.GetPoder_string(3)];

        

    //    //si sleect == 1
    //    if (select == 1)
    //    {
    //        m_img_l.sprite = sprite_1;
    //        m_img_center.sprite = sprite_2;
    //        m_img_r.sprite = sprite_3;
    //    }
    //    else if (select == 2)
    //    {
    //        m_img_l.sprite = sprite_1;
    //        m_img_center.sprite = sprite_2;
    //        m_img_r.sprite = sprite_3;
    //    }
    //    else
    //    {
    //        m_img_l.sprite = sprite_1;
    //        m_img_center.sprite = sprite_2;
    //        m_img_r.sprite = sprite_3;
    //    }
    //    //si sleect == 2
    //    //si sleect == 3
    //}
}
