using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class RB_Utilities : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    bool velocityActive;
    Vector2 velocity_direction;
    float velocity_potencia;
    private void Update()
    {
        if (velocityActive)
        {
            rb.velocity = velocity_direction * velocity_potencia * Time.deltaTime;
        }
    }

    public Vector2 _test_vector2;
    public float _testpotencia;
    [Button("ApplyForce")]
    public void TEST_Force()=>ApplyForce(_test_vector2, _testpotencia);
    
    [Button("ApplyVelocity")]
    public void TEST_VELOCITY()
    {
        ActiveVelocity(true);
        SetVelocity(_test_vector2, _testpotencia);
    }



    public void ApplyForce(Vector2 direction, float potencia)
    {
        rb.AddForce(direction.normalized * potencia, ForceMode2D.Impulse);
    }

    public void CancelForceAndVelocity()=>rb.velocity = Vector2.zero;
    
    public void ActiveVelocity(bool state)
    {
        velocityActive = state;
    }
    public void SetVelocity(Vector2 direction, float potencia)
    {
        if (velocityActive)
        {
            velocity_direction = direction;
            velocity_potencia = potencia;
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.cyan;

        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)rb.velocity;/* * m_rb.velocity.magnitude/10f*/;

        // Línea principal
        Gizmos.DrawLine(start, end);
        // Flecha
        Vector3 dir = (end - start).normalized;
        float size = 1.25f;
        Vector3 right = Quaternion.Euler(0, 0, 150) * dir * size;
        Vector3 left = Quaternion.Euler(0, 0, -150) * dir * size;
        Gizmos.DrawLine(end, end + right);
        Gizmos.DrawLine(end, end + left);
    }
}
