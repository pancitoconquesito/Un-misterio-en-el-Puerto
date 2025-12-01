using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas_Controller_25 : MonoBehaviour
{
    public enum TipoArea
    {
        Rectangular, Circular
    }
    [SerializeField] TipoArea m_tipoArea;
    [SerializeField] Vector2 m_areaVector;
    [SerializeField] Color m_colorArea;
    [SerializeField] bool m_estatico;
    Vector2 initialPosition;
    private void OnDrawGizmos()
    {
        if (m_areaVector != null)
        {
            Vector2 posini = transform.position;
            if (m_estatico)
            {
                posini = initialPosition;
            }


            Gizmos.color = m_colorArea;
            if (m_tipoArea == TipoArea.Circular)
            {
                m_areaVector.y = m_areaVector.x;
            }

            if (m_tipoArea == TipoArea.Rectangular)
            {
                Gizmos.DrawWireCube(posini, new Vector3(m_areaVector.x, m_areaVector.y, 0) * 2f);
                Gizmos.DrawCube(posini, new Vector3(m_areaVector.x, m_areaVector.y, 0) * 2f);
            }
            else
            {
                Gizmos.DrawSphere(posini, m_areaVector.magnitude);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(posini, new Vector2(posini.x, posini.y + m_areaVector.magnitude));
                Gizmos.DrawLine(posini, new Vector2(posini.x + m_areaVector.magnitude, posini.y));
            }

        }
    }
}
