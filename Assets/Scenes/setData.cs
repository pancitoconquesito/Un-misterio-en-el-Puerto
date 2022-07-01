using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setData : MonoBehaviour
{
    [SerializeField] private Transform[] posicionInicio;
    //private float tiempoInicio = 0;
    private GameObject pj_OBJ;
    private movementPJ m_movementPJ;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pj_OBJ = GameObject.FindGameObjectWithTag("Player");
        m_movementPJ = pj_OBJ.GetComponent<movementPJ>();
        DATA_SINGLETON m_DATA_SINGLETON = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        int posicion = m_DATA_SINGLETON.id_entrada_siguienteEtapa;
        pj_OBJ.transform.position = posicionInicio[posicion].position;

        //setTipoEntrada_function(m_DATA_SINGLETON.m_tipoEntrada);
        m_movementPJ.setTipoEntrada(m_DATA_SINGLETON.m_tipoEntrada);
        Invoke("activarMovimiento", 0.7f);
    }
    private void Update()
    {
        
    }

    private void activarMovimiento()
    {
        m_movementPJ.activarMovimiento();
    }
}
