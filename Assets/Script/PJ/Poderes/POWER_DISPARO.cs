using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWER_DISPARO : Generic_Poder, IPOWER
{
    [SerializeField] private float m_coste;
    [SerializeField] private float m_fuerzaEmpuje;
    [SerializeField] private changeMirada m_changeMirada;
    CameraController m_CameraController;
    private void Awake()
    {
        base.m_PowerManager.Poder_DISPARO = this;
    }
    void Start()
    {
        //cargar desde DATA}
        //m_coste
        m_CameraController = MASTER_REFERENCE.instance.CameraController;
        m_PowerManager.StaminaPsiquica.Coste_Disparo = m_coste;//change
        base.curr_cadencia = 0;
    }
    movementPJ m_movementPJ;
    public void TryExecute(movementPJ m_movementPJ)
    {
        if(base.TryExecutePower(m_movementPJ, m_coste))
        {
            this.m_movementPJ = m_movementPJ;
            Execute(m_PowerManager.ChangeMirada.getMirada());
        }
        else
        {

        }
    }
    public void Execute(GLOBAL_TYPE.LADO _lado)
    {
        lado = _lado;
        base.ExecutePower(_lado);
    }


    public void _Shoot()
    {
        Debug.Log("disparar Function");
        Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_disparo);
        GameObject currBala = m_ObjectPooling.emitirObj(1.5f, m_transformPivote.position, true, true);
        BalaPsiquica _BalaPsiquica = currBala.GetComponent<BalaPsiquica>();
        _BalaPsiquica.StartMovement(lado);
        m_CameraController.ShakeCamera(15, 2, 0.6f);
        //m_PowerManager.ChangeMirada.getMirada()
        if (m_changeMirada.getMirada() == GLOBAL_TYPE.LADO.iz)
        {
            m_movementPJ.ApplyForce_X(Vector3.right, m_fuerzaEmpuje);
        }
        else
        {
            m_movementPJ.ApplyForce_X(Vector3.left, m_fuerzaEmpuje);

        }
    }
}
