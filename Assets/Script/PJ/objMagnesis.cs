using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMagnesis : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private float minVelocidadDanio;
    [SerializeField] private BoxCollider2D _Ref_boxColliderDanio;
    [SerializeField] private Rigidbody2D m_rigibody;
    public float getSpeedMove()
    {
        return speedMove;
    }
    public float getMinVelocidadDanio()
    {
        return minVelocidadDanio;
    }
    public BoxCollider2D get_Ref_boxColliderDanio()
    {
        return _Ref_boxColliderDanio;
    }

    private void Update()
    {
        if(m_rigibody.velocity.sqrMagnitude > minVelocidadDanio)
        {
            _Ref_boxColliderDanio.enabled = true;
        }else _Ref_boxColliderDanio.enabled = false;
    }
}
