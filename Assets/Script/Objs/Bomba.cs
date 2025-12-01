using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Bomba : MonoBehaviour
{
    [SerializeField] private PoolObjectForceObject m_PoolObjectForceObject;
    //[SerializeField] private float tiempoExplosion;
    [SerializeField] private GameObject obj_explosion;
    [SerializeField][Tag] string tagExplota;
    private float curr_tiempo=0;
    private bool activado = false;

    public void SetInitialValues(float tiempo)
    {
        curr_tiempo = tiempo;
        activado = true;
        m_CameraController = MASTER_REFERENCE.instance.CameraController;
    }
    void Update()
    {
        if (!activado) return;
        if (curr_tiempo > 0) curr_tiempo -= Time.deltaTime;
        else
        {
            if (activado)
            {
                Explotar();
            }
        }
    }
    CameraController m_CameraController;

    public void Explotar()
    {
        m_CameraController.ShakeCamera(40, 1.5f, 1f);
        activado = false;
        Instantiate(obj_explosion, transform.position, Quaternion.identity);
        m_PoolObjectForceObject.ForceReturnToPool();
    }
}
