using UnityEngine;
using NaughtyAttributes;
using System;

public class RaycastCheck : MonoBehaviour
{
    [SerializeField] GameObject go_target;
    [SerializeField] bool showLineOnEditor=false;
    [SerializeField] private Vector2 direccion = Vector2.right;
    [SerializeField] Vector2 offset;
    [SerializeField, Min(0.1f)] float largo = 2f;
    [SerializeField, Layer] int layerMask;
    [SerializeField, Tag] string tagFiltrar;
    [ShowNonSerializedField]bool estaColisionando = false;
    public event Action OnColision;
    int mask_convertido;

    public bool EstaColisionando { get => estaColisionando; set => estaColisionando = value; }
    public Vector2 Direccion { get => direccion; set => direccion = value; }

    private void Awake()
    {
        mask_convertido = 1 << layerMask;
        if (go_target == null)
        {
            go_target = transform.gameObject;
        }
    }

    private void Update()
    {
        LanzarRaycast();
    }

    //private void LanzarRaycast()
    //{
    //    Vector2 origen = (Vector2)transform.position + offset;
    //    RaycastHit2D hit = Physics2D.Raycast(origen, Direccion.normalized, largo, mask_convertido);

    //    Vector2 end = origen + Direccion.normalized * largo;

    //    if (hit.collider != null &&
    //        (string.IsNullOrEmpty(tagFiltrar) || hit.collider.CompareTag(tagFiltrar)))
    //    {
    //        if (!estaColisionando)
    //        {
    //            OnColision?.Invoke();
    //        }
    //        estaColisionando = true;

    //        // Línea verde hasta el punto de colisión
    //        Debug.DrawLine(origen, hit.point, Color.green);

    //        // Línea amarilla desde colisión hasta el final del raycast
    //        Debug.DrawLine(hit.point, end, Color.yellow);

    //        // Flecha en el final
    //        DibujarFlecha(end, Direccion.normalized, Color.yellow);
    //    }
    //    else
    //    {
    //        Debug.DrawLine(origen, end, Color.red);

    //        // Flecha en el final
    //        DibujarFlecha(end, Direccion.normalized, Color.red);

    //        estaColisionando = false;
    //    }
    //}

    private void LanzarRaycast()
    {
        // Aplica la rotación del objeto al vector Dirección
        Vector2 direccionRotada = go_target.transform.TransformDirection(Direccion.normalized);

        Vector2 origen = (Vector2)transform.position + offset;
        RaycastHit2D hit = Physics2D.Raycast(origen, direccionRotada, largo, mask_convertido);

        Vector2 end = origen + direccionRotada * largo;

        if (hit.collider != null &&
            (string.IsNullOrEmpty(tagFiltrar) || hit.collider.CompareTag(tagFiltrar)))
        {
            if (!estaColisionando)
                OnColision?.Invoke();

            estaColisionando = true;

            Debug.DrawLine(origen, hit.point, Color.green);
            Debug.DrawLine(hit.point, end, Color.yellow);
            DibujarFlecha(end, direccionRotada, Color.yellow);
        }
        else
        {
            Debug.DrawLine(origen, end, Color.red);
            DibujarFlecha(end, direccionRotada, Color.red);
            estaColisionando = false;
        }
    }



    // Método auxiliar para dibujar flecha
    private void DibujarFlecha(Vector2 punto, Vector2 direccion, Color color, float tamaño = 0.2f)
    {
        Vector2 derecha = new Vector2(-direccion.y, direccion.x) * tamaño; // perpendicular
        Vector2 izquierda = -derecha;

        Vector2 punta1 = punto - direccion * tamaño + derecha;
        Vector2 punta2 = punto - direccion * tamaño + izquierda;

        Debug.DrawLine(punto, punta1, color);
        Debug.DrawLine(punto, punta2, color);
    }

    private void OnDrawGizmos()
    {
        if (showLineOnEditor)
        {
            Vector2 origen = (Vector2)transform.position + offset;
            if (go_target == null)
            {
                go_target = transform.gameObject;
            }
            Vector2 dir = go_target.transform.TransformDirection(Direccion.normalized);
            Vector2 end = origen + dir * largo;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(origen, end);

            // Flecha
            Vector2 derecha = new Vector2(-dir.y, dir.x) * 0.2f;
            Vector2 izquierda = -derecha;
            Vector2 punta1 = end - dir * 0.2f + derecha;
            Vector2 punta2 = end - dir * 0.2f + izquierda;
            Gizmos.DrawLine(end, punta1);
            Gizmos.DrawLine(end, punta2);
        }
    }


    //private void OnDrawGizmos()
    //{
    //    if (showLineOnEditor)
    //    {
    //        Vector2 origen = (Vector2)transform.position + offset;
    //        Vector2 end = origen + Direccion.normalized * largo;

    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(origen, end);

    //        // Flecha en gizmos
    //        Vector2 dir = Direccion.normalized;
    //        Vector2 derecha = new Vector2(-dir.y, dir.x) * 0.2f;
    //        Vector2 izquierda = -derecha;
    //        Vector2 punta1 = end - dir * 0.2f + derecha;
    //        Vector2 punta2 = end - dir * 0.2f + izquierda;
    //        Gizmos.DrawLine(end, punta1);
    //        Gizmos.DrawLine(end, punta2);
    //    }
    //}

}
