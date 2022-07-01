using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class test_cantidadSTAMINA : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private staminaPsiquica m_staminaPsiquica;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texto.text= "Cantidad Stamina (%) : "+ GLOBAL_TYPE.Round(m_staminaPsiquica.getCantidadStaminaPorcentaje(), 2) + "%";
    }
}
