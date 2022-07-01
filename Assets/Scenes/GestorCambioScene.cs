using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CambiarScene))]
[RequireComponent(typeof(SpriteRenderer))]
public class GestorCambioScene : MonoBehaviour
{
    [SerializeField] private BoxCollider2D mBoxCollider2D;
    [SerializeField] private CambiarScene m_cambiarScene;
    [SerializeField] private int IndiceSiguientePosicion;
    [SerializeField] private GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;
    [SerializeField] private Animator m_animatorTransicion;
    private bool completado = false;
    private movementPJ m_movementPJ;
    private changeMirada m_changeMirada;
    // Start is called before the first frame update
    void Start()
    {
        m_movementPJ = GameObject.FindGameObjectWithTag("Player").GetComponent<movementPJ>();
        m_changeMirada = GameObject.FindGameObjectWithTag("Player").GetComponent<changeMirada>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!completado && collision.CompareTag("Player") && m_movementPJ.test_getEstado()!=GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            completado = true;
            DATA_SINGLETON m_dataSingleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
            m_dataSingleton.id_entrada_siguienteEtapa = IndiceSiguientePosicion;
            m_dataSingleton.m_tipoEntrada = m_tipoEntrada;

            switch (m_tipoEntrada)
            {
                case GLOBAL_TYPE.TIPO_ENTRADA.CAYENDO:
                    {
                        if (m_changeMirada.getMirada() == GLOBAL_TYPE.LADO.iz)
                        {
                            m_dataSingleton.m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo;
                            m_tipoEntrada= GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo;
                        }
                        else
                        {
                            m_dataSingleton.m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo;
                            m_tipoEntrada= GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo;
                        }
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.SALTANDO:
                    {
                        if (m_changeMirada.getMirada() == GLOBAL_TYPE.LADO.iz)
                        {
                            m_dataSingleton.m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_salto;
                            m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_salto;
                        }
                        else
                        {
                            m_dataSingleton.m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_salto;
                            m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_salto;
                        }
                        break;
                    }
            }
            m_animatorTransicion.SetTrigger("finishStage");
            m_movementPJ.setTipoEntrada(m_tipoEntrada);

            Invoke("aaa",1f);
        }
    }
    private void aaa()
    {
        m_cambiarScene.changeScene();
        
    }
}
