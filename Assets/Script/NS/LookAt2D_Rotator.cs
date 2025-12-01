using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D_Rotator : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] Transform spriteChild;   // el hijo que tiene el SpriteRenderer
    [SerializeField] bool spriteFacesLeftByDefault = true;
    [SerializeField] float rotationOffset = 0f;

    Vector3 originalScale;

    void Awake()
    {
        if (spriteChild == null && transform.childCount > 0)
            spriteChild = transform.GetChild(0);

        if (spriteChild != null)
            originalScale = spriteChild.localScale;
    }


    public void LookAtDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Si el sprite apunta a la izquierda por defecto, le sumamos 180°
        if (spriteFacesLeftByDefault)
            angle += 180f;

        // Offset opcional
        angle += rotationOffset;

        // Aplicar rotación
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // --- 🔹 Flip automático para mantener sprite "derecho" ---
        if (spriteChild != null)
        {
            Vector3 scale = originalScale;

            // Si el ángulo está entre 90° y 270°, el sprite estaría de cabeza
            // -> invertimos su escala en Y para mantenerlo visualmente derecho
            if (angle > 90f && angle < 270f)
                scale.y = -Mathf.Abs(originalScale.y);
            else
                scale.y = Mathf.Abs(originalScale.y);

            spriteChild.localScale = scale;
        }

        //if (direction.sqrMagnitude < 0.0001f)
        //    return; // dirección cero, no hacemos nada

        //// Calcular ángulo
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //// Si el sprite mira a la izquierda por defecto, le sumamos 180 grados
        //if (spriteFacesLeftByDefault)
        //    angle += 180f;

        //// Aplicar offset adicional si se quiere ajustar visualmente
        //angle += rotationOffset;

        //// Aplicar rotación al objeto (padre)
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void FlipY(bool flip)
    {
        if (spriteChild == null) return;

        Vector3 s = originalScale;
        s.y = Mathf.Abs(s.y) * (flip ? -1f : 1f);
        spriteChild.localScale = s;
    }


    public void FlipX(bool flip)
    {
        if (spriteChild == null) return;

        Vector3 s = originalScale;
        s.x = Mathf.Abs(s.x) * (flip ? -1f : 1f);
        spriteChild.localScale = s;
    }
}
