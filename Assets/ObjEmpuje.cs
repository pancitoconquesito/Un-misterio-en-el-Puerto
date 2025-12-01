using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEmpuje : MonoBehaviour, IDamageable
{
    [SerializeField]private float pushPower;
    [SerializeField] private float m_Cooldown;
    float m_currentCooldown;
    movementPJ m_movementPJ;
    bool counterTimer;
    private void Start()
    {
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
        m_currentCooldown = m_Cooldown;
    }
    private void Update()
    {
        if (counterTimer)
        {
            if (m_currentCooldown > 0) m_currentCooldown -= Time.deltaTime;
            else
            {
                counterTimer = false;
            }
        }
    }
    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if(!counterTimer && m_dataDanio.QuienAtaca== dataDanio.QuienEmiteDanio.Player /*&& !m_movementPJ.IsGrounded()*/)
        {
            m_currentCooldown = m_Cooldown;
            counterTimer = true;

            /*
            Vector3 dir = Vector3.zero;
            Vector3 attackPosition = m_dataDanio.PositionCollision;
            if (transform.position.y < m_movementPJ.gameObject.transform.position.y)
            {
                Debug.Log("UP");
                dir = -Vector3.up;
                m_movementPJ.ApplyForce_X(dir, pushPower);
                m_movementPJ.ApplyForce_Y(pushPower*100f);

            }
            else { 
            
                dir=(m_movementPJ.gameObject.transform.position- attackPosition).normalized;
                m_movementPJ.ApplyForce_X(dir, pushPower);

            }
            */
            m_movementPJ.ApplyForce(Vector2.up, pushPower);

            return true;
        }
        return false;
    }

    public bool IsEnemy()
    {
        return false;
    }
}
