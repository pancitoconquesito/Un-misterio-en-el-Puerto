using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DATA_PJ
{
    //vida
    //poderes
    bool haspowers;
    int currentPower;
    int totalPowers;


    public DATA_PJ()
    {
        Haspowers = false;
        TotalPowers = 0;

        CurrentPower = 1;

        //setPoder(totalPowers+1, "1-disparo"); totalPowers+=1;
        //setPoder(totalPowers+1, "2-desdoblar"); totalPowers += 1;
        //setPoder(totalPowers+1, "3-bomba"); totalPowers += 1;


        //AddPower("1-disparo");
        //AddPower("2-desdoblar");
        //AddPower("3-bomba");
        //AddPower("4-inmersion");
    }
    string respPoderes = "";
    public void AddPower(string value)
    {
        if (respPoderes.Contains(value))
        {
            return;
        }
        respPoderes += value;
        TotalPowers += 1;
        switch (TotalPowers)
        {
            case 1:
                {
                    poder_primero = value;
                    break;
                }
            case 2: { poder_segundo = value; break; }
            case 3: { poder_tercero = value; break; }
            case 4: { poder_cuarto = value; break; }
            case 5: { poder_quinto = value; break; }
        }
        //Debug.Log("Total poderes: "+TotalPowers);
    }
    //public void setPoder(int index, string value)
    //{
    //    switch (index)
    //    {
    //        case 1:
    //            {
    //                poder_primero = value;
    //                break;
    //            }
    //        case 2: { poder_segundo = value; break; }
    //        case 3: { poder_tercero = value; break; }
    //        case 4: { poder_cuarto = value; break; }
    //    }
    //}

    public string GetPoder_string(int index)
    {
        string valueReturn = "ERROR";
        //cambiar a diccionario
        switch (index)
        {
            case 0:
                {
                    valueReturn = "null";
                    break;
                }
            case 1: { valueReturn = poder_primero; break; }
            case 2: { valueReturn = poder_segundo; break; }
            case 3: { valueReturn = poder_tercero; break; }
            case 4: { valueReturn = poder_cuarto; break; }
            case 5: { valueReturn = poder_quinto; break; }
        }
        return valueReturn;
    }
    public string GetPoder_Name(int index)
    {
        string valueReturn = "ERROR";
        //cambiar a diccionario
        switch (index)
        {
            case 0:
                {
                    valueReturn = "null";
                    break;
                }
            case 1: { valueReturn = poder_primero; break; }
            case 2: { valueReturn = poder_segundo; break; }
            case 3: { valueReturn = poder_tercero; break; }
            case 4: { valueReturn = poder_cuarto; break; }
            case 5: { valueReturn = poder_quinto; break; }
        }
        return valueReturn;
    }
    public string poder_primero, poder_segundo, poder_tercero, poder_cuarto, poder_quinto;
    public void ShowListaPoderes()
    {
        Debug.Log($"P1: {poder_primero}");
        Debug.Log($"P2: {poder_segundo}");
        Debug.Log($"P3: {poder_tercero}");
        Debug.Log($"P4: {poder_cuarto}");
        Debug.Log($"P5: {poder_quinto}");
    }
    public bool Haspowers { get => haspowers; set => haspowers = value; }
    public int CurrentPower { get => currentPower; set => currentPower = value; }
    public int TotalPowers { get => totalPowers; set => totalPowers = value; }

    //1: disparo
    //2:desdoblar
    //3:bomba
    //4:inmersion
    //5:quinto
    public List<string> GetPowerLString_HARD()
    {
        List<string> ret = new List<string>
        {
            "null",
            "1-disparo",
            "2-desdoblar",
            "3-bomba",
            "4-inmersion",
            "5-quinto",
        };
        return ret;
    }
    public List<string> GetPowerLString_DINAMIC()
    {
        List<string> ret = new List<string>
        {
            "null",
            poder_primero,
            poder_segundo,
            poder_tercero,
            poder_cuarto,
            poder_quinto
        };
        return ret;
    }
    internal int GetIndexByName(string v)
    {
        int valueReturn = 0;
        //cambiar a diccionario
        switch (v)
        {
            case "null":
                {
                    valueReturn = 0;
                    break;
                }
            case "1-disparo": { valueReturn = 1; break; }
            case "2-desdoblar": { valueReturn = 2; break; }
            case "3-bomba": { valueReturn = 3; break; }
            case "4-inmersion": { valueReturn = 4; break; }
            case "5-quinto": { valueReturn = 5; break; }
        }
        return valueReturn;
    }
}
