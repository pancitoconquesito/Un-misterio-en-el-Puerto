using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ActivadorEvent : MonoBehaviour, IDamageable
{
    [SerializeField] bool bloquearAlActivar = true;
    [SerializeField] dataDanio.QuienEmiteDanio quienPuedeDaniar;
    public bool IsEnemy() => false;
    public UnityEvent OnDamageable;
    bool bloqueado = false;
    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if((bloqueado && bloquearAlActivar) || quienPuedeDaniar != m_dataDanio.QuienAtaca)
        {
            return false;
        }
        if (bloquearAlActivar)
        {
            bloqueado = true;
        }
        OnDamageable?.Invoke();
        return true;
    }
}
