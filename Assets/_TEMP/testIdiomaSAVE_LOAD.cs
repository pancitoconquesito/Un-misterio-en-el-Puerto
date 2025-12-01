using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIdiomaSAVE_LOAD 
{
    private GLOBAL_TYPE.IDIOMA m_idioma;

    public testIdiomaSAVE_LOAD()
    {
        CargarIdioma();
    }
    private void CargarIdioma()
    {
        int valorIdioma = PlayerPrefs.GetInt("idioma",0);
        switch (valorIdioma)
        {
            case 0: { m_idioma = GLOBAL_TYPE.IDIOMA.espanol; break; }
            case 1: { m_idioma = GLOBAL_TYPE.IDIOMA.ingles; break; }
        }
        //print("Idioma actual : "+m_idioma);
    }
    public GLOBAL_TYPE.IDIOMA getIdioma()
    {
        return m_idioma;
    }
    public void setIdioma(int valorIdioma)
    {
        switch (valorIdioma)
        {
            case 0: { m_idioma = GLOBAL_TYPE.IDIOMA.espanol; break; }
            case 1: { m_idioma = GLOBAL_TYPE.IDIOMA.ingles; break; }
        }
    }
    public void saveIdioma()
    {
        PlayerPrefs.SetInt("idioma", GLOBAL_TYPE.parseIdioma(m_idioma));
    }
}
