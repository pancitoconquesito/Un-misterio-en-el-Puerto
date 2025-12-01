using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColorChildren : MonoBehaviour
{
    [SerializeField] private Color32 m_color;
    private List<SpriteRenderer> m_L_sp;
    public void changeColor()
    {
        m_L_sp = new List<SpriteRenderer>();
        int count = transform.childCount;
        
        for (int i = 0; i < count; i++)
            m_L_sp.Add(transform.GetChild(i).transform.GetComponent<SpriteRenderer>());
        foreach (var item in m_L_sp)
        {
            item.color = m_color;
        }
    }
}
