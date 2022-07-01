using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashPJ : MonoBehaviour
{
    [Header("-- Param --")]
    [SerializeField] private Rigidbody2D rigiPJ;
    [SerializeField] private float potenciaDash;
    [SerializeField] private float costoStamina;
    [SerializeField] private staminaPsiquica m_stamina;

    private void Awake()
    {
        //TEST_TEST
        
        m_stamina.setCosteDash(costoStamina);
    }
    public void startDash(GLOBAL_TYPE.LADO lado)
    {
        if(m_stamina.puedeDash())
        {
            m_stamina.addStamina(-costoStamina);

            int _lado;
            if (lado == GLOBAL_TYPE.LADO.iz)
            {
                _lado = -1;
            }
            else
            {
                _lado = 1;
            }
            rigiPJ.velocity = Vector2.zero;
            rigiPJ.velocity = new Vector2(_lado, 0) * potenciaDash;

        }
        
    }
}
