using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStatePower : StateMachineBehaviour
{
    [SerializeField] private movementPJ m_movementPJ = null;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_movementPJ == null)
            m_movementPJ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>();
        m_movementPJ.FinishPowerDisparo();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_movementPJ == null)
            m_movementPJ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>();
        m_movementPJ.FinishPowerDisparo();
    }
}
