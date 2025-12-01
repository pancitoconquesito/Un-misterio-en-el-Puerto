using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameraShake : MonoBehaviour
{
    private CinemachineImpulseSource m_CinemachineImpulseSource;
    public static cameraShake instancia;
    private void Awake()
    {
        instancia = this;
    }
    private void Start()
    {
        m_CinemachineImpulseSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineImpulseSource>();
    }


    public void Shake(float amplitud, float frecuencia, float tiempo, CinemachineVirtualCamera m_CinemachineVirtualCamera)
    {
        amplitud *= 2f;
        var noise = m_CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = amplitud;
        noise.m_FrequencyGain = frecuencia;
        StartCoroutine(graduallyReduceShake(tiempo, noise));
    }
    private IEnumerator graduallyReduceShake(float tiempo, Cinemachine.CinemachineBasicMultiChannelPerlin noise)
    {
        float _time = tiempo;
        float reductionFactor = noise.m_AmplitudeGain / _time;
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            noise.m_AmplitudeGain -= reductionFactor * Time.deltaTime;
            noise.m_FrequencyGain -= reductionFactor * Time.deltaTime;
            yield return null;
        }
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
    public void shake(float sustainTime, float decayTime, float intensity)
    {
        m_CinemachineImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = sustainTime;
        m_CinemachineImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = decayTime;
        m_CinemachineImpulseSource.m_ImpulseDefinition.m_AmplitudeGain = intensity;
        m_CinemachineImpulseSource.GenerateImpulse();
    }
    //[ContextMenu("Ejecutar funcion A")]

    //public void TEST()
    //{
    //    shake(1,1);
    //}
}
