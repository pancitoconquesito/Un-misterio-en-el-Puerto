using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineStateDrivenCamera m_stateCam;
    [SerializeField] Animator at_camera;
    [SerializeField] cameraShake m_cameraShake;
    [SerializeField] CinemachineVirtualCamera m_CAM_medio_Gameplay;
    [SerializeField] CinemachineVirtualCamera m_CAM_muerte;
    [SerializeField] CinemachineVirtualCamera m_CAM_cerca;
    CinemachineVirtualCamera m_curr_CAM;
    List<CinemachineVirtualCamera> cameras;

    GameObject m_pivote;

    private void Awake()
    {
        m_curr_CAM = m_CAM_medio_Gameplay;
        cameras = new List<CinemachineVirtualCamera>();
        cameras.Add(m_CAM_medio_Gameplay);
        cameras.Add(m_CAM_muerte);
        cameras.Add(m_CAM_cerca);

        m_pivote = m_curr_CAM.Follow.gameObject;
    }
    public void ResetCamPivote()
    {
        m_stateCam.Follow = null;
        foreach (var cam in cameras)
        {
            cam.Follow = null;
        }
    }
    public void ReactiveCamera()
    {
        m_stateCam.Follow = m_pivote.transform;
        foreach (var cam in cameras)
        {
            cam.Follow = m_pivote.transform;
        }
    }
    public void ShakeCamera(float amplitud, float frecuencia, float tiempo)
    {
        m_cameraShake.Shake(amplitud, frecuencia, tiempo, m_curr_CAM);
    }
    public void SetCameraMuerte()
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 1;
        }
        m_CAM_muerte.Priority = 10;
        m_curr_CAM = m_CAM_muerte;
        at_camera.SetTrigger("muerte");
    }
    public void SetCameraCerca_PAUSE()
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 1;
        }
        m_CAM_cerca.Priority = 10;
        m_curr_CAM = m_CAM_cerca;
        at_camera.SetTrigger("cerca");
    }
    public void SetCameraGameplay_normal()
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 1;
        }
        m_CAM_medio_Gameplay.Priority = 10;
        m_curr_CAM = m_CAM_medio_Gameplay;
        at_camera.SetTrigger("gameplay");
    }
}
