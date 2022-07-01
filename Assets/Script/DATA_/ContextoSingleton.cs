using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextoSingleton : MonoBehaviour
{
    public int cantidadVidaPJ;
    private void Awake()
    {
        cantidadVidaPJ = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void actualizarVida(int valor)
    {
        cantidadVidaPJ = valor;
    }
}
