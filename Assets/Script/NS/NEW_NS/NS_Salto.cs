using System;
using UnityEngine;

public class NS_Salto : MonoBehaviour
{
    public enum ProcesoSalto
    {
        sinSaltar, preparandoSalto, saltando,
    }
    ProcesoSalto procesoSalto = ProcesoSalto.sinSaltar;
    [SerializeField] CheckerRayCast CheckerRayCast_paredSalto;
    [SerializeField] CheckerRayCast CheckerRayCast_suelo;
    [SerializeField] Vector2 potenciaSalto;
    [SerializeField] float tiempoSalto = 2f;
    [SerializeField] Rigidbody2D m_rb;
    Vector2 movimientoFinal = Vector2.zero;
    float curr_tiempoSalto;
    bool saltando = false;
    public event Action OnSalto;
    public event Action EndSalto;
    GameObject goTarget;
    public void SetGoTarget(GameObject goTarget)
    {
        this.goTarget = goTarget;
    }

    private void Awake()
    {
        CheckerRayCast_paredSalto.OnHitEnter += Saltar;
        //CheckerRayCast_paredSalto.OnHitExit += ExitSaltar;
    }

    bool activo = false;

    public bool Activo { get => activo; set => activo = value; }

    void Update()
    {
        if (!activo)
        {
            return;
        }
        if (curr_tiempoSalto > -1f)
        {
            curr_tiempoSalto -= Time.deltaTime;
        }

        float difX = transform.position.x - goTarget.transform.position.x;
        if (difX < 0)
            movimientoFinal.x = -1;
        else
            movimientoFinal.x = 1;


        if (procesoSalto == ProcesoSalto.preparandoSalto)
        {
            actionSalto = false;
            curr_tiempoSalto = tiempoSalto;
            procesoSalto = ProcesoSalto.saltando;
        }
        if (procesoSalto == ProcesoSalto.saltando)
        {
            float lado = 1;
            if (movimientoFinal.x > 0)
            {
                lado = -1;
            }
            //m_rb.AddForce(new Vector2(lado * potenciaSalto.x, potenciaSalto.y) , ForceMode2D.Force);
            //m_rb.AddForce(new Vector2(lado, 1) * potenciaSalto.x, ForceMode2D.Force);
            //m_rb.AddForce(new Vector2(lado / 2f, 1) * potenciaSalto.x, ForceMode2D.Impulse);
            //m_rb.AddForce(new Vector2(0, 1) * potenciaSalto.x, ForceMode2D.Impulse);
            //m_rb.velocity = Vector2.zero;
            m_rb.AddForce(new Vector2(potenciaSalto.x*lado, potenciaSalto.y), ForceMode2D.Impulse);

            //if (saltoEstancado)
            //{
            //    m_rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
            //}
            //saltoEstancado = false;
            curr_tiempoSalto = -1;


            if (curr_tiempoSalto < 0f)
            {
                procesoSalto = ProcesoSalto.sinSaltar;
                EndSalto?.Invoke();

                activo = false;
            }
            return;
        }

        if (saltoEstancado && m_rb.velocity.y<0 && !CheckerRayCast_suelo.IsColisionando)
        {
            float lado = 1;
            if (movimientoFinal.x > 0)
            {
                lado *= -1;
            }
            saltoEstancado = false;
            //Debug.Log("Salto estancado****************");
            //m_rb.AddForce(new Vector2(50f, 0), ForceMode2D.Impulse);
            m_rb.velocity= new Vector2(30f * lado, m_rb.velocity.y+30f);
            Debug.Break();
        }


        if (actionSalto && procesoSalto == ProcesoSalto.sinSaltar)
        {
            if (curr_tiempoCongelado > -1f)
            {
                curr_tiempoCongelado -= Time.deltaTime;
            }
            if(curr_tiempoCongelado < 0)
            {
                actionSalto = false;
                OnSalto?.Invoke();
                procesoSalto = ProcesoSalto.preparandoSalto;
                saltoEstancado = true;
            }
        }




    }
    float tiempoCongelado = 1f;
    float curr_tiempoCongelado=1f;
    bool actionSalto = false;
    bool saltoEstancado = false;
    //revisar cuando este listo el salto e //!Invoke()
    private void Saltar(GameObject obj)
    {
        curr_tiempoCongelado = tiempoCongelado;
        actionSalto = true;
        Debug.Log("Colpared;: "+ CheckerRayCast_paredSalto.IsColisionando);
        if (activo && /*procesoSalto == ProcesoSalto.sinSaltar &&*/ CheckerRayCast_suelo.IsColisionando)
        {
            OnSalto?.Invoke();
            procesoSalto = ProcesoSalto.preparandoSalto;
        }
    }

    private void FixedUpdate()
    {
        if (!activo)
        {
            return;
        }

        //if (procesoSalto == ProcesoSalto.preparandoSalto)
        //{
        //    curr_tiempoSalto = tiempoSalto;
        //    procesoSalto = ProcesoSalto.saltando;
        //}
        //if (procesoSalto == ProcesoSalto.saltando)
        //{
        //    float lado = 1;
        //    if (movimientoFinal.x > 0)
        //    {
        //        lado *= -1;
        //    }
        //    //m_rb.AddForce(new Vector2(lado * potenciaSalto.x, potenciaSalto.y) , ForceMode2D.Force);
        //    //m_rb.AddForce(new Vector2(lado, 1) * potenciaSalto.x, ForceMode2D.Force);
        //    m_rb.AddForce(new Vector2(lado/2f, 1) * potenciaSalto.x, ForceMode2D.Impulse);


        //    curr_tiempoSalto = -1;


        //    if (curr_tiempoSalto < 0f)
        //    {
        //        procesoSalto = ProcesoSalto.sinSaltar;
        //        EndSalto?.Invoke();
        //        activo = false;
        //    }
        //    return;
        //}
    }

}
