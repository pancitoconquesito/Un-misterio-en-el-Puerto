using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DATA_OBJ_Persistentes
{
    public List<string> l_paredesRompibles;
    public DATA_OBJ_Persistentes()
    {
        l_paredesRompibles = new List<string>();
        l_paredesRompibles.Add("Sub_A[A]:false");
        l_paredesRompibles.Add("Sub_B[A]:false");
        //l_paredesRompibles.Add("Sub_F[A]:false");
    }


    public bool IsInList(string key)
    {
        return l_paredesRompibles.Contains($"{key}:true") || l_paredesRompibles.Contains($"{key}:false");
    }

    public bool GetValueByKey(string key)
    {
        bool b_value = true;
        foreach (var item in l_paredesRompibles)
        {
            if (item.Contains(key))
            {
                string s_value = item.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                if (s_value == "false")
                {
                    b_value = false;
                }
                return b_value;
            }
        }
        return b_value;
    }


    public void Changevalue(string key, bool value)
    {
        string newValue = key+":";
        if (value)
        {
            newValue += "true";
        }
        else
        {
            newValue += "false";
        }
        for (int i = 0; i < l_paredesRompibles.Count; i++)
        {
            if (l_paredesRompibles[i].Contains(key))
            {
                l_paredesRompibles[i] = newValue;
            }
        }
    }
}
