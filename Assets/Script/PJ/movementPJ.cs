using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class movementPJ : MonoBehaviour
{
    [Header("-- Basic param --")]
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float limiteInput_movX;
    [SerializeField] private float velocidadHorizontal = 4f;
    [SerializeField] private float potenciaSalto = 9f;
    //[SerializeField] private float caida = 10f;
    //[SerializeField]private float gravedad = -9.8f;
    private bool vivo = true, move = true;

    private Vector3 movimientoHorizontal;

    [SerializeField] public float acceleration;
    [SerializeField] public float decceleration;
    private float velPower=1;
    [SerializeField] private grounded m_grounded;
    [SerializeField] private changeMirada m_changeMirada;

    [SerializeField] private staminaPsiquica m_staminaPsiquica;
    [SerializeField] private float maximoVelocidadCaida;

    [Header("-- Run --")]
    [SerializeField]private float factorRun;
    private bool isRun = false;


    [Header("-- Dash --")]
    [SerializeField] private dashPJ m_dash;
    [SerializeField]private float timeInDash;
    private float current_timeInDash;
    [SerializeField] private float timeBeetweenDash;
    private float current_timeBeetweenDash;
    [SerializeField] float timeDashWithOutGravity;
    private float current_timeDashWithOutGravity;



    [Header("-- Shoot/Sword --")]
    [SerializeField] private float timeShoot;
    private float current_timeShoot;
    [SerializeField] private ShootSword m_shootSword;
    [SerializeField] private float costoSword;

    [Header("-- Magnesis --")]
    [SerializeField] private magnesis m_magnesis;

    private NewControls m_ControlPJ;

    [Header("-- Inventario --")]
    [SerializeField] private inventaryChsngePanels m_inventario;

    [Header("-- Estado --")]
    [SerializeField] private GLOBAL_TYPE.ESTADOS m_ESTADO= GLOBAL_TYPE.ESTADOS.movementNormal;
    [SerializeField] private respawnPJ m_respawnPJ;

    [Header("-- polvo movimiento --")]
    [SerializeField] private ObjectPooling m_ObjectPooling;
    [SerializeField] private float cadenciaParticulasPolvo_base;
    [SerializeField] private float cadenciaParticulasPolvo_min;
    [SerializeField] private float cadenciaParticulasPolvo_max;
    private float current_cadenciaParticulasPolvo=0;
    private void Awake()
    {
        //setControls();
        m_staminaPsiquica.setCosteEspadazo(costoSword);
    }

    private void Start()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.entrandoScene;
    }
    private void setControls()
    {
        m_ControlPJ = new NewControls();
        m_ControlPJ.PLAYER.Enable();

        m_ControlPJ.PLAYER.Jump.performed += jump;
        m_ControlPJ.PLAYER.Jump.canceled += detenerJump;

        m_ControlPJ.PLAYER.Movement_Axis_LEFT.performed += ctx => getInput_Axis_LEFT(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
        m_ControlPJ.PLAYER.Movement_Axis_LEFT.canceled += setZerotInput_Axis_LEFT;

        m_ControlPJ.PLAYER.Run.performed += startRun;
        m_ControlPJ.PLAYER.Run.canceled += cancelRun;

        m_ControlPJ.PLAYER.Dash.started += dash;

        m_ControlPJ.PLAYER.Shoot.started += shoot;

        m_ControlPJ.PLAYER.Magnesis.started += startMagnesis;
        m_ControlPJ.PLAYER.Magnesis.canceled += cancelarMagnesis;

        m_ControlPJ.PLAYER.Inventary.started += startInventary;
    }

    //private void OnEnable() { if (m_ControlPJ == null) setControls(); }
    private void desactivarControles()
    {
        //if (m_ControlPJ != null) m_ControlPJ.PLAYER.Disable();
        if (m_ControlPJ != null)
        {
            m_ControlPJ.PLAYER.Disable();
            m_ControlPJ = null;
        }
    }
    private void OnDisable() { desactivarControles(); }
    public void enterConversacion()
    {
        m_rigidbody.velocity = -Vector2.zero;
        m_ESTADO = GLOBAL_TYPE.ESTADOS.interactuar;
        m_animator.SetBool("interactuar_hablar", true);
        m_animator.SetTrigger("interactuar_hablar_trigger");
    }
    public void salirConversacion()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        m_animator.SetBool("interactuar_hablar", false);
    }
    private void startInventary(InputAction.CallbackContext ctx)
    {
        if (GLOBAL_TYPE.canInventario(m_ESTADO))
        {
            if(m_ESTADO != GLOBAL_TYPE.ESTADOS.inventario)
            {
                m_rigidbody.velocity = Vector2.zero;
                if (m_isGrounded && m_ESTADO == GLOBAL_TYPE.ESTADOS.magnesis)
                {
                    m_ESTADO = GLOBAL_TYPE.ESTADOS.inventario;
                    m_animator.SetBool("salirMagnesis", true);
                    m_magnesis.detenerRayo();
                }
                m_animator.SetBool("inventarioBool", true);
                m_animator.SetTrigger("inventario");

                m_ESTADO = GLOBAL_TYPE.ESTADOS.inventario;
                m_inventario.activarInventario();
            }
            else
            {
                m_animator.SetBool("inventarioBool", false);
                m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
                m_inventario.desactivarInventario();
            }
        }
    }
    private void startMagnesis(InputAction.CallbackContext ctx)
    {
        if(m_isGrounded && m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal)
        {
            
            m_ESTADO = GLOBAL_TYPE.ESTADOS.magnesis;
            m_animator.SetBool("salirMagnesis", false);
            m_magnesis.lanzarRayo(m_changeMirada.getMirada());
        }

    }
    private void cancelarMagnesis(InputAction.CallbackContext ctx)
    {
        m_animator.SetBool("salirMagnesis", true);
        m_magnesis.detenerRayo();
        //detenerMagnesis();
        m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
    }
    public void detenerMagnesis()
    {
        if(m_ESTADO != GLOBAL_TYPE.ESTADOS.muerto && m_ESTADO != GLOBAL_TYPE.ESTADOS.inventario && m_ESTADO!= GLOBAL_TYPE.ESTADOS.interactuar)
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
    }

    public void detenerMagnesisSinStamina()
    {
        m_animator.SetBool("salirMagnesis", true);
        m_magnesis.detenerRayo();
        //detenerMagnesis();
        m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
        if (m_ESTADO != GLOBAL_TYPE.ESTADOS.muerto && m_ESTADO != GLOBAL_TYPE.ESTADOS.inventario && m_ESTADO != GLOBAL_TYPE.ESTADOS.interactuar)
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
    }
    private void shoot(InputAction.CallbackContext ctx)
    {
        if (GLOBAL_TYPE.canShoot(m_ESTADO) && current_timeShoot<0.1f && m_staminaPsiquica.getCantidadStamina()>costoSword && m_staminaPsiquica.puedeEspadazo())
        {
            m_animator.SetTrigger("sword");
            current_timeShoot = timeShoot;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.sword;
            m_staminaPsiquica.addStamina(-costoSword);
            //m_shootSword.startSword(valorInput_Vertical);
        }
    }

    private void dash(InputAction.CallbackContext ctx)
    {
        if (current_timeInDash<0.1f && current_timeBeetweenDash<0.1f && GLOBAL_TYPE.canDash(m_ESTADO) && m_staminaPsiquica.puedeDash())
        {
            m_animator.SetTrigger("dash");
            m_rigidbody.velocity = Vector2.zero;
            current_timeDashWithOutGravity = timeDashWithOutGravity;
            current_timeInDash = timeInDash;
            current_timeBeetweenDash = timeBeetweenDash;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.dash;
            m_dash.startDash(m_changeMirada.getMirada());
        }
    }
    private void startRun(InputAction.CallbackContext ctx)
    {
        if(!isRun)
            m_animator.SetBool("run", true);
        isRun = true;
    }
    private void cancelRun(InputAction.CallbackContext ctx)
    {
        isRun = false;
        m_animator.SetBool("run", false);
    }
    

    public void swordFinished()
    {
        //print("aaaa");
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.sword)
        {
            current_timeShoot = 0;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        }
    }

    void Update()
    {
        current_timeInDash -= Time.deltaTime;
        if (current_timeInDash < 0.1f && m_ESTADO == GLOBAL_TYPE.ESTADOS.dash)
        {
            current_timeInDash = 0;
            //if (m_ESTADO == GLOBAL_TYPE.ESTADOS.dash)
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        }

        current_timeBeetweenDash -= Time.deltaTime;
        if (current_timeBeetweenDash < 0.1f)    current_timeBeetweenDash = 0;

        current_timeDashWithOutGravity -= Time.deltaTime;
        if(current_timeDashWithOutGravity < 0.1f)
        {
            current_timeDashWithOutGravity = 0;
        }
        else
        {
            m_rigidbody.velocity= new Vector2(m_rigidbody.velocity.x, 3);
        }

        /*
        current_timeShoot -= Time.deltaTime;
        if (current_timeShoot < 0.1f && m_ESTADO == GLOBAL_TYPE.ESTADOS.sword)
        {
            current_timeShoot = 0;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        }*/

        current_cadenciaParticulasPolvo -= Time.deltaTime;
        if (current_cadenciaParticulasPolvo < -100) current_cadenciaParticulasPolvo = -100;
        if (move)
        {
            m_isGrounded = m_grounded.isGrounded();
            m_animator.SetBool("ground", m_isGrounded);

            if (vivo && GLOBAL_TYPE.canChangeMirada(m_ESTADO))
            {
                m_changeMirada.miradaPj(valorInput_Horizontal);
                if (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal)
                {
                    m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
                    particulasDePolvo();

                }
                else
                    m_animator.SetFloat("Velocity_X", 0);
            }
            m_animator.SetFloat("velocity_Y", m_rigidbody.velocity.y);

        }
        else
        {
            m_rigidbody.velocity = m_rigidbody.velocity*0.2f;

        }

        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.magnesis)
        {
            m_rigidbody.velocity = m_rigidbody.velocity * 0.1f;
        }



        /*
        float laserLength = 5f;
        Vector2 posicion = new Vector2(transform.position.x, transform.position.y-2.2f);
        RaycastHit2D hit = Physics2D.Raycast(posicion, Vector2.down, laserLength);
        if (hit.collider != null)
        {
            Debug.DrawRay(posicion, Vector2.down * laserLength, Color.yellow);
            if (hit.collider.CompareTag("Plataform"))
            {
                print("angulo " + hit.collider.gameObject.transform.rotation.z);
                objetoSuelo = hit.collider.gameObject.transform.right;
            }
            else
            {
                print("noup "+hit.collider.name);
            }
        }
        */

    }
    private Vector3 objetoSuelo;
    private void particulasDePolvo()
    {
        if (m_isGrounded && current_cadenciaParticulasPolvo < 0 && Mathf.Abs(valorInput_Horizontal) > 0.5f)
        {
            if (isRun)
            {
                current_cadenciaParticulasPolvo = cadenciaParticulasPolvo_base + Random.Range(cadenciaParticulasPolvo_min, cadenciaParticulasPolvo_max);
                m_ObjectPooling.emitirObj(1.5f, transform.position);
            }
            else
            {
                current_cadenciaParticulasPolvo = cadenciaParticulasPolvo_base * 2 + Random.Range(cadenciaParticulasPolvo_min, cadenciaParticulasPolvo_max) * 2;
                m_ObjectPooling.emitirObj(1.5f, transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (move && vivo && m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal && current_timeDashWithOutGravity < 0.1f)
        {
            float currentFactorRun = 1;
            if (isRun) currentFactorRun = factorRun;
            float factorSalto = 0.7f;
            if (m_isGrounded) factorSalto = 1;
            float targetSpeed = valorInput_Horizontal * velocidadHorizontal * currentFactorRun * factorSalto;

            //m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput));

            float speedDif = targetSpeed - m_rigidbody.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);


            

            m_rigidbody.AddForce(movement * Vector2.right);
            //m_rigidbody.AddForce(movement * objetoSuelo);
        }

        movimientoEntradaStage();
        //print("VELOCIDAD CAIDA : "+m_rigidbody.velocity.y);

        if(m_rigidbody.velocity.y < maximoVelocidadCaida)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, maximoVelocidadCaida);
        }
    }

    private void movimientoEntradaStage()
    {
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            switch (m_tipoEntrada)
            {
                case GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay:
                    {
                        //m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_caminando:
                    {
                        m_changeMirada.miradaPj(1);
                        m_rigidbody.velocity = Vector2.right * 5f;
                        m_animator.SetFloat("Velocity_X", 1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_caminando:
                    {
                        m_changeMirada.miradaPj(-1);
                        m_rigidbody.velocity = Vector2.left * 5f;
                        m_animator.SetFloat("Velocity_X", 1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo:
                    {
                        m_changeMirada.miradaPj(-1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo:
                    {
                        m_changeMirada.miradaPj(1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_salto:
                    {
                        if (!saltoInicial)
                        {
                            saltoInicial = true;
                            m_animator.SetTrigger("startJump");
                            m_changeMirada.miradaPj(-1);
                            m_rigidbody.velocity = new Vector2(-12, potenciaSalto*0.8f);
                        }
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_salto:
                    {

                        if (!saltoInicial) {
                            saltoInicial = true;
                            m_animator.SetTrigger("startJump");
                            m_changeMirada.miradaPj(1);
                            m_rigidbody.velocity = new Vector2(12, potenciaSalto*0.8f);
                        }
                        break;
                    }
            }
        }
    }
    private bool saltoInicial = false;
    private float valorInput_Horizontal, valorInput_Vertical;
    private void getInput_Axis_LEFT(float currentValor_X, float valorInput_Vertical)
    {
        //if (ctx.performed)
        //{
            //float currentValor = ctx.ReadValue<Vector2>().x;
            if(Mathf.Abs(currentValor_X) > limiteInput_movX)
                valorInput_Horizontal = currentValor_X;
            else valorInput_Horizontal = 0;
        //}
        if(m_ESTADO==GLOBAL_TYPE.ESTADOS.movementNormal)
            m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
        else
            m_animator.SetFloat("Velocity_X", 0);
        //valorInput_Vertical = ctx.ReadValue<Vector2>().y;
        m_animator.SetFloat("Axis_Y", valorInput_Vertical);


        //print("valorInput_Vertical : "+ valorInput_Vertical);
    }
    private void setZerotInput_Axis_LEFT(InputAction.CallbackContext ctx)
    {
        valorInput_Horizontal = 0;
        m_animator.SetFloat("Velocity_X", 0);
        valorInput_Vertical = 0;
        m_animator.SetFloat("Axis_Y", 0);
    }
    private bool m_isGrounded = false;
    private void jump( InputAction.CallbackContext ctx )
    {
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal && move && vivo && m_isGrounded)
        {
            m_animator.SetTrigger("startJump");
            m_rigidbody.AddForce(Vector2.up * potenciaSalto, ForceMode2D.Impulse);
        }
    }

    private void detenerJump(InputAction.CallbackContext ctx)
    {
        m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y * 0.5f);
    }

    public bool puedeConversar()
    {
        return m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal;
    }

    public void recibirDanio(dataDanio m_dataDanio, bool soloEmpuje)
    {
        float impacto;
        if (m_dataDanio.getLado() == GLOBAL_TYPE.LADO.iz) impacto = m_dataDanio.getImpactoEmpuje();
        else impacto = -m_dataDanio.getImpactoEmpuje();
        m_rigidbody.velocity = Vector3.zero;
        if (m_dataDanio.tipo_danio == GLOBAL_TYPE.TIPO_DANIO.lava) {
            m_rigidbody.velocity = new Vector2(0, m_rigidbody.velocity.y * 0.5f);
        }
        else
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x + impacto, m_rigidbody.velocity.y * 0.5f);
        }
        if (!soloEmpuje)
        {
            m_ESTADO = GLOBAL_TYPE.ESTADOS.danio;
            m_animator.SetTrigger("danio");
        }
        //m_animator.SetBool("danioBool", true);
    }
    public void quitarDanio()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        //m_animator.SetBool("danioBool", false);
    }
    public void morir()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.muerto;
        m_animator.SetTrigger("morir");
        //m_magnesis.enabled = false;
        m_rigidbody.velocity = Vector2.zero;
        move = false;
        m_respawnPJ.respawn();
        

    }

    public void setCambioMirada(GLOBAL_TYPE.LADO newMirada)
    {
        if(newMirada != m_changeMirada.getMirada())
        {
            if (newMirada == GLOBAL_TYPE.LADO.iz) m_changeMirada.miradaPj(-1);
            else m_changeMirada.miradaPj(1);
        }
        
    }

    ////////TEST//////////////
    ///

    public GLOBAL_TYPE.ESTADOS test_getEstado()
    {
        return m_ESTADO;
    }
    public void activarMovimiento()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        setControls();
    }
    private GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;
    public void setTipoEntrada(GLOBAL_TYPE.TIPO_ENTRADA tipoEntrada)
    {
        m_tipoEntrada = tipoEntrada;
        if(m_ESTADO != GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            if (m_ControlPJ != null) { 
                m_ControlPJ.PLAYER.Disable();
                m_ControlPJ = null;
            }
            m_ESTADO = GLOBAL_TYPE.ESTADOS.entrandoScene;
        }
    }

    public void cogerItem()
    {
        desactivarControles();
        m_ESTADO = GLOBAL_TYPE.ESTADOS.cogerItem;
        m_rigidbody.velocity = Vector2.zero;
    }
}
