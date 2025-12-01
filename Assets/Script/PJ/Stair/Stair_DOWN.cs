using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Stair_DOWN : MonoBehaviour
{

    [SerializeField] PlatformEffector2D m_PlatformEffector2D;
    [SerializeField] LayerMask m_layerPJ;
    [SerializeField] LayerMask m_layerOthers;
    [SerializeField] Stairs_UP m_Stairs_UP;
    // Start is called before the first frame update
    public enum Estados
    {
        fuera, saliendo, stay
    }
    Estados m_Estados;
    [SerializeField] [Tag] string m_tagPJ;
    [SerializeField] float m_coolDown;

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
    bool exitJumping = false;
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
        if (m_Estados== Estados.stay  && m_movementPJ.InputLeftAxis.y > 0.1f && m_movementPJ.transform.position.y > transform.position.y)//
        {
            exitJumping = true;
            m_Estados = Estados.fuera;
            m_Stairs_UP.DesactivarEscalera();
            DesactivarEscalera();
            m_movementPJ.ExitStairToTop();
        }
    }

    internal bool IsExitJumping()
    {
        return exitJumping;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("PuedeActivar() "+ PuedeActivar());
        if (collision.CompareTag(m_tagPJ) && PuedeActivar() && m_Estados == Estados.fuera)
        {
            ActivarEscalera();
        }
    }

    private bool PuedeActivar()
    {
        return m_movementPJ.CanStartStair_DOWN() && m_movementPJ.transform.position.y > transform.position.y;
    }

    private void ActivarEscalera()
    {
        exitJumping = false;
        m_PlatformEffector2D.colliderMask = m_layerOthers;
        m_Estados = Estados.stay;
        m_movementPJ.StartStair(m_Stairs_UP);
        LeanTween.moveX(m_movementPJ.gameObject, transform.position.x, 0.25f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tagPJ) && m_movementPJ.transform.position.y > transform.position.y)
        {
            exitJumping = false;//!
            m_Estados = Estados.fuera;
            m_movementPJ.ExitStair();
            DesactivarEscalera();
        }
    }

    public void DesactivarEscalera()
    {
        m_PlatformEffector2D.colliderMask = m_layerOthers;
        m_PlatformEffector2D.colliderMask += m_layerPJ;
        //m_PlatformEffector2D.colliderMask = (1 << m_layerPJ) | (1 << m_layerOthers); 
        m_Estados = Estados.saliendo;
        curr_coolDown = m_coolDown;
    }

    public void FullExit()
    {
        m_PlatformEffector2D.colliderMask = m_layerOthers;
        m_PlatformEffector2D.colliderMask += m_layerPJ;
        m_Estados = Estados.fuera;
        curr_coolDown = -1;
    }
}
