using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovimientoPatrulla : MonoBehaviour
{
    public enum TipoPatrulla
    {
        VariosPuntos,
        //IzDer, 
    }
    [SerializeField] TipoPatrulla tipoPatrulla;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] NS_Salto m_NS_Salto;
    //[SerializeField] RecibiDanio_cb recibiDanio_cb;


    [Header("BASE ----------------------------")]
    [SerializeField] bool ignorarY=true;
    [SerializeField] bool addGravity=true;
    [SerializeField] float gravedad;
    [SerializeField] float velocidad;

    NS_States_v2026_1 m_NS_States_v2026_1_MASTER;
    internal void Config(NS_States_v2026_1 nS_States_v2026_1)
    {
        m_NS_States_v2026_1_MASTER = nS_States_v2026_1;
    }

    //[Header("IZ DER ----------------------------")]
    //[SerializeField] GameObject go_puntoIZ;
    //[SerializeField] GameObject go_puntoDER;
    //[SerializeField] float distanciaMinima;
    //Vector2 v_puntoIZ;
    //Vector2 v_puntoDER;

    [Header("VariosPuntos----------------------------")]
    [SerializeField] GameObject[] l_posiciones;
    [SerializeField] bool pingPong=false;
    [SerializeField] float distanciaMinima_VariosPuntos;
    [SerializeField] LookAt2D_Rotator LookAt2D_Rotator;
    List<Vector2> l_posiciones_vector;
    int currIndex_VariosPuntos = 0;
    int totalPuntos_VariosPuntos;
    //[Header("VariosPuntos avanzado ------------------------")]
    //[SerializeField] GameObject go_checkerRayCast;
    //List<CheckerRayCast> checkerRayCast;
    //CheckerRayCast checkerRayCast_der_down;
    //CheckerRayCast checkerRayCast_der_up;
    //CheckerRayCast checkerRayCast_iz_up;
    //CheckerRayCast checkerRayCast_iz_down;


    float delaySalto = 1.5f;
    float curr_delaySalto = 0;
    bool patrullando=false;
    //bool recibiendoDanio=false;
    //Vector2 velocidadImpacto=Vector2.zero;
    Vector2 movimientoFinal = Vector2.zero;
    //bool quitandoDanio = false;
    void StartSalto()
    {
        saltando = true;
    }
    void EndSalto()
    {
        curr_delaySalto = delaySalto;
        saltando = false;
    }
    private void Awake()
    {
        if (!addGravity)
        {
            rb.gravityScale = 0;
        }
        m_NS_Salto.OnSalto += StartSalto;
        m_NS_Salto.EndSalto += EndSalto;
        //recibiDanio_cb.OnRecibirDanio += OnRecibirDanio;
        //if (go_checkerRayCast != null)
        //{
        //    checkerRayCast = go_checkerRayCast.GetComponents<CheckerRayCast>().ToList();
        //}

        //if (checkerRayCast != null && checkerRayCast.Count > 0)
        //{
        //    checkerRayCast_der_down = checkerRayCast.Find(x => x.Nombre == "der_down");
        //    checkerRayCast_der_up = checkerRayCast.Find(x => x.Nombre == "der_up");
        //    checkerRayCast_iz_up = checkerRayCast.Find(x => x.Nombre == "iz_up");
        //    checkerRayCast_iz_down = checkerRayCast.Find(x => x.Nombre == "iz_down");
        //}
    }
    //private void OnDisable()
    //{
    //    recibiDanio_cb.OnRecibirDanio -= OnRecibirDanio;
    //    StopAllCoroutines();
    //}
    private void Start()
    {
        if (l_posiciones != null && l_posiciones.Length > 0)
        {
            l_posiciones_vector = new List<Vector2>();
            foreach (var item in l_posiciones)
            {
                l_posiciones_vector.Add(item.transform.position);
                item.transform.SetParent(null);
            }
            totalPuntos_VariosPuntos = l_posiciones_vector.Count;
            currIndex_VariosPuntos = 0;

            if (ignorarY)
            {
                l_posiciones_vector[0] = new Vector2(l_posiciones_vector[0].x, transform.position.y);
            }

            //pingPong
            for (int i = 1; i < totalPuntos_VariosPuntos; i++)
            {
                l_posiciones_vector.Add(l_posiciones_vector[totalPuntos_VariosPuntos - i]);
            }
            Vector2 direccion = (Vector2)transform.position - l_posiciones_vector[0];
            //LookAt2D_Rotator.LookAtDirection(direccion);
        }
    }
    internal void SetPatrulla(bool value)
    {
        if(value && !patrullando)
        {
            IniciarPatrulla();
        }
        patrullando = value;
    }

    void Update()
    {
        if (!patrullando)
        {
            return;
        }

        if (curr_delaySalto > -1f)
        {
            curr_delaySalto -= Time.deltaTime;
        }

        switch (tipoPatrulla)
        {
            //case TipoPatrulla.IzDer:
            //    {
            //        IzDer();
            //        break;
            //    }
            case TipoPatrulla.VariosPuntos:
                {
                    VariosPuntos();
                    break;
                }
        }
    }
    bool saltando = false;
    private void FixedUpdate()
    {
        if (!patrullando || curr_delaySalto>0 || saltando)
        {
            return;
        }

        if (!m_NS_States_v2026_1_MASTER.RecibiendoDanio)
        {
            if (addGravity)
            {
                rb.velocity = movimientoFinal + Vector2.up * gravedad;
            }
            else
            {
                rb.velocity = movimientoFinal;
            }
        }
        else
        {
            rb.velocity = m_NS_States_v2026_1_MASTER.VelocidadImpacto;
        }
    }

    private void VariosPuntos()
    {
        if (ignorarY)
        {
            l_posiciones_vector[currIndex_VariosPuntos] = new Vector2(l_posiciones_vector[currIndex_VariosPuntos].x, transform.position.y);
        }
        float distancia = Vector2.Distance(transform.position, l_posiciones_vector[currIndex_VariosPuntos]);
        Vector2 direccion = l_posiciones_vector[currIndex_VariosPuntos] - (Vector2)transform.position;

        if (distancia < distanciaMinima_VariosPuntos)
        {
            currIndex_VariosPuntos++;
            if (currIndex_VariosPuntos >= l_posiciones_vector.Count)
            {
                currIndex_VariosPuntos = 0;
            }
            return;
        }
        //Vector2 saveDir = direccion;
        ////direccion vs colisionadores
        //if (checkerRayCast_der_down.IsColisionando && saveDir.x > 0 && saveDir.y > 0)
        //{
        //    direccion = Vector2.up;
        //}
        //if (checkerRayCast_der_up.IsColisionando && saveDir.x > 0 && saveDir.y <= 0)
        //{
        //    direccion = Vector2.down;
        //}
        //if (checkerRayCast_iz_down.IsColisionando && saveDir.x < 0 && saveDir.y > 0)
        //{
        //    direccion = Vector2.up;
        //}
        //if (checkerRayCast_iz_up.IsColisionando && saveDir.x < 0 && saveDir.y < 0)
        //{
        //    direccion = Vector2.down;
        //}


        movimientoFinal = direccion.normalized * velocidad;
        LookAt2D_Rotator.LookAtDirection(direccion);
    }


    //internal void OnRecibirDanio(Vector2 velocidadImpacto)
    //{
    //    this.velocidadImpacto = velocidadImpacto;
    //    recibiendoDanio = true;
    //}

    //private void quitarDanio()
    //{
    //    if (recibiendoDanio) recibiendoDanio = false;
    //    quitandoDanio = false;
    //}


    //GLOBAL_TYPE.LADO ladoPatrulla = GLOBAL_TYPE.LADO.iz;
    private void IniciarPatrulla()
    {
        //buscar el que este mas lejos, (X A B) = debo ir a B
    }

    //void IzDer()
    //{
    //    int ladoDir = 1;
    //    float distancia = 0;
    //    if (ignorarY)
    //    {
    //        v_puntoIZ.y = transform.position.y;
    //        v_puntoDER.y = transform.position.y;
    //    }

    //    distancia = Vector2.Distance(transform.position, v_puntoIZ);
    //    if(ladoPatrulla == GLOBAL_TYPE.LADO.iz)
    //    {
    //        if(distancia < distanciaMinima)
    //        {
    //            CambiarLado();
    //        }
    //    }
    //    else
    //    {
    //        distancia = Vector2.Distance(transform.position, v_puntoDER);
    //        if (distancia < distanciaMinima)
    //        {
    //            CambiarLado();
    //        }
    //    }
    //    if (ladoPatrulla == GLOBAL_TYPE.LADO.iz)
    //    {
    //        ladoDir = -1;
    //    }

    //    movimientoFinal = Vector2.right * velocidad * ladoDir;
    //}


    //void CambiarLado()
    //{
    //    if (ladoPatrulla == GLOBAL_TYPE.LADO.iz)
    //    {
    //        ladoPatrulla = GLOBAL_TYPE.LADO.der;
    //    }
    //    else
    //    {
    //        ladoPatrulla = GLOBAL_TYPE.LADO.iz;
    //    }
    //    //changeMirada.miradaPj(ladoPatrulla);
    //}
}


