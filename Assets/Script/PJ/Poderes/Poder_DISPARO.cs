using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder_DISPARO : MonoBehaviour
{
    [SerializeField] private Transform m_transformPivote;
    [SerializeField] private changeMirada m_changeMirada;
    [SerializeField] private ObjectPooling m_ObjectPooling;
    [SerializeField] private staminaPsiquica m_staminaPsiquica;
    [SerializeField] private Animator m_Animator;

    [SerializeField] private float cadencia;
    float curr_cadencia;

    // Start is called before the first frame update
    void Start()
    {
        //cargar desde DATA
        m_staminaPsiquica.Coste_Disparo = 30;
        curr_cadencia = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curr_cadencia > -1) curr_cadencia -= Time.deltaTime;
    }

    internal void TryExecute(movementPJ m_movementPJ)
    {
        if(m_staminaPsiquica.getCantidadStamina() > m_staminaPsiquica.Coste_Disparo && curr_cadencia<0)
        {
            curr_cadencia = cadencia;
            m_staminaPsiquica.addStamina(-m_staminaPsiquica.Coste_Disparo);
            m_movementPJ.SetState(GLOBAL_TYPE.ESTADOS.POWER_Disparo);
            Execute(m_changeMirada.getMirada());
        }
    }

    private void Execute(GLOBAL_TYPE.LADO _lado)
    {
        m_Animator.SetInteger("Power_INT", 1);//1: Disparo
        m_Animator.SetTrigger("Power_TRIGGER");
        lado = _lado;
    }
    GLOBAL_TYPE.LADO lado;
    public void _Shoot()
    {
        Debug.Log("disparar Function");

        GameObject currBala = m_ObjectPooling.emitirObj(1.5f, m_transformPivote.position, true, true);
        BalaPsiquica _BalaPsiquica = currBala.GetComponent<BalaPsiquica>();
        _BalaPsiquica.StartMovement(lado);
    }
}
