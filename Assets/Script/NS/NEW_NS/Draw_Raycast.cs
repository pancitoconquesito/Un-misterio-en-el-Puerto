using UnityEngine;

public static class Draw_Raycast
{
    public static void DibujarFlechaCompleta(Vector2 punto, Vector2 direccion, Color color, float tamanio = 0.2f)
    {
        DibujarFlecha(punto + direccion * tamanio, direccion, color, 1f);
        DibujarLinea(punto, direccion, color, tamanio);
    }

    public static void DibujarPuntoFinal(Vector2 punto, Vector2 direccion, Color color, float tamanio = 1f)
    {
        DibujarFlecha(punto, direccion, color, tamanio);
        DibujarFlecha(punto + new Vector2(-tamanio, 0f), direccion, color, tamanio);
    }
    public static void DibujarLinea(Vector2 origen, Vector2 dir, Color color, float tamanio)
    {
        Vector2 destino = origen + dir * tamanio;
        Debug.DrawLine(origen, destino, color);
    }
    public static void DibujarFlecha(Vector2 punto, Vector2 direccion, Color color, float tamanio = 0.2f)
    {
        Vector2 derecha = new Vector2(-direccion.y, direccion.x) * tamanio; // perpendicular
        Vector2 izquierda = -derecha;

        Vector2 punta1 = punto - direccion * tamanio + derecha;
        Vector2 punta2 = punto - direccion * tamanio + izquierda;

        Debug.DrawLine(punto, punta1, color);
        Debug.DrawLine(punto, punta2, color);
    }
}
