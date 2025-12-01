using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Stairs_UP : MonoBehaviour
{
    public enum Estados
    {
        fuera, saliendo, stay
    }
    Estados m_Estados;
    [SerializeField][Tag] string m_tagPJ;
    [SerializeField] float m_coolDown;
    [SerializeField] Stair_DOWN m_Stair_DOWN;
    [SerializeField] StairsTop m_StairsTop;
    float curr_coolDown;
    movementPJ m_movementPJ;

    // Start is called before the first frame update
    private void Awake()
    {
        m_Estados = Estados.fuera;
        curr_coolDown = -1;
    }
    void Start()
    {
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Estados == Estados.saliendo)
        {
            curr_coolDown -= Time.deltaTime;
            if (curr_coolDown < 0)
            {
                m_Estados = Estados.fuera;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tagPJ) && PuedeActivar() && m_Estados == Estados.fuera)
        {
            ActivarEscalera();
        }
    }

    private bool PuedeActivar()
    {
        return m_movementPJ.CanStartStair_UP() && !m_Stair_DOWN.IsExitJumping();
    }

    private void ActivarEscalera()
    {
        m_Stair_DOWN.DesactivarEscalera();
        m_Estados = Estados.stay;
        m_movementPJ.StartStair(this);
        LeanTween.moveX(m_movementPJ.gameObject, transform.position.x, 0.25f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tagPJ))
        {
            m_Estados = Estados.fuera;
            m_movementPJ.ExitStair();
        }
    }

    public void DesactivarEscalera()
    {
        m_Stair_DOWN.DesactivarEscalera();
        m_Estados = Estados.saliendo;
        curr_coolDown = m_coolDown;
    }

    internal bool IsActiveStair()
    {
        return m_Estados == Estados.stay;
    }
}
