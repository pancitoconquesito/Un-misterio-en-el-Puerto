using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder_Quinto : Generic_Poder, IPOWER
{
    [SerializeField] private float m_coste;
    private void Awake()
    {
        base.m_PowerManager.PODER_Quinto = this;
    }
    void Start()
    {
        m_PowerManager.StaminaPsiquica.Coste_Quinto = m_coste;//change
        base.curr_cadencia = 0;
    }
    public void TryExecute(movementPJ m_movementPJ)
    {
        if (m_movementPJ.IsGroundedFunction() && base.TryExecutePower(m_movementPJ, m_coste))
        {
            Debug.Log("puede ejecutar poder | esta En el suelo: " + m_movementPJ.IsGroundedFunction());
            Execute(m_PowerManager.ChangeMirada.getMirada());
        }
        else
        {
            Debug.Log("Necesita estar en el suelo.");
        }
    }
    public void Execute(GLOBAL_TYPE.LADO _lado)
    {
        lado = _lado;
        base.ExecutePower(_lado);
        Debug.Log("Execute Quinto!");
    }

}
