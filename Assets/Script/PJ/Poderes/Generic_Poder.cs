using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Poder : MonoBehaviour
{
    [SerializeField] protected PowerManager m_PowerManager;

    [SerializeField] protected Transform m_transformPivote;
    [SerializeField] protected ObjectPooling m_ObjectPooling;
    [SerializeField] protected float cadencia;
    [SerializeField] protected PowerManager.NumPoder m_NumPoder;
    [SerializeField] protected GLOBAL_TYPE.ESTADOS m_statePower;
    protected float curr_cadencia;
    protected GLOBAL_TYPE.LADO lado;

    void Start()
    {
        ///curr_cadencia = 0;
    }

    void Update()
    {
        if (curr_cadencia > -1) curr_cadencia -= Time.deltaTime;
    }

    protected bool TryExecutePower(movementPJ m_movementPJ, float costePoder)
    {
        //float costePoder = m_PowerManager.StaminaPsiquica.Coste_Disparo;//change
        if (m_movementPJ.CanExecutePower() 
            && m_PowerManager.StaminaPsiquica.getCantidadStamina() > costePoder && curr_cadencia < 0)
        {
            curr_cadencia = cadencia;
            m_PowerManager.StaminaPsiquica.addStamina(-costePoder);
            m_movementPJ.SetState(m_statePower);
            return true;
        }
        return false;
    }
    protected void ExecutePower(GLOBAL_TYPE.LADO _lado)
    {
        m_PowerManager.Animator_PJ.SetInteger("Power_INT", (int)m_NumPoder);
        m_PowerManager.Animator_PJ.SetTrigger("Power_TRIGGER");
        lado = _lado;
    }
    
}
