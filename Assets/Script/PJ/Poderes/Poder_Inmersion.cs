using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder_Inmersion : Generic_Poder, IPOWER
{
    [SerializeField] private float m_coste;
    //[SerializeField] private float potenciaInicio;

    private void Awake()
    {
        base.m_PowerManager.PODER_Inmersion = this;
    }
    void Start()
    {
        //cargar desde DATA}
        //m_coste
        m_PowerManager.StaminaPsiquica.Coste_Inmersion = m_coste;//change
        base.curr_cadencia = 0;
    }

    public void TryExecute(movementPJ m_movementPJ)
    {
        if (m_movementPJ.IsGroundedFunction() && base.TryExecutePower(m_movementPJ, m_coste))
        {
            Debug.Log("puede ejecutar poder | esta En el suelo: " + m_movementPJ.IsGroundedFunction());
            Execute(m_PowerManager.ChangeMirada.getMirada());
        }
        else
        {
            Debug.Log("Necesita estar en el suelo.");
        }
    }
    public void Execute(GLOBAL_TYPE.LADO _lado)
    {
        lado = _lado;
        base.ExecutePower(_lado);
        Debug.Log("Execute Inmersion!");
    }


    //public void _CreateBomba()
    //{
    //    Debug.Log("_CreateBomba Function");
    //    GameObject currBomba = m_ObjectPooling.emitirObj(tiempoBomba + 0.25f, m_transformPivote.position, true, true);
    //    currBomba.GetComponent<Bomba>().SetInitialValues(tiempoBomba);
    //    Rigidbody2D _bombarigi = currBomba.GetComponent<Rigidbody2D>();
    //    _bombarigi.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), 1) * potenciaInicio);
    //    float nr = Random.Range(30f, 80f);
    //    _bombarigi.AddTorque(nr);
    //}
}
