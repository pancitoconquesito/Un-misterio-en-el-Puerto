using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador_RectTransform : MonoBehaviour
{
    public RectTransform targetRectTransform; // El RectTransform que deseas rotar
    public float rotationSpeed = 90f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar el RectTransform en el eje Z
        targetRectTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
