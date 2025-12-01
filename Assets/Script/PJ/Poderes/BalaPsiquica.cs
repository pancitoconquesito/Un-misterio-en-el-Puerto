using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPsiquica : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_Rigidbody2D;
    [SerializeField] float m_velocity;
    PoolObjectForceObject m_PoolObjectForceObject;

    private void Awake()
    {
        m_PoolObjectForceObject = GetComponent<PoolObjectForceObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void StartMovement(GLOBAL_TYPE.LADO lado)
    {

        if (lado == GLOBAL_TYPE.LADO.iz)
        {
            m_Rigidbody2D.velocity = new Vector2(-m_velocity, 0);
            this.transform.localScale = new Vector2(-1,1);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(m_velocity, 0);
            Debug.Log("lado bala");
            this.transform.localScale = Vector2.one;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataform"))
        {
            m_PoolObjectForceObject.ForceReturnToPool();
            //transform.position = new Vector2(transform.position.x - 1000, transform.position.y - 1000);
        }
    }
}
