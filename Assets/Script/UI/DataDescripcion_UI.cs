using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDescripcion_UI : MonoBehaviour
{
    [SerializeField][TextArea(2, 4)] string m_titulo;
    [SerializeField][TextArea(6,10)] string m_descripcion;

    public string Descripcion { get => m_descripcion; set => m_descripcion = value; }
    public string Titulo { get => m_titulo; set => m_titulo = value; }
}
