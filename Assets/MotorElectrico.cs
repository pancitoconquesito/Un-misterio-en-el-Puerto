using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MotorElectrico : MonoBehaviour
{
    [SerializeField] private CheckElectricidad m_CheckElectricidad;
    public UnityEvent Prendido;
    public UnityEvent Apagado;
    // Start is called before the first frame update
    void Start()
    {
        m_CheckElectricidad.OnCollision_action+=EnergyStart;
        m_CheckElectricidad.OnExitCollision+= EnemrgyCancel;
    }

    public void EnergyStart()
    {
        Prendido?.Invoke();
        Debug.Log("Prendido");
    }
    public void EnemrgyCancel()
    {
        Apagado?.Invoke();
        Debug.Log("Apagado");
    }


}
