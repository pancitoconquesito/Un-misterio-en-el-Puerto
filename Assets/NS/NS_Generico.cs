using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NS_Generico : MonoBehaviour, IDamageable
{
    [Header("-- NS --")]
    [SerializeField] private int vidaTotal;
    [SerializeField] private GameObject container;

    private ObjectPooling m_ObjectPooling;
    [SerializeField] private Rigidbody2D m_rigidbody;
    protected bool vivo;
    public NS_Generico()
    {
        vivo = true;
       //m_ObjectPooling = _ObjectPooling;
    }
    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        bool retorno = false;
        if(vivo)
            retorno = recibirDanio(m_dataDanio);
        return retorno;
    }
    public virtual bool recibirDanio(dataDanio m_dataDanio)
    {
        bool retorno = false;
        //print("Yo " + gameObject.name + " recibi danio desde Generico");
        vidaTotal -= m_dataDanio.danio;
        if (vidaTotal < 0)
        {
            morir(m_dataDanio);
            retorno = true;
        }
        //m_rigidbody.velocity = Vector3.zero;
        return retorno;
    }
    public virtual void morir(dataDanio m_dataDanio)
    {
        vivo = false;
        print("acabo de morir!");
       // Destroy(container);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
