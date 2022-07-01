using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(changeMirada))]
public class NS_Mov_Volador : MonoBehaviour
{
    private Transform targetPJ;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private float velocidad;
    [SerializeField] private float rangoVision;
    [SerializeField] private NS_mov_IZDER m_NS_mov_IZDER;
    [SerializeField] private float factorRetrocesoRecibirDanio;
    [SerializeField] private Animator m_animator;
    
    private changeMirada m_changeMirada;

    private Vector3 direccion;
    private Vector3 direccionInicial;
    private enum estados
    {
        patrullando, persiguiendo, volviendo, muerto
    }
    private estados m_estados;
    // Start is called before the first frame update
    void Start()
    {
        m_changeMirada = GetComponent<changeMirada>();

        targetPJ = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        direccionInicial = transform.position;
        
        m_estados = estados.patrullando;
        m_NS_mov_IZDER.enabled = true;
    }
    void Update()
    {
        if (!recibiendoDanio)
        {
            switch (m_estados)
            {
                case estados.patrullando:
                    {
                        revisarPJDistancia();
                        break;
                    }
                case estados.persiguiendo:
                    {
                        revisarPJDistancia();
                        direccion = (targetPJ.position - transform.position).normalized;
                        revisarMirada(targetPJ.position.x - transform.position.x);
                        break;
                    }
                case estados.volviendo:
                    {
                        direccion = (direccionInicial - transform.position).normalized;
                        revisarMirada(direccionInicial.x - transform.position.x);
                        if (m_estados == estados.volviendo && Vector2.Distance(transform.position, direccionInicial) < 0.1f)
                        {
                            m_estados = estados.patrullando;
                            m_NS_mov_IZDER.enabled = true;
                        }
                        break;
                    }
            }
        }
        
        
    }
    private void revisarMirada(float valor)
    {
        m_changeMirada.miradaPj(valor);
    }
    private void revisarPJDistancia()
    {
        float distanciaActual = Vector2.Distance(transform.position, targetPJ.position);
        
        if(distanciaActual < rangoVision)
        {
            if (m_estados == estados.patrullando || m_estados == estados.volviendo) { 
                m_estados = estados.persiguiendo;
                m_NS_mov_IZDER.enabled = false;
                m_animator.SetTrigger("perseguir");
            }
            
        }
        else
        {
            if (m_estados == estados.persiguiendo)
            {
                m_estados = estados.volviendo;
                m_animator.SetTrigger("patrullar");
            }
        }
    }
    private void FixedUpdate()
    {
        if (!recibiendoDanio) { 
            if(m_estados==estados.persiguiendo || m_estados== estados.volviendo)    m_rigidbody.velocity=direccion * velocidad;
        }
        else
        {

            m_rigidbody.velocity = direccion_retroceso;
            //print("direccion_retroceso : "+ direccion_retroceso);
        }

    }
    // Update is called once per frame
    private bool recibiendoDanio = false;
    private Vector3 direccion_retroceso;
    public void recibirDanio(dataDanio m_dataDanio)
    {
        if (!recibiendoDanio) { 
            recibiendoDanio = true;
            //print("aqui");
            m_rigidbody.velocity = Vector3.zero;
            Vector3 direccionDanio = (transform.position - m_dataDanio.m_transformAtacante.position).normalized;
            direccion_retroceso = direccionDanio * factorRetrocesoRecibirDanio;
            Invoke("terminarDanio",0.25f);
            //print("recibiendoDanio : " + recibiendoDanio);
        }
        else
        {
            print("mori como volador!!");
        }
    }
    private void terminarDanio()
    {
        if(recibiendoDanio)
            recibiendoDanio = false;
    }
    public void morir()
    {
        //vivo = false;
        velocidad = 0;
        m_animator.SetTrigger("morir");
        
    }




    private void OnDrawGizmos()
    {
        if (targetPJ != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetPJ.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        //Gizmos.DrawSphere(transform.position, rangoVision);
    }
    private void OnDrawGizmosSelected()
    {
        targetPJ = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }


}
