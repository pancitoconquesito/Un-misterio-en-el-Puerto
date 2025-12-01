using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAlertConfig : MonoBehaviour
{
    [SerializeField] Estrategias_Config Estrategias_Config;
    [SerializeField] int indexArea;
    [SerializeField] bool isUltimaArea;
    Check_areaCollider check_areaCollider;
    bool isDentro = false;

    public bool IsDentro { get => isDentro; set => isDentro = value; }

    void Awake()
    {
        check_areaCollider = GetComponent<Check_areaCollider>();
        check_areaCollider.OnColision += OnColision;
        check_areaCollider.OnExitColision += OnExitColision;
    }
    public void OnColision()
    {
        if (isUltimaArea)
        {
            Estrategias_Config.OnEstrategia();
            isDentro = true;
        }
    }
    public void OnExitColision()
    {
        if (isUltimaArea)
        {
            Estrategias_Config.ExitEstrategia();
            isDentro = false;
        }
    }
    private void OnDisable()
    {
        check_areaCollider.OnColision -= OnColision;
    }
}
