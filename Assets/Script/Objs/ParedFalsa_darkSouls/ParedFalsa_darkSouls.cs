using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedFalsa_darkSouls : MonoBehaviour, IDamageable
{
    [SerializeField] Animator m_anim;
    [SerializeField] ObjectPooling m_particulas;
    [SerializeField] Collider2D m_trigger;
    [SerializeField] Collider2D m_colisino;
    public bool IsEnemy()=>false;

    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        m_anim.SetTrigger("Start");
        m_trigger.enabled = false;
        m_colisino.enabled = false;
        m_particulas.emitirObj(1, true);
        return false;
    }
}
