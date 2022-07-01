using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataDanio : MonoBehaviour
{
    
    [SerializeField] public GLOBAL_TYPE.TIPO_DANIO tipo_danio;
    [SerializeField] public int danio;
    [SerializeField] private float impactoEmpuje;
    public Transform m_transformAtacante;
    private GLOBAL_TYPE.LADO m_lado;

    private void Start()
    {
        m_transformAtacante = transform;
    }
    public int getDanio()
    {
        return danio;
    }
    public GLOBAL_TYPE.LADO getLado() { return m_lado; }
    public void setLado(GLOBAL_TYPE.LADO valor) { m_lado = valor; }
    public float getImpactoEmpuje() { return impactoEmpuje; }
    public void setTransform(Transform valor) { m_transformAtacante = valor; }
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
