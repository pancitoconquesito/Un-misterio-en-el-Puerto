using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class AnimEvent : MonoBehaviour
{
    [SerializeField][SortingLayer] string m_sortingLayer;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private POWER_DISPARO m_Poder_DISPARO;
    [SerializeField] private PODER_Bomba m_PoderBomba;
    [SerializeField] private movementPJ m_movementPJ;

    public void _JUMP(float potentcia)
    {

        this.m_movementPJ.ApplyForce_X(Vector2.up, potentcia);
    }

    public void Anim_evt_Disparar()
    {
        Debug.Log("disparar event");
        m_Poder_DISPARO._Shoot();
    }
    public void Anim_evt_Bomba()
    {
        Debug.Log("Bomba event");
        m_PoderBomba._CreateBomba();
    }

    public void MoveToFront()
    {
        m_SpriteRenderer.sortingLayerName = m_sortingLayer;
        m_SpriteRenderer.sortingOrder = 10;
    }
}
