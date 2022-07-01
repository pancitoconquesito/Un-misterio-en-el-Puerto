using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NS_Simple : NS_Generico
{
    //[Header("-- NS-Simple --")]
    //[SerializeField] private int varSimple;
    [SerializeField] private Animator m_animator;
    [SerializeField] private NS_mov_IZDER m_NS_mov_IZDER;
    [SerializeField] private NS_Mov_Volador m_NS_Mov_Volador;


    [SerializeField] private GameObject go_prefab_p_muerte;
    [SerializeField] private ObjectPooling m_objectPoolingDolorAtacanteNS;
    [SerializeField] private float delayParticulas;

    [SerializeField] private float cadenciaRecibirDanio;
    private float currentCadenciaRecibirDanio=0;
    public NS_Simple() : base()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCadenciaRecibirDanio -= Time.deltaTime;
    }
    public override bool recibirDanio(dataDanio m_dataDanio)
    {
        if(currentCadenciaRecibirDanio < 0)
        {
            currentCadenciaRecibirDanio = cadenciaRecibirDanio;

            if (base.recibirDanio(m_dataDanio)) { 
                m_animator.SetTrigger("morir");
                Invoke("mostrarPArticulas",delayParticulas);
                if (m_NS_Mov_Volador != null) m_NS_Mov_Volador.morir();
                else if (m_NS_mov_IZDER != null) m_NS_mov_IZDER.morir();
                return true;
            }
            else
            {


                if (m_NS_Mov_Volador!=null) m_NS_Mov_Volador.recibirDanio(m_dataDanio);
                else
                    if (m_NS_mov_IZDER != null) m_NS_mov_IZDER.recibirDanio(m_dataDanio);
            }
            return true;
        }
        return false;
    }
    private void mostrarPArticulas()
    {
        Instantiate(go_prefab_p_muerte, transform.position, Quaternion.identity);
    }
}
