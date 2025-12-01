using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ParedesOcultas : MonoBehaviour
{
    [SerializeField] bool m_mantenerseDesaparecido;
    [SerializeField] [Tag] string m_pj_tag;
    [SerializeField] float m_tiempoInterpolacion_entrada;
    [SerializeField] float m_tiempoInterpolacion_salida;
    List<NodeSP> m_l_sp;
    private void Awake()
    {
        m_l_sp = new List<NodeSP>();
        int countChild = transform.childCount;
        for (int i = 0; i < countChild; i++)
        {
            NodeSP new_Node = new NodeSP();
            new_Node.m_sp = transform.GetChild(i).GetComponent<SpriteRenderer>();
            new_Node.id_desaparecer = -1;
            new_Node.id_aparecer = -1;
            m_l_sp.Add(new_Node);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_pj_tag))
        {
            foreach (var item in m_l_sp)
            {
                if (item.id_aparecer!=-1)
                {
                    LeanTween.cancel(item.id_aparecer);
                    item.id_aparecer = -1;
                }
            }

            foreach (var item in m_l_sp)
            {
                float initialValueAlpha = item.m_sp.color.a;
                item.id_desaparecer = LeanTween.value(gameObject, initialValueAlpha, 0, m_tiempoInterpolacion_entrada)
                    .setOnUpdate((float value) =>
                    {
                        Color newColor = item.m_sp.color;
                        newColor.a = value;
                        item.m_sp.color = newColor;
                    })
                    .setEase(LeanTweenType.easeInOutCubic).id;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_mantenerseDesaparecido)
        {
            return;
        }
        if (collision.CompareTag(m_pj_tag))
        {
            foreach (var item in m_l_sp)
            {
                if (item.id_desaparecer != -1)
                {
                    LeanTween.cancel(item.id_desaparecer);
                    item.id_desaparecer = -1;
                }
            }

            foreach (var item in m_l_sp)
            {
                float initialValueAlpha = item.m_sp.color.a;
                item.id_aparecer = LeanTween.value(gameObject, initialValueAlpha, 1, m_tiempoInterpolacion_salida)
                    .setOnUpdate((float value) =>
                    {
                        Color newColor = item.m_sp.color;
                        newColor.a = value;
                        item.m_sp.color = newColor;
                    })
                    .setEase(LeanTweenType.easeInOutCubic).id;
            }
        }
    }
}
public class NodeSP
{
    public SpriteRenderer m_sp;
    public int id_desaparecer;
    public int id_aparecer;
}
