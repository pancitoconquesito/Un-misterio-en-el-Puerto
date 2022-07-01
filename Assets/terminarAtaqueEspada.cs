using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminarAtaqueEspada : StateMachineBehaviour
{
    [SerializeField] private movementPJ m_movementPJ=null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_movementPJ == null)
            m_movementPJ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>();
        m_movementPJ.swordFinished();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //verride public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (m_movementPJ == null)
    //        m_movementPJ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>();
    //    m_movementPJ.swordFinished();
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_movementPJ == null)
            m_movementPJ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>();
        m_movementPJ.swordFinished();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<movementPJ>().swordFinished();
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
