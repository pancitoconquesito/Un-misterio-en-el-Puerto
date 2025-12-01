using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataDanio : MonoBehaviour
{
    public enum TipoElementalDanioEnum
    {
        None, Electrico
    }
    public enum QuienEmiteDanio
    {
        Player, Enemy, ObjectScene
    }
    [SerializeField] private TipoElementalDanioEnum _TipoElementalDanio;
    [SerializeField] public GLOBAL_TYPE.TIPO_DANIO tipo_danio;
    [SerializeField] public int danio;
    [SerializeField] private float impactoEmpuje;
    [SerializeField] private QuienEmiteDanio m_QuienEmiteDanio;
    [SerializeField] private Vector3 m_positionCollision;

    internal void SetPositionCollision(Vector2 _point)
    {
        m_positionCollision = _point;
    }

    public Transform m_transformAtacante;

    private GLOBAL_TYPE.LADO m_lado;

    public QuienEmiteDanio QuienAtaca { get => m_QuienEmiteDanio; set => m_QuienEmiteDanio = value; }
    public Vector3 PositionCollision { get => m_positionCollision; set => m_positionCollision = value; }
    public TipoElementalDanioEnum TipoElementalDanio { get => _TipoElementalDanio; set => _TipoElementalDanio = value; }

    public GLOBAL_TYPE.LADO getLado() { return m_lado; }
    public void setLado(GLOBAL_TYPE.LADO valor) { m_lado = valor; }
    public float getImpactoEmpuje() { return impactoEmpuje; }
    public void setTransform(Transform valor) { m_transformAtacante = valor; }

    private void Start()
    {
        m_transformAtacante = transform;
    }
    public int getDanio()
    {
        return danio;
    }
    public void updateTransform()
    {
        m_transformAtacante = transform;
        //m_transformAtacante.position = newTransform;
    }
    public void updateTransform(Vector2 newTransform) {
        m_transformAtacante = transform;
        //m_transformAtacante.position = newTransform;
    }
}
