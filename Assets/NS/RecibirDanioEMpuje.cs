using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibirDanioEMpuje : MonoBehaviour, IDamageable
{
    [SerializeField] bool isEnemy;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float factorEMpuje;
    [SerializeField] float cadencia;
    float curr_cadencia;

    GameObject go_pj;

    private void OnEnable()
    {
        curr_cadencia = 0;
    }
    private void Update()
    {
        if (curr_cadencia > -1)
        {
            curr_cadencia -= Time.deltaTime;
        }
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if (curr_cadencia > 0)
        {
            return false;
        }
        curr_cadencia = cadencia;
        if (go_pj == null)
        {
            go_pj = MASTER_REFERENCE.instance.GO_PJ;
        }
        if (rb != null)
        {
            rb.AddForce((transform.position - go_pj.transform.position) * m_dataDanio.danio * factorEMpuje, ForceMode2D.Impulse);
        }
        return true;
    }

}
