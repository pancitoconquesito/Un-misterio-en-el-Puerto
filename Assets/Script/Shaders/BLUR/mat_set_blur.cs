using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mat_set_blur : MonoBehaviour
{
    [Range(0.0f,10.0f)]public float offsetX = 0f; // Aquí defines el offset que quieras
    private Renderer rend;
    private MaterialPropertyBlock mpb;
    void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }
    void Update()
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_Offset", offsetX/500.0f);  // Asegúrate que "_Offset" es el nombre de la propiedad en el shader
        rend.SetPropertyBlock(mpb);
    }
}
