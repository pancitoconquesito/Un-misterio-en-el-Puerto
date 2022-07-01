using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cerrarPrograma : MonoBehaviour
{
    public void BTN_CERRAR_APLICACION()
    {
        Application.Quit();
        print("Se cerro");
    }
}
