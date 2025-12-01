using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DATA_ITEMS
{
    bool key_pika;
    public bool Key_pika { get => key_pika; set => key_pika = value; }
    public enum ITEMS
    {
        llave_pika,
        llave_trebol,
        llave_corazon,
        llave_diamante
    }


    public List<string> l_ITEMS;
    public DATA_ITEMS()
    {
        Key_pika = false;



        l_ITEMS = new List<string>();
        foreach (string name in Enum.GetNames(typeof(ITEMS)))
        {
            l_ITEMS.Add($"{name}:false");
        }

    }
    public bool IsInList(string key)
    {
        return l_ITEMS.Contains($"{key}:true") || l_ITEMS.Contains($"{key}:false");
    }

    public bool GetValueByKey(string key)
    {
        bool b_value = true;
        foreach (var item in l_ITEMS)
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
        string newValue = key + ":";
        if (value)
        {
            newValue += "true";
        }
        else
        {
            newValue += "false";
        }
        for (int i = 0; i < l_ITEMS.Count; i++)
        {
            if (l_ITEMS[i].Contains(key))
            {
                l_ITEMS[i] = newValue;
            }
        }
    }
}
