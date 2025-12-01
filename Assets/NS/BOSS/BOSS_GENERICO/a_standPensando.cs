using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class a_standPensando : StateMachineBehaviour
{
    public enum ActionCamino
    {
        MOVE, ESTRATEGIA
    }

    [Header("Cambio a Action")]
    [SerializeField] float m_limite = 0.0f;
    [SerializeField] int m_contadorExtraProbabilidad;
    [SerializeField] float m_contadorRangoExtra;

    [Header("Selector move")]
    [SerializeField] List<int> prop_move;

    [Header("Selector estrategia")]
    [SerializeField] List<int> prop_estrategia;
    
    
    float m_contador=0.0f;
    int numeroAleatorio_action;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_contador = 0.0f;
        numeroAleatorio_action = Random.Range(0, 101);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_contador <= m_limite)
        {
            if(numeroAleatorio_action < m_contadorExtraProbabilidad)
            {
                float numeroAleatorio_f = Random.Range(0f, 1f);
                m_contador += Time.deltaTime + numeroAleatorio_f * m_contadorRangoExtra;
            }
            else
            {
                m_contador += Time.deltaTime;
            }
        }
        else
        {
            switch (Criterio())
            {
                case ActionCamino.MOVE:
                    {
                        int counterSelected = GetRandomIndex(prop_move);
                        animator.SetInteger("num_move", counterSelected);
                        animator.SetTrigger("tr_move");
                        break;
                    }
                case ActionCamino.ESTRATEGIA:
                    {
                        int counterSelected = GetRandomIndex(prop_estrategia);
                        animator.SetInteger("num_estrategia", counterSelected);
                        animator.SetTrigger("tr_estrategia");
                        break;
                    }
            }
        }
    }

    private int GetRandomIndex(List<int> lista)
    {
        int numeroAleatorio_selectAction = Random.Range(0, 101);
        int counterProb = 0;
        int counterSelected = 1;
        foreach (int curr_prob in lista)
        {
            counterProb += curr_prob;
            if (counterProb >= numeroAleatorio_selectAction)
            {
                break;
            }
            counterSelected++;
        }
        return counterSelected;
    }

    public abstract ActionCamino Criterio();


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
