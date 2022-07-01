using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATA : MonoBehaviour
{
    public static DATA instance;

    [SerializeField] private testIdiomaSAVE_LOAD idioma_data;
    [SerializeField] private ContextoSingleton contexto;
    public SAVE_LOAD_SYSTEM save_load_system;
    private int indiceSiguientePosicion;

    private void Awake()
    {
        instance = this;
    }
    public void setIndiceSiguientePosicion(int valor)
    {
        indiceSiguientePosicion = valor;
        print("ahora vale : "+indiceSiguientePosicion);
    }
    public int getIndiceSiguientePosicion()
    {
        return indiceSiguientePosicion;
    }

    public GLOBAL_TYPE.IDIOMA getIdioma(){
        return idioma_data.getIdioma();
    }
    public int getVidaActual()
    {
        return contexto.cantidadVidaPJ;
    }
    void Start()
    {
        
    }
    public void updateVidaPJ(int valor)
    {
        contexto.actualizarVida(valor);
    }

}
