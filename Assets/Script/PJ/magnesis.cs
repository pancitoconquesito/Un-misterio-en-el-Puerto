using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Magnesis : MonoBehaviour
{
    [SerializeField] private float timeRayo;
    private float current_timeRayo;
    [SerializeField] private float speedRayo;
    private bool activado = false;
    //[SerializeField] private Rigidbody2D m_rigidbody_rayo;
    [SerializeField] private Transform m_transform_magnesis;
    [SerializeField] private GameObject m_magnesis_GO;
    [SerializeField] private movementPJ m_movementPJ;
    [SerializeField] private Collider2D m_colliderRayo;

    private int idLeanMovement_X, idLeanMovement_Y;
    private NewControls m_Control_Magnesis;
    [SerializeField] private float distanciaMaxima;
    [SerializeField] private Animator m_aniamtorPJ;


    [SerializeField] private SpriteRenderer sp_rayoCapturador;

    [SerializeField] private float costeInicialStamina;
    [SerializeField] private float costeMagnesis;
    [SerializeField] private staminaPsiquica m_staminaPsiquica;

    [SerializeField] private Animator m_aniamtorRayo;
    [SerializeField] private ParticleSystem m_particlesMAgnesis_obj;
    [SerializeField] private rayoMagnesis m_rayoMagnesis;
    [SerializeField] private float m_ptenciadorMagnesis;
    private void activarControles()
    {
        
        m_Control_Magnesis = new NewControls();
        //m_Control_Magnesis.MAGNESIS.Enable();

        m_Control_Magnesis.MAGNESIS.Move.performed += setValorInputMagnesis;
        m_Control_Magnesis.MAGNESIS.Move.canceled += cancelValorInputMagnesis;


        m_staminaPsiquica.setCosteMagnesis(costeMagnesis);
        m_staminaPsiquica.setCosteInicialMagnesis(costeInicialStamina);
    }
    private void desactivarControles()
    {
        if (m_Control_Magnesis != null) { 
            m_Control_Magnesis.MAGNESIS.Disable();
            m_Control_Magnesis = null;
        }
    }
    private void Start()
    {
        m_particlesMAgnesis_obj.Stop();
    }
    private void setValorInputMagnesis(InputAction.CallbackContext ctx)
    {
        moveTargetVector = ctx.ReadValue<Vector2>();
    }
    private void cancelValorInputMagnesis(InputAction.CallbackContext ctx)
    {
        if (m_Control_Magnesis != null)
            moveTargetVector = Vector2.zero;
    }
    //private void OnEnable(){m_Control_Magnesis.MAGNESIS.Enable();}
    private void OnDisable(){ desactivarControles(); }
    void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
    private void FixedUpdate()
    {
        
        if (activado)
        {
            LeanTween.cancel(idLeanMovement_X);
            idLeanMovement_X =LeanTween.moveLocalX(m_magnesis_GO, m_transform_magnesis.localPosition.x+0.1f*speedRayo, Time.fixedDeltaTime).id;
            LeanTween.cancel(idLeanMovement_Y);
            idLeanMovement_Y = LeanTween.moveLocalY(m_magnesis_GO, m_transform_magnesis.localPosition.y + moveTargetVector.y/10 * speedRayo, Time.fixedDeltaTime).id;
        }

        if (target)
        {
            float curr_distanciaMaxima = (transform.position - m_rigidbody_target.transform.position).magnitude;
            if (m_staminaPsiquica.puedeMagnesis() && curr_distanciaMaxima < distanciaMaxima)
            {
                m_rigidbody_target.AddForce(moveFinal * speedMoveTarget * m_ptenciadorMagnesis);
                float orqueX = moveFinal.normalized.x;
                m_rigidbody_target.AddTorque(orqueX * 100f);
                m_staminaPsiquica.addStamina(-costeMagnesis* Time.fixedDeltaTime);
            }
            else
            {
                moveTargetVector = Vector2.zero;
                m_movementPJ.detenerMagnesisSinStamina();
                //detenerRayo();
            }
        }
    }
    private void Update()
    {
        if (activado)
        {
            current_timeRayo -= Time.deltaTime;
            if (current_timeRayo < 0.1f)
            {
                //m_aniamtorPJ.SetTrigger("exitMagnesis");
                m_aniamtorPJ.SetBool("salirMagnesis", true);
                current_timeRayo = 0;
                detenerRayo();
            }
            //LeanTween.cancel(idLeanMovement_X);
            //idLeanMovement_X = LeanTween.moveLocalX(m_magnesis_GO, m_transform_magnesis.localPosition.x + 0.1f * speedRayo, Time.deltaTime).id;
            //LeanTween.cancel(idLeanMovement_Y);
            //idLeanMovement_Y = LeanTween.moveLocalY(m_magnesis_GO, m_transform_magnesis.localPosition.y + moveTargetVector.y / 10 * speedRayo, Time.deltaTime).id;
        }

        if (target)
        {

            //print("velocity sqrMagnitude de target : " + m_rigidbody_target.velocity.sqrMagnitude);
            /*
            if(m_rigidbody_target.velocity.sqrMagnitude > currentMinVelocidadDanio)
            {
                boxCollider_danioVelocidadTarget.enabled = true;
            }
            else
            {
                boxCollider_danioVelocidadTarget.enabled = false;
            }*/

            moveFinal = moveTargetVector;
        }
    }
    //private float currentMinVelocidadDanio;
    //private BoxCollider2D boxCollider_danioVelocidadTarget=null;
    private Vector2 moveFinal = Vector2.zero;
    private Vector2 moveTargetVector;//, moveTargetVectorInertia;
    private int dir;
    public void lanzarRayo(GLOBAL_TYPE.LADO lado)
    {
        activarControles();

        sp_rayoCapturador.enabled = true;
        m_aniamtorRayo.SetTrigger("start");

        m_aniamtorPJ.SetBool("salirMagnesis", false);
        //print("lanzarRayo");
        //m_aniamtorPJ.SetBool("magnesisCaptura", true);
        m_aniamtorPJ.SetTrigger("magnesisCaptura");
        m_colliderRayo.enabled = true;
        m_Control_Magnesis.MAGNESIS.Enable();
        activado = true;
        current_timeRayo = timeRayo;

        dir = 1;
        if (lado == GLOBAL_TYPE.LADO.iz) dir = -1;
        
    }
    float originalGravityScale;
    public void detenerRayo()
    {
        if (m_rigidbody_target != null)
        {
            //if(m_objMagnesis.ReturnGravity)//m_returnGravity
                m_rigidbody_target.gravityScale = originalGravityScale;
            //else m_rigidbody_target.gravityScale = 1f;
        }
        LeanTween.cancel(idLeanMovement_X);
        m_transform_magnesis.localPosition = Vector2.zero;
        LeanTween.cancel(idLeanMovement_Y);
        moveTargetVector = Vector2.zero;
        current_timeRayo = 0;
        activado = false;
        m_movementPJ.detenerMagnesis();
        //if(boxCollider_danioVelocidadTarget!=null)
        //    boxCollider_danioVelocidadTarget.enabled = false;

        m_particlesMAgnesis_obj.Stop();
        m_particlesMAgnesis_obj.gameObject.transform.parent = null;
        m_particlesMAgnesis_obj.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);

        m_rayoMagnesis.setComplete(false);
        detenerTarget();
        desactivarControles();
        if (m_objMagnesis != null)
        {
            m_objMagnesis.Tomado(false);
        }
        m_objMagnesis = null;
    }
    private Rigidbody2D m_rigidbody_target = null;
    private bool target = false;
    float speedMoveTarget = 1f;
    private GameObject target_OBJ=null;
    public void targetLogrado(GameObject gameTarget)
    {
        desactivarControles();
        activarControles();
        if (m_staminaPsiquica.puedeCapturarConMagnesis())
        {

            m_particlesMAgnesis_obj.Play();
            m_particlesMAgnesis_obj.transform.gameObject.transform.parent = gameTarget.transform;
            m_particlesMAgnesis_obj.transform.position = gameTarget.transform.position;
            m_particlesMAgnesis_obj.transform.localScale = new Vector3(1f,1f,1f);

            m_staminaPsiquica.addStamina(-costeInicialStamina);

            //sp_rayoCapturador.enabled = false;
            m_aniamtorRayo.SetTrigger("end");

            m_aniamtorPJ.SetTrigger("magnesisControl");
            target_OBJ = gameTarget;
            m_colliderRayo.enabled = false;

            moveTargetVector = Vector2.zero;

            activado = false;
            LeanTween.cancel(idLeanMovement_X);
            LeanTween.cancel(idLeanMovement_Y);

            m_rigidbody_target = gameTarget.GetComponent<Rigidbody2D>();
            originalGravityScale = m_rigidbody_target.gravityScale;
            m_rigidbody_target.gravityScale = 0.1f;

            target = true;
            m_Control_Magnesis.MAGNESIS.Enable();

            m_objMagnesis = gameTarget.GetComponent<objMagnesis>();
            speedMoveTarget = m_objMagnesis.getSpeedMove();
            m_objMagnesis.Tomado(true);
            //currentMinVelocidadDanio = _objMagnesis.getMinVelocidadDanio();
            //boxCollider_danioVelocidadTarget = _objMagnesis.get_Ref_boxColliderDanio();
        }

    }
    objMagnesis m_objMagnesis;
    public void detenerTarget()
    {
        target = false;
        m_colliderRayo.enabled = false;
        sp_rayoCapturador.enabled = false;
    }
}
