using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SetCamerasCollider : MonoBehaviour
{
    public List<CinemachineConfiner2D> m_LCinemachineConfiner;
    private void Awake()
    {
        GameObject objPoly = GameObject.FindGameObjectWithTag("PolyCamera");
        if (objPoly == null) return;
        PolygonCollider2D poly = objPoly.GetComponent<PolygonCollider2D>();
        if (poly == null) return;
        foreach (var item in m_LCinemachineConfiner)
        {
            item.m_BoundingShape2D = poly;
        }
    }
}
