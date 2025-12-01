using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextoSingleton : MonoBehaviour
{
    public int cantidadVidaPJ;

    public int currentPjPower;
    public int totalPowers;

    public int CurrentPjPower { get => currentPjPower; set => currentPjPower = value; }

    //public int curr_poderIndex;
    private void Awake()
    {
        cantidadVidaPJ = 3;//!
    }

    public void actualizarVida(int valor)
    {
        cantidadVidaPJ = valor;
    }

    internal void UpdateCurrentPower(DATA_PJ _dataPJ)
    {
        currentPjPower = _dataPJ.CurrentPower;
        totalPowers = _dataPJ.TotalPowers;
    }
}
