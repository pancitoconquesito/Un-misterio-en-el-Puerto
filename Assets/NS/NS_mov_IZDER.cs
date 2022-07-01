using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(changeMirada))]
public class NS_mov_IZDER : MonoBehaviour
{
    [SerializeField] private GLOBAL_TYPE.LADO direccionCaminata;
    [SerializeField] private float velocidad;
    [Range(0,100f)][SerializeField] private float rangoPatrulla;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private float factorRetrocesoAlRecibirDanio;
    private changeMirada m_changeMirada;
    void Start()
    {
        m_changeMirada = GetComponent<changeMirada>();

        direccionCaminata = GLOBAL_TYPE.LADO.iz;
        vec_IZ= new Vector3(transform.position.x - rangoPatrulla, transform.position.y, transform.position.z);
        vec_DER= new Vector3(transform.position.x + rangoPatrulla, transform.position.y, transform.position.z);
    }

    private Vector3 vec_IZ, vec_DER;
    void Update()
    {
        setLado();
        moverse();
    }

    private void setLado()
    {
        if (direccionCaminata == GLOBAL_TYPE.LADO.iz && transform.position.x < vec_IZ.x )
        {
            direccionCaminata = GLOBAL_TYPE.LADO.der;
        }
        if (direccionCaminata == GLOBAL_TYPE.LADO.der && transform.position.x > vec_DER.x)
        {
            direccionCaminata = GLOBAL_TYPE.LADO.iz;
        }
    }
    private Vector3 lado;
    private void FixedUpdate()
    {
        if (!reciboiendoDanio)
            m_rigidbody.velocity = velocidad * lado;
        else
            m_rigidbody.velocity = lado;
    }
    private void moverse()
    {
        if(vivo && !reciboiendoDanio)
        {
            if (direccionCaminata == GLOBAL_TYPE.LADO.iz) lado = Vector3.left;
            else lado = Vector3.right;
            m_changeMirada.miradaPj(lado.x);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - rangoPatrulla, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + rangoPatrulla, transform.position.y, transform.position.z));
    }
    public void morir()
    {
        vivo = false;
        velocidad = 0;
    }
    private bool reciboiendoDanio=false;
    private bool vivo = true;
    public void recibirDanio(dataDanio m_dataDanio)
    {
        //si no esta recibiendo daño ya
        reciboiendoDanio = true;
        Vector3 dirEmpuje = (transform.position - m_dataDanio.m_transformAtacante.position).normalized;

        lado = m_dataDanio.getImpactoEmpuje() * dirEmpuje * factorRetrocesoAlRecibirDanio;
        Invoke("quitarDanio",0.3f);
    }
    private void quitarDanio()
    {
        if(reciboiendoDanio && vivo) reciboiendoDanio = false;
    }
}
