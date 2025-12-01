using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ExplotarAlColisionar : MonoBehaviour
{
    [SerializeField] Bomba m_Bomba;
    [SerializeField][Tag] string m_tag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Col: {collision.name} | {collision.tag}");
        if (collision.CompareTag(m_tag))
        {
            m_Bomba.Explotar();
        }
    }
}
