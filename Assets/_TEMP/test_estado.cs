using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class test_estado : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private movementPJ m_movementPJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "Estado Actual : [ "+parseEstado(m_movementPJ.test_getEstado())+" ]";
    }

    private string parseEstado(GLOBAL_TYPE.ESTADOS estado)
    {
        string retorno = "";
        switch (estado)
        {
            case GLOBAL_TYPE.ESTADOS.movementNormal:
                {
                    retorno = "normalMovement";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.dash:
                {
                    retorno = "dash";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.sword:
                {
                    retorno = "sword";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.magnesis:
                {
                    retorno = "Magnesis";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.inventario:
                {
                    retorno = "inventario";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.danio:
                {
                    retorno = "danio";
                    break;
                }

            case GLOBAL_TYPE.ESTADOS.interactuar:
                {
                    retorno = "conversación";
                    break;
                }

            case GLOBAL_TYPE.ESTADOS.muerto:
                {
                    retorno = "muerto";
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.herida:
                {
                    retorno = "herida";
                    break;
                }
        }
        return retorno;
    }
}
