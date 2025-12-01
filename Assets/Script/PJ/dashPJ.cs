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

    [SerializeField] ParticleSystem m_particle;
    [SerializeField] ParticleSystem m_particle_2;
    [SerializeField] TrailRenderer m_TrailRenderer;
    private void Awake()
    {
        //TEST_TEST
        
        m_stamina.setCosteDash(costoStamina);
        //isDashing = false;
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
            m_particle.Play();
            m_particle_2.Play();
            m_TrailRenderer.enabled = true;
        }
        
    }
    public void StopParticles()
    {

        //if (m_particle.isPlaying)
        //{
        //    m_particle.Stop();
        //    m_particle_2.Stop();
        //}
        //rigiPJ.velocity = new Vector2(rigiPJ.velocity.x, 1);
        Invoke("StopParticles_delay", 0.2f);
    }
    public void StopParticles_delay()
    {
        if (m_TrailRenderer.enabled)
        {
            m_TrailRenderer.enabled = false;
        }
        //if (m_particle.isPlaying)
        //{
        //    m_particle.Stop();
        //    m_particle_2.Stop();
        //}
    }
    //bool isDashing;
    //public void Update()
    //{
    //    if (isDashing)
    //    {

    //    }
    //}

}
