using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_NS_Return : StateMachineBehaviour
{
    NS_States_v2026_1 m_NS_States_v2026_1;
    Perseguir_25 m_Perseguir_25;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_Perseguir_25 == null)
        {
            m_Perseguir_25 = animator.transform.parent.parent.GetComponent<Perseguir_25>();
        }
        m_Perseguir_25.Activo = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_NS_States_v2026_1 == null)
        {
            m_NS_States_v2026_1 = animator.transform.parent.parent.GetComponent<NS_States_v2026_1>();
        }
        m_NS_States_v2026_1.ReturnExitPerseguir(true);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
