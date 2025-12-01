using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaState : MonoBehaviour
{
    [SerializeField] MovimientoPatrulla m_MovimientoPatrulla;
    [SerializeField] Animator animator;

    bool stateActive = false;
    bool hasPatrulla;

    public bool HasPatrulla { get => hasPatrulla; set => hasPatrulla = value; }


    NS_States_v2026_1 m_NS_States_v2026_1_MASTER;
    internal void Config(NS_States_v2026_1 nS_States_v2026_1)
    {
        m_NS_States_v2026_1_MASTER = nS_States_v2026_1;
        m_MovimientoPatrulla.Config(nS_States_v2026_1);
    }
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("hasPatrulla", HasPatrulla);
        
    }

    public void StartState()//!
    {
        m_MovimientoPatrulla.SetPatrulla(true);
        stateActive = true;
        animator.SetTrigger("tr_move");
        //Debug.Log("tr_move");
    }
    public void StopState()//!
    {
        m_MovimientoPatrulla.SetPatrulla(false);
        stateActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!stateActive)
        {
            return;
        }

        //if()animator.SetTrigger("tr_move");


        //check perseguir

        //check estrategia

        //Debug.Log("Patrullando");
        //do
    }


}
