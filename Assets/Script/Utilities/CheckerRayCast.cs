using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class CheckerRayCast : MonoBehaviour
{
    [Header("IDENTIFICADOR")]
    [SerializeField] string nombre;

    [Header("Configuración Raycast")]
    
    [SerializeField] Vector2 direccion;
    [SerializeField] Vector2 offsetOrigen;
    [SerializeField] [Tag]string tagFiltro;
    [SerializeField] [Label("Capas Múltiples")]LayerMask capasFiltro;
    [SerializeField] [Min(0f)]float distancia = 10f;
    [SerializeField] bool Check_solo_triggers=false;
    //[SerializeField] bool raycastAll=false;

    public event Action<GameObject> OnHitEnter;
    public event Action<GameObject> OnHitExit;
    GameObject ultimoObjetoDetectado;

    [ShowNonSerializedField]bool colisionando;

    public bool IsColisionando { get => colisionando;}
    public string Nombre { get => nombre; set => nombre = value; }
    public GameObject UltimoObjetoDetectado { get => ultimoObjetoDetectado; set => ultimoObjetoDetectado = value; }
    public Vector2 Direccion { get => direccion; set => direccion = value; }
    public Vector2 OffsetOrigen { get => offsetOrigen; set => offsetOrigen = value; }

    private void Update()
    {
        EjecutarRaycast2D();
    }
    void RaycastAll()
    {
        //var hits = Physics2D.RaycastAll(origen, direccion, distancia, capasFiltro);
        //foreach (var hit in hits)
        //{
        //    if (hit.collider.isTrigger)
        //        continue; // saltar triggers

        //    // este es tu hit válido
        //    Debug.Log("Golpeó: " + hit.collider.name);
        //    break;
        //}
    }
    private void EjecutarRaycast2D()
    {
        //sdsds
        //if (raycastAll)
        //{
        //    RaycastAll();
        //}

        if (Check_solo_triggers)
        {
            direccion = direccion.normalized;//! normalizarlo en start
            Vector2 origen = transform.position;
            origen += offsetOrigen;
            RaycastHit2D hit = Physics2D.Raycast(origen + (direccion * distancia),
                -direccion,
                distancia,
                capasFiltro);
            if (hit.collider != null && hit.collider.CompareTag(tagFiltro))
            {
                DibujarFlecha(origen, direccion * distancia, Color.green, 0.5f);
                if (hit.collider.gameObject != ultimoObjetoDetectado)
                {
                    ultimoObjetoDetectado = hit.collider.gameObject;
                    OnHitEnter?.Invoke(ultimoObjetoDetectado);
                }
                colisionando = true;
            }
            else
            {
                DibujarFlecha(origen, direccion * distancia, Color.red, 0.5f);
                if (ultimoObjetoDetectado != null)
                {
                    OnHitExit?.Invoke(ultimoObjetoDetectado);
                    ultimoObjetoDetectado = null;
                }
                colisionando = false;
            }
        }
        else
        {
            direccion = direccion.normalized; // ¡Normalizarlo en Start!
            Vector2 origen = (Vector2)transform.position + offsetOrigen;

            // 🔸 Configurar filtro para ignorar triggers
            ContactFilter2D filtro = new ContactFilter2D();
            filtro.useTriggers = false;                 // 👈 Ignora triggers
            filtro.SetLayerMask(capasFiltro);           // Tu layer mask
            filtro.useLayerMask = true;

            // 🔸 Array para almacenar el resultado
            RaycastHit2D[] hits = new RaycastHit2D[1];
            int cantidad = Physics2D.Raycast(origen + (direccion * distancia),
                                             -direccion,
                                             filtro,
                                             hits,
                                             distancia);

            if (cantidad > 0)
            {
                RaycastHit2D hit = hits[0];

                if (hit.collider != null && hit.collider.CompareTag(tagFiltro))
                {
                    DibujarFlecha(origen, direccion * distancia, Color.green, 0.5f);

                    if (hit.collider.gameObject != ultimoObjetoDetectado)
                    {
                        ultimoObjetoDetectado = hit.collider.gameObject;
                        OnHitEnter?.Invoke(ultimoObjetoDetectado);
                    }

                    colisionando = true;
                }
            }
            else
            {
                DibujarFlecha(origen, direccion * distancia, Color.red, 0.5f);

                if (ultimoObjetoDetectado != null)
                {
                    OnHitExit?.Invoke(ultimoObjetoDetectado);
                    ultimoObjetoDetectado = null;
                }

                colisionando = false;
            }
        }


    }

    private void DibujarFlecha(Vector2 origen, Vector2 direccion, Color color, float tamanio)
    {
        Debug.DrawRay(origen, direccion, color);

        // Flecha en la punta
        Vector2 fin = origen + direccion;
        Vector2 derecha = Quaternion.Euler(0, 0, 150) * direccion.normalized * tamanio;
        Vector2 izquierda = Quaternion.Euler(0, 0, -150) * direccion.normalized * tamanio;

        Debug.DrawRay(fin, derecha, color);
        Debug.DrawRay(fin, izquierda, color);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 origen = (Vector2)transform.position + offsetOrigen;
        Vector2 dirNormalizada = direccion.normalized;
        Gizmos.DrawLine(origen, origen + dirNormalizada * distancia);
        Vector2 fin = origen + dirNormalizada * distancia;
        float tamañoFlecha = 0.25f;
        Vector2 derecha = Quaternion.Euler(0, 0, 150) * dirNormalizada * tamañoFlecha;
        Vector2 izquierda = Quaternion.Euler(0, 0, -150) * dirNormalizada * tamañoFlecha;
        Gizmos.DrawLine(fin, fin + derecha);
        Gizmos.DrawLine(fin, fin + izquierda);
        Gizmos.DrawSphere(origen, 0.05f);
    }

}
