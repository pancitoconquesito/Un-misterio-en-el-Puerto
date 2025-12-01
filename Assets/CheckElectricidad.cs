using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckElectricidad : MonoBehaviour
{
    [SerializeField] private float timeCadenceDamage;
    [SerializeField] private ObjectPooling _ObjectPooling;
    public Action OnCollision_action, OnExitCollision;
    float curr_timeCadenceDamage;
    private void Start()
    {
        curr_timeCadenceDamage = timeCadenceDamage;
    }
    private void Update()
    {
        if(curr_timeCadenceDamage>0)
            curr_timeCadenceDamage -= Time.deltaTime;
    }
    private bool EjecutarDanio(dataDanio m_dataDanio)
    {
        if (curr_timeCadenceDamage > 0)
        {
            return false;
        }
        curr_timeCadenceDamage = timeCadenceDamage;
        _ObjectPooling.emitirObj(0.4f, m_dataDanio.PositionCollision);
        OnCollision_action?.Invoke();
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        dataDanio _dataDanio = collision.gameObject.GetComponent<dataDanio>();
        if (_dataDanio != null && _dataDanio.TipoElementalDanio == dataDanio.TipoElementalDanioEnum.Electrico)
        {
            _dataDanio.SetPositionCollision(collision.ClosestPoint(transform.position));
            EjecutarDanio(_dataDanio);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dataDanio _dataDanio = collision.gameObject.GetComponent<dataDanio>();
        if (_dataDanio != null && _dataDanio.TipoElementalDanio == dataDanio.TipoElementalDanioEnum.Electrico)
        {
            OnExitCollision?.Invoke();
        }
        
    }
}
