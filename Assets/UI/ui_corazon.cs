using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ui_corazon : MonoBehaviour
{
    [SerializeField] private RectTransform m_RectTransformCorazones;
    [SerializeField] private int cantidadCorazones;
    private float tamanio;
    void Start()
    {
        cantidadCorazones = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().vidaPj;
        tamanio = m_RectTransformCorazones.sizeDelta.x;
        m_RectTransformCorazones.sizeDelta = new Vector2(tamanio * cantidadCorazones, tamanio);
    }
    public void updateVida_UI(int cantidadCorazonesActual)
    {
        cantidadCorazones = cantidadCorazonesActual;
        m_RectTransformCorazones.sizeDelta = new Vector2(tamanio * cantidadCorazones, tamanio);
    }
}
