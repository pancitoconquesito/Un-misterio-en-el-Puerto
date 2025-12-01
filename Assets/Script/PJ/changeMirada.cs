using System;
using UnityEngine;

public class changeMirada : MonoBehaviour
{
    [SerializeField] private bool miradaNormalDerecha;
    [SerializeField] private float offsetInput;
    [SerializeField] private GameObject[] objVolteables;

    private Vector3[] lista_tamOriginal;
    private GLOBAL_TYPE.LADO mirada;
    private GLOBAL_TYPE.LADO currentMirada;
    private void Awake()
    {
        lista_tamOriginal = new Vector3[objVolteables.Length];
        for (int i = 0; i < lista_tamOriginal.Length; i++)
        {
            lista_tamOriginal[i] = objVolteables[i].transform.localScale;
        }
        if (miradaNormalDerecha)
        {
            mirada = GLOBAL_TYPE.LADO.der;
            currentMirada = GLOBAL_TYPE.LADO.der;
            mirarDer();
        }
        else
        {
            mirada = GLOBAL_TYPE.LADO.iz;
            currentMirada = GLOBAL_TYPE.LADO.iz;
            mirarIZ();
        }
    }
    public GLOBAL_TYPE.LADO getMirada()
    {
        return mirada;
    }
    public void miradaPj(float valor, bool voltearPJ=true, bool forzarUpdate=false)
    {
        if (valor < 0f- offsetInput) currentMirada = GLOBAL_TYPE.LADO.iz;
        if (valor > 0f+ offsetInput) currentMirada = GLOBAL_TYPE.LADO.der;
        if (currentMirada != mirada || forzarUpdate)
        {
            mirada = currentMirada;
            if (voltearPJ)
            {
                changeMiradaFuncion();
            }
        }
    }
    public void miradaPj(GLOBAL_TYPE.LADO lADO, bool voltearPJ = true, bool forzarUpdate = false)
    {
        currentMirada = lADO;
        if (currentMirada != mirada || forzarUpdate)
        {
            mirada = currentMirada;
            if (voltearPJ)
            {
                changeMiradaFuncion();
            }
        }
    }
    private void changeMiradaFuncion()
    {
        switch (currentMirada)
        {
            case GLOBAL_TYPE.LADO.iz: { mirarIZ(); break; }
            case GLOBAL_TYPE.LADO.der: { mirarDer(); break; }
        }
    }
    private void mirarIZ()
    {
        for (int i = 0; i < objVolteables.Length; i++)
        {
            if (miradaNormalDerecha)
                objVolteables[i].transform.localScale = new Vector3(-lista_tamOriginal[i].x, lista_tamOriginal[i].y, lista_tamOriginal[i].z);
            else
                objVolteables[i].transform.localScale = lista_tamOriginal[i];
        }
    }
    private void mirarDer()
    {
        for (int i = 0; i < objVolteables.Length; i++)
        {
            if(miradaNormalDerecha)
                objVolteables[i].transform.localScale = lista_tamOriginal[i];
            else
                objVolteables[i].transform.localScale = new Vector3(-lista_tamOriginal[i].x, lista_tamOriginal[i].y, lista_tamOriginal[i].z);
        }
    }

    //internal void miradaPjStair(float valorInput_Horizontal)
    //{
    //    throw new NotImplementedException();
    //}
}
