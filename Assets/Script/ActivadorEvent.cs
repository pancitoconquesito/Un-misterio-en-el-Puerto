using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ActivadorEvent : MonoBehaviour, IDamageable
{
    [SerializeField] bool bloquearAlActivar = true;
    [SerializeField] dataDanio.QuienEmiteDanio quienPuedeDaniar;
    [SerializeField] float coldDown=0.25f;
    [SerializeField] bool variosUsos = false;
    float curr_coldDown;
    public bool IsEnemy() => false;
    public UnityEvent OnDamageable;
    bool bloqueado = false;

    private void Awake()
    {
        curr_coldDown = coldDown;
    }
    private void Update()
    {
        if (curr_coldDown > -1)
        {
            curr_coldDown -= Time.deltaTime;
        }
    }
    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if(curr_coldDown>0 || (bloqueado && bloquearAlActivar) || quienPuedeDaniar != m_dataDanio.QuienAtaca)
        {
            return false;
        }
        curr_coldDown = coldDown;
        if (!variosUsos && bloquearAlActivar)
        {
            bloqueado = true;
        }
        OnDamageable?.Invoke();
        return true;
    }
}
