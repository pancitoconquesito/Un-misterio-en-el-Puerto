using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMagnesis : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private float minVelocidadDanio;
    [SerializeField] private BoxCollider2D _Ref_boxColliderDanio;
    [SerializeField] private Rigidbody2D m_rigibody;
    [SerializeField] private bool DanioContinuo=false;
    //[SerializeField] private bool m_returnGravity=true;

    //public bool ReturnGravity { get => m_returnGravity;}

    bool tomado = false;
    public void Tomado(bool value)
    {
        if (value)
        {
            SetFreezeRotationZ(false);
        }
        tomado = value;
    }
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
        if (!DanioContinuo)
        {
            if(m_rigibody.velocity.sqrMagnitude > minVelocidadDanio)
            {
                _Ref_boxColliderDanio.enabled = true;
            }else _Ref_boxColliderDanio.enabled = false;
        }

        if (!tomado && m_rigibody.velocity.magnitude < 0.1f)
        {
            SetFreezeRotationZ(true);
        }
    }

    public void SetFreezeRotationZ(bool freeze)
    {
        if (freeze)
            m_rigibody.constraints |= RigidbodyConstraints2D.FreezeRotation;
        else
            m_rigibody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
    }
}
