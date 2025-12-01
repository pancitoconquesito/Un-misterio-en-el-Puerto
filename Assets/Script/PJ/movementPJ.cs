using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using static GLOBAL_TYPE;

public class movementPJ : MonoBehaviour
{
    [Header("-- Basic param --")]
    [SerializeField] staminaPsiquica stamina;
    [SerializeField] private Animator m_at_arranar;
    [SerializeField] private AnimEvent m_animEventSP;
    private BoxCollider2D m_boxCollisionPlatform;
    private Rigidbody2D m_rigidbody_A;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float limiteInput_movX;
    [SerializeField] private float velocidadHorizontal = 4f;
    [SerializeField] private float potenciaSalto = 9f;
    [SerializeField] private float m_coyoteTime;
    [SerializeField] private float stopThreshold = 0.01f;
    float m_currentCoyoteTime;
    //[SerializeField] private float caida = 10f;
    //[SerializeField]private float gravedad = -9.8f;
    private bool vivo = true, move = true;
    private Vector3 movimientoHorizontal;
    [SerializeField] public float acceleration;
    [SerializeField] public float decceleration;

    float m_originalGravityScale;
    internal bool IsFalling()=> m_rigidbody_A.velocity.y < 0 && !m_isGrounded;
    private float velPower = 1;
    private grounded m_groundedClass;
    private changeMirada m_changeMirada;
    [SerializeField] private staminaPsiquica m_staminaPsiquica;
    [SerializeField] private float maximoVelocidadCaida;

    [Header("-- Run --")]
    [SerializeField] private float factorRun;
    private bool isRun = false;

    [Header("-- Dash --")]
    private dashPJ m_dash;
    [SerializeField] private float timeInDash;
    private float current_timeInDash;
    bool m_doingDash;
    [SerializeField] private float timeBeetweenDash;
    private float current_timeBeetweenDash;
    [SerializeField] float timeDashWithOutGravity;
    private float current_timeDashWithOutGravity;

    [Header("-- Shoot/Sword --")]
    [SerializeField] private float timeShoot;
    internal GLOBAL_TYPE.ESTADOS GetState() => m_ESTADO;
    private float current_timeShoot;
    [SerializeField] private ShootSword m_shootSword;
    [SerializeField] private float costoSword;



    [Header("-- Magnesis --")]
    private Magnesis m_magnesis;

    [Header("-- Dash invencible --")]
    [SerializeField] private DashInvencible m_DashInvencible;


    [Header("-- Inventario --")]
    [SerializeField] private InventaryChangePanels m_inventario;
    float cooldown_inventario=0.6f;
    float curr_cooldown_inventario=0;


    [Header("-- Estado --")]
    [SerializeField] private GLOBAL_TYPE.ESTADOS m_ESTADO;

    private NewControls m_ControlPJ;


    private RespawnPJ m_respawnPJ;

    [Header("-- polvo movimiento --")]
    private ObjectPooling m_ObjectPooling;
    [SerializeField] private float cadenciaParticulasPolvo_base;
    [SerializeField] private float cadenciaParticulasPolvo_min;
    [SerializeField] private float cadenciaParticulasPolvo_max;
    [SerializeField] private ObjectPooling m_op_Jump;
    [SerializeField] private ObjectPooling m_op_Jump_pared;
    [SerializeField] private GameObject m_pivoteSaltoGO;
    [SerializeField] private GameObject m_pivoteSaltoGO_pared;
    [SerializeField] private ObjectPooling m_ObjectPooling_PolvoGrande;
    private float current_cadenciaParticulasPolvo = 0;

    [Header("--POWERS MANAGER--")]
    [SerializeField] PowerManager m_PowerManager;

    [Header("--Escaleras--")]
    [SerializeField] float m_velocidadEscalera;
    [SerializeField] float factorRunStair;
    [SerializeField] ObjectPooling m_PO_Enter;
    [SerializeField] ObjectPooling m_PO_Exit;
    [SerializeField] Bool_checkRayscastWithEvent m_Bool_checkRayscastWithEvent;

    [Header("--Mapa--")]
    [SerializeField] PJ_MAPA m_PJ_MAPA;

    [Header("--Pared--")]
    [SerializeField] CheckerRayCast pared_ray;
    [SerializeField] float coolDown_JumpPared;

    [Header("- Others -")]
    float curr_factorRunStair = 1;
    bool m_isMoveablePlatform;
    CameraController m_CameraController;
    bool poder_CanDash;
    bool poder_telekinesis;
    private Vector2 inputLeftAxis;
    GLOBAL_TYPE.DIRECCIONES_4 dir_sword;
    float factorAirAttack;
    bool lastGrounded = false;
    float landedTimer = 0.1f;
    float curr_landedTimer = 0;
    bool firstLanded = false;
    bool landed = false;
    private float lastDir;
    private bool wasMoving;
    private Vector3 objetoSuelo;
    [SerializeField]attackAirForce_DATA attackAirForce_DATA_UP = new attackAirForce_DATA { count=0, minValue = 0f, maxValue = 0.75f, maxCount = 3 };
    attackAirForce_DATA attackAirForce_DATA_FRONT = new attackAirForce_DATA { count=0, minValue = 0f, maxValue = 0.2f, maxCount = 2 };
    attackAirForce_DATA attackAirForce_DATA_DOWN = new attackAirForce_DATA { count=0, minValue = 0f, maxValue = 0.15f, maxCount = 2 };
    private bool saltoInicial = false, subirPJ_L=false, subirPJ_R=false;
    private float valorInput_Horizontal, valorInput_Vertical;
    Vector2 valueInterno_Axis;
    private bool m_isGrounded = false;
    bool stopJumping = false;
    bool has_JumpWall = false;
    private GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;
    Stairs_UP curr_Stairs;
    float curr_coolDown_JumpPared;
    [ShowNonSerializedField] SUPER_ESTADO m_SUPER_ESTADO;


    public Animator AnimatorPJ { get => m_animator; }
    public DashInvencible DashInvencible { get => m_DashInvencible; set => m_DashInvencible = value; }
    public bool DoingDash { get => m_doingDash; set => m_doingDash = value; }
    internal bool IsGroundedFunction()=>m_isGrounded;
    public Vector2 InputLeftAxis { get { return inputLeftAxis; } }
    public bool Poder_CanDash { get => poder_CanDash; set => poder_CanDash = value; }
    public bool Poder_telekinesis { get => poder_telekinesis; set => poder_telekinesis = value; }
    public staminaPsiquica Stamina { get => stamina; set => stamina = value; }
    public bool Has_JumpWall { get => has_JumpWall; set => has_JumpWall = value; }

    void SetSuperEstado(SUPER_ESTADO estado)
    {
        m_SUPER_ESTADO = estado;
        m_animator.SetLayerWeight(GLOBAL_TYPE.GetLayerAnim(estado), 1);
    }

    private void Awake()
    {
        SetSuperEstado(SUPER_ESTADO.NORMAL);
        //setControls();
        m_boxCollisionPlatform = this.GetComponent<BoxCollider2D>();
        m_rigidbody_A = this.GetComponent<Rigidbody2D>();
        m_groundedClass = this.GetComponent<grounded>();
        m_changeMirada = this.GetComponent<changeMirada>();
        m_dash = this.GetComponent<dashPJ>();
        m_magnesis = this.GetComponent<Magnesis>();
        m_respawnPJ = this.GetComponent<RespawnPJ>();
        m_ObjectPooling = this.GetComponent<ObjectPooling>();
        m_staminaPsiquica.setCosteEspadazo(costoSword);
        m_originalGravityScale = m_rigidbody_A.gravityScale;
    }


    private void Start()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.entrandoScene;
        m_CameraController = MASTER_REFERENCE.instance.CameraController;
        Poder_CanDash = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.Has_Dash;
        Poder_telekinesis = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.Has_telekinesis;
        Has_JumpWall = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.Has_JumpWall;
        curr_coolDown_JumpPared = 0;
    }
    internal void IsEnabledFisicas(bool value)
    {
        m_rigidbody_A.velocity = Vector2.zero;
        m_rigidbody_A.isKinematic = !value;
        m_boxCollisionPlatform.enabled = value;
    }
    internal bool PuedeConversar()
    {
        return m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal;
    }
    internal bool PuedeRecuperarStamina()
    {
        return m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal 
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.interactuar//conversar
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.inventario
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.Mapa;
    }
    private void setControls()
    {
        m_ControlPJ = new NewControls();
        m_ControlPJ.PLAYER.Enable();

        m_ControlPJ.PLAYER.Jump.performed += jump;
        m_ControlPJ.PLAYER.Jump.canceled += detenerJump;
        m_ControlPJ.PLAYER.Movement_Axis_LEFT.performed += ctx => getInput_Axis_LEFT(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
        m_ControlPJ.PLAYER.Movement_Axis_LEFT_Button.performed += ctx => getInput_Axis_LEFT_button(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
        m_ControlPJ.PLAYER.Movement_Axis_LEFT.canceled += setZerotInput_Axis_LEFT;
        m_ControlPJ.PLAYER.Run.performed += startRun;
        m_ControlPJ.PLAYER.Run.canceled += cancelRun;
        m_ControlPJ.PLAYER.Dash.started += dash;
        m_ControlPJ.PLAYER.Sword.started += shoot;
        m_ControlPJ.PLAYER.Magnesis.started += startMagnesis;
        m_ControlPJ.PLAYER.Magnesis.canceled += cancelarMagnesis;
        m_ControlPJ.PLAYER.Inventary.started += startInventary;
        m_ControlPJ.PLAYER.POWER_EXECUTE.started += ctx => StartPower();
        m_ControlPJ.PLAYER.POWER_Left.started += ctx => Power_L();
        m_ControlPJ.PLAYER.POWER_Right.started += ctx => Power_R();
        m_ControlPJ.PLAYER.R_button.started += ctx => BtnCancel();
        m_ControlPJ.PLAYER.Mapa.started += ctx => Mapa();
    }
    internal void Mapa()
    {
        if (!m_PJ_MAPA.CanChangeState())
        {
            return;
        }
        if (GLOBAL_TYPE.CanMapa(m_ESTADO))
        {
            setZerotInput_Axis_LEFT();
            //comenzar mapa
            m_ESTADO = GLOBAL_TYPE.ESTADOS.Mapa;
            m_rigidbody_A.velocity = Vector2.zero;
            m_PJ_MAPA.StartMapa();
            //anim
            Debug.Log("Start MAPA");
        }
        else
        {
            if (m_ESTADO == GLOBAL_TYPE.ESTADOS.Mapa)
            {
                //salir del mapa
                m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
                m_rigidbody_A.velocity = Vector2.zero;
                m_PJ_MAPA.EndMapa(true);
                Debug.Log("End MAPA");
            }
        }
    }
    internal bool TryPoder_TeletransportacionPsiquica()
    {
        return (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal);
    }
    internal bool TryPoder_Bomba()
    {
        return (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal);
    }

    internal bool TryPoder_Disparo()
    {
        return (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal);
    }

    private void Power_L()
    {
        m_PowerManager.MovePower_toLeft();
    }
    private void Power_R()
    {
        m_PowerManager.MovePower_toRight();
    }
    private void StartPower()
    {
        if(m_ESTADO != GLOBAL_TYPE.ESTADOS.Stair)
        {
            m_PowerManager.ExecutePower();
        }
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
        m_rigidbody_A.velocity = -Vector2.zero;
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
        if (GLOBAL_TYPE.canInventario(m_ESTADO) && m_isGrounded && move && curr_cooldown_inventario<0)
        {
            if (m_ESTADO != GLOBAL_TYPE.ESTADOS.inventario)
            {
                curr_cooldown_inventario = cooldown_inventario;
                m_rigidbody_A.velocity = Vector2.zero;
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
                curr_cooldown_inventario = cooldown_inventario;
                m_animator.SetBool("inventarioBool", false);
                m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
                m_inventario.desactivarInventario();
            }
        }
    }
    private void startMagnesis(InputAction.CallbackContext ctx)
    {
        if (Poder_telekinesis && m_isGrounded && m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal)
        {
            m_ESTADO = GLOBAL_TYPE.ESTADOS.magnesis;
            m_animator.SetBool("salirMagnesis", false);
            m_magnesis.lanzarRayo(m_changeMirada.getMirada());
        }
    }
    private void cancelarMagnesis(InputAction.CallbackContext ctx)
    {
        if(m_ESTADO== GLOBAL_TYPE.ESTADOS.Stair || m_ESTADO!=GLOBAL_TYPE.ESTADOS.magnesis)
        {
            return;
        }
        m_animator.SetBool("salirMagnesis", true);
        m_magnesis.detenerRayo();
        //detenerMagnesis();
        m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
    }
    public void detenerMagnesis()
    {
        if (m_ESTADO != GLOBAL_TYPE.ESTADOS.muerto && m_ESTADO != GLOBAL_TYPE.ESTADOS.inventario && m_ESTADO != GLOBAL_TYPE.ESTADOS.interactuar)
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
        if (GLOBAL_TYPE.canShoot(m_ESTADO) && current_timeShoot < 0.1f && m_staminaPsiquica.getCantidadStamina() > costoSword && m_staminaPsiquica.puedeEspadazo())
        {
            Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_attack_start);
            UpdateAirAttackDATA();
            m_animator.SetTrigger("sword");
            current_timeShoot = timeShoot;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.sword;
            //m_staminaPsiquica.addStamina(-costoSword);
            m_CameraController.ShakeCamera(5, 5, 0.6f);

            //valueInterno_Axis
            if (valueInterno_Axis.y < -0.2f)
            {
                //abajo
                dir_sword = GLOBAL_TYPE.DIRECCIONES_4.down;
            }
            else if(valueInterno_Axis.y > 0.2f)
            {
                //arriba
                dir_sword = GLOBAL_TYPE.DIRECCIONES_4.up;
            }
            else
            {
                //frontal
                dir_sword = GLOBAL_TYPE.DIRECCIONES_4.right;//frontal
            }
        }
    }
    internal bool IsAtaqueAbajo()
    {
        return dir_sword == GLOBAL_TYPE.DIRECCIONES_4.down;
    }

    internal bool CanStartConversacion()
    {
        return m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal && m_isGrounded;
    }

    private void UpdateAirAttackDATA()
    {
        float valueY = m_animator.GetFloat("Axis_Y");
        if (Mathf.Abs(valueY) > 0.1f)
        {
            if (valueY < 0)//abajo
            {
                if (attackAirForce_DATA_DOWN.count < attackAirForce_DATA_DOWN.maxCount)
                {
                    factorAirAttack = attackAirForce_DATA_DOWN.maxValue - attackAirForce_DATA_DOWN.count * (attackAirForce_DATA_DOWN.maxValue - attackAirForce_DATA_DOWN.minValue) / attackAirForce_DATA_DOWN.maxCount;
                    factorAirAttack *= -Physics2D.gravity.y;
                }
                else
                {
                    attackAirForce_DATA_DOWN.count = attackAirForce_DATA_DOWN.maxCount;
                    factorAirAttack = maximoVelocidadCaida * 0.7f;
                }
                attackAirForce_DATA_DOWN.count = attackAirForce_DATA_DOWN.count + 1;
            }
            else//arriba
            {
                if (attackAirForce_DATA_UP.count < attackAirForce_DATA_UP.maxCount)
                {
                    factorAirAttack = attackAirForce_DATA_UP.maxValue - attackAirForce_DATA_UP.count * (attackAirForce_DATA_UP.maxValue - attackAirForce_DATA_UP.minValue) / attackAirForce_DATA_UP.maxCount;
                    factorAirAttack *= -Physics2D.gravity.y*.5f;
                }
                else
                {
                    attackAirForce_DATA_UP.count = attackAirForce_DATA_UP.maxCount;
                    factorAirAttack = maximoVelocidadCaida*0.7f;
                }
                attackAirForce_DATA_UP.count = attackAirForce_DATA_UP.count + 1;
            }
        }
        else
        {
            //frontal
            if (attackAirForce_DATA_FRONT.count < attackAirForce_DATA_FRONT.maxCount)
            {
                factorAirAttack = attackAirForce_DATA_FRONT.maxValue - attackAirForce_DATA_FRONT.count * (attackAirForce_DATA_FRONT.maxValue - attackAirForce_DATA_FRONT.minValue) / attackAirForce_DATA_FRONT.maxCount;
                factorAirAttack *= -Physics2D.gravity.y;
            }
            else
            {
                attackAirForce_DATA_FRONT.count = attackAirForce_DATA_FRONT.maxCount;
                factorAirAttack = maximoVelocidadCaida * 0.7f;
            }
            attackAirForce_DATA_FRONT.count = attackAirForce_DATA_FRONT.count + 1;
        }
    }

    private void dash(InputAction.CallbackContext ctx)
    {
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair)
        {
            ExitStair();
        }

        if (Poder_CanDash && current_timeInDash < 0.1f && current_timeBeetweenDash < 0.1f && GLOBAL_TYPE.canDash(m_ESTADO) && m_staminaPsiquica.puedeDash())
        {
            m_DashInvencible.IsEnableAbility = true;
            Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_dash);
            m_doingDash = true;
            m_animator.SetTrigger("dash");
            m_rigidbody_A.velocity = Vector2.zero;
            current_timeDashWithOutGravity = timeDashWithOutGravity;
            current_timeInDash = timeInDash;
            current_timeBeetweenDash = timeBeetweenDash;
            m_ESTADO = GLOBAL_TYPE.ESTADOS.dash;
            m_dash.startDash(m_changeMirada.getMirada());
            m_CameraController.ShakeCamera(3,5,0.3f);
            //m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, 0);
        }
    }
    
    private void startRun(InputAction.CallbackContext ctx)
    {
        curr_factorRunStair = factorRunStair;
        if (!isRun)
            m_animator.SetBool("run", true);
        isRun = true;
    }
    private void cancelRun(InputAction.CallbackContext ctx)
    {
        curr_factorRunStair = 1;

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

    //float timeScene = 0;
    private void landed_function()
    {
        if (move && curr_landedTimer < 0 && m_ESTADO!= GLOBAL_TYPE.ESTADOS.Mapa && m_ESTADO != GLOBAL_TYPE.ESTADOS.interactuar && m_ESTADO != GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            //Debug.Log("landed---");
            curr_landedTimer = landedTimer;
            if (firstLanded)
            {
                Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_landed_soft);
            }
            firstLanded = true;
            m_ObjectPooling_PolvoGrande.emitirObj(1f, transform.position -new Vector3(0,1.8f,0));
            m_at_arranar.SetTrigger("arranar");
        }
    }
    void Update()
    {
        DebugOverlay.Log("EstadoPJ", $"EstadoPJ: {m_ESTADO} | m_isGrounded: {m_isGrounded}");

        if (curr_cooldown_inventario > -1)
        {
            curr_cooldown_inventario -= Time.deltaTime;
        }
        if (m_ControlPJ!=null)
        {
            inputLeftAxis = m_ControlPJ.PLAYER.Movement_Axis_LEFT.ReadValue<Vector2>();
        }
        lastGrounded = m_isGrounded;
        m_isGrounded = m_groundedClass.isGrounded() && m_rigidbody_A.velocity.y <= 0;
        if (m_isGrounded)
        {
            if (!landed)
            {
                landed = true;
                landed_function();
            }
            if (m_ESTADO==GLOBAL_TYPE.ESTADOS.Stair && inputLeftAxis.y<0 && m_groundedClass.EsSueloSolido)
            {
                m_rigidbody_A.velocity = Vector2.zero;
                ExitStairToDown();
            }
        }
        else
        {
            landed = false;
        }
        m_animator.SetBool("ground", m_isGrounded);

        if (curr_landedTimer > -1)
        {
            curr_landedTimer -= Time.deltaTime;
        }

        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            //movimientoEntradaStage();
            return;
        }

        current_timeInDash -= Time.deltaTime;
        if (current_timeInDash < 0.1f && m_ESTADO == GLOBAL_TYPE.ESTADOS.dash)
        {
            current_timeInDash = 0;
            //if (m_ESTADO == GLOBAL_TYPE.ESTADOS.dash)
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
            m_dash.StopParticles();
        }

        current_timeBeetweenDash -= Time.deltaTime;
        if (current_timeBeetweenDash < 0.1f) current_timeBeetweenDash = 0;

        current_timeDashWithOutGravity -= Time.deltaTime;
        if (current_timeDashWithOutGravity < 0.1f)
        {
            current_timeDashWithOutGravity = 0;
        }
        else
        {
            m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, 3);
        }

        current_cadenciaParticulasPolvo -= Time.deltaTime;
        if (current_cadenciaParticulasPolvo < -100) current_cadenciaParticulasPolvo = -100;
        if (move)
        {
            m_isMoveablePlatform = m_groundedClass.isMoveablePlatform();
            if (m_isGrounded) {
                attackAirForce_DATA_DOWN.count = 0;
                attackAirForce_DATA_FRONT.count = 0;
                attackAirForce_DATA_UP.count = 0;
                m_currentCoyoteTime = 0;
            }
            else
            {
                m_currentCoyoteTime += Time.deltaTime;
            }

            if (vivo && GLOBAL_TYPE.canChangeMirada(m_ESTADO))
            {
                if (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal)
                {
                    m_changeMirada.miradaPj(valorInput_Horizontal, true);
                    m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
                    particulasDePolvo();
                }
                else//stair
                {
                    m_changeMirada.miradaPj(valorInput_Horizontal, false);
                    m_animator.SetFloat("Velocity_X", 0);
                }
            }
            m_animator.SetFloat("velocity_Y", m_rigidbody_A.velocity.y);

        }
        else
        {
            m_rigidbody_A.velocity = m_rigidbody_A.velocity * 0.2f;

        }

        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.magnesis)
        {
            m_rigidbody_A.velocity = m_rigidbody_A.velocity * 0.1f;
        }

        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair)
        {
            m_animator.speed = curr_factorRunStair;
        }
        else
        {
            m_animator.speed = 1;
        }

        // Comprueba si la velocidad es menor al umbral en ambas direcciones (x e y)
        if (m_rigidbody_A.velocity.magnitude < stopThreshold && m_ESTADO==GLOBAL_TYPE.ESTADOS.movementNormal)
        {
            if (wasMoving)
            {
                wasMoving = false;
                OnPlayerStop();
            }
        }
        else
        {
            wasMoving = true;
        }

        //pared
        if (Has_JumpWall)
        {
            Update_JumpWall();
        }
    }

    private void Update_JumpWall()
    {
        if(curr_coolDown_JumpPared > -1f)
        {
            curr_coolDown_JumpPared -= Time.deltaTime;
        }
        bool okPared = false;
        if (!m_isGrounded && pared_ray.IsColisionando && m_rigidbody_A.velocity.y <= 0f && curr_coolDown_JumpPared - (curr_coolDown_JumpPared * 0.5f) < 0)
        {
            if ((m_changeMirada.getMirada() == LADO.iz && valueInterno_Axis.x < -0.5f)
                ||
                (m_changeMirada.getMirada() == LADO.der && valueInterno_Axis.x > 0.5f))
            {
                okPared = true;
                if (m_ESTADO != ESTADOS.onWall)
                {
                    //wall por primera vez
                    m_animator.ResetTrigger("ExitWall");
                    m_animator.ResetTrigger("OnWall");
                    m_animator.SetTrigger("OnWall");
                }
                m_ESTADO = ESTADOS.onWall;
            }
        }
        if (!okPared && m_ESTADO == ESTADOS.onWall)
        {
            CancelarPared();
        }
    }

    void OnPlayerStop()
    {
        // Lógica a ejecutar cuando el player se detiene
        //Debug.Log("Player se ha detenido");
    }

    private void particulasDePolvo()
    {
        if (m_isGrounded && current_cadenciaParticulasPolvo < 0 && Mathf.Abs(valorInput_Horizontal) > 0.5f)
        {
            if (isRun)
            {
                current_cadenciaParticulasPolvo = cadenciaParticulasPolvo_base + Random.Range(cadenciaParticulasPolvo_min, cadenciaParticulasPolvo_max);
                m_ObjectPooling.emitirObj(1.5f, transform.position + new Vector3(0,-1,0));
            }
            else
            {
                current_cadenciaParticulasPolvo = cadenciaParticulasPolvo_base * 2 + Random.Range(cadenciaParticulasPolvo_min, cadenciaParticulasPolvo_max) * 2;
                m_ObjectPooling.emitirObj(1.5f, transform.position + new Vector3(0, -1, 0));
            }
        }
    }

    private void FixedUpdate()
    {
        //velocidades
        switch (m_ESTADO)
        {
            case GLOBAL_TYPE.ESTADOS.movementNormal:
                {
                    if (move && vivo && m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal && current_timeDashWithOutGravity < 0.1f)
                    {
                        float currentFactorRun = 1;
                        if (isRun) currentFactorRun = factorRun;
                        float factorSalto = 0.7f;
                        if (m_isGrounded) factorSalto = 1;

                        float targetSpeed = valorInput_Horizontal * velocidadHorizontal * currentFactorRun * factorSalto;
                        float speedDif = targetSpeed - m_rigidbody_A.velocity.x;
                        float accelRate = (Mathf.Abs(targetSpeed) > 0.1f) ? acceleration : decceleration;
                        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

                        m_rigidbody_A.AddForce(movement * Vector2.right);
                        //m_rigidbody.velocity = valorInput_Horizontal * 30 * Vector2.right;
                    }
                    //movimientoEntradaStage();//???
                    if (m_rigidbody_A.velocity.y < maximoVelocidadCaida)
                    {
                        m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, maximoVelocidadCaida);
                    }
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.herida:
            case GLOBAL_TYPE.ESTADOS.sword:
                {
                    if (m_isGrounded)//suelo
                    {
                        m_rigidbody_A.velocity = Vector2.zero;
                    }
                    else//aire
                    {
                        m_rigidbody_A.velocity = new Vector2(0, factorAirAttack);
                    }

                    break;
                }
            case GLOBAL_TYPE.ESTADOS.POWER_Inmersoin:
            case GLOBAL_TYPE.ESTADOS.POWER_Bomba:
                {
                    m_rigidbody_A.velocity = new Vector2(0, m_rigidbody_A.velocity.y);
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.dash:
                {
                    m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, m_rigidbody_A.gravityScale);
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.POWER_Quinto:
            case GLOBAL_TYPE.ESTADOS.POWER_Disparo: {
                    m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, 0);
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.Stair:
                {
                    //Debug.Log("Activar ESCALERA!");
                    m_rigidbody_A.velocity = new Vector2(0, inputLeftAxis.y * m_velocidadEscalera * curr_factorRunStair);
                    //m_animator.speed = curr_factorRunStair;
                    //!mover arriba 
                    //!mover abajo
                    //!estar detenido
                    //!salir
                    break;
                }
            case GLOBAL_TYPE.ESTADOS.onWall:
                {
                    m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, 0);
                    break;
                }

        }
    }
    //!
    public void CancelarPared()
    {
        m_ESTADO = ESTADOS.movementNormal;
        m_animator.SetTrigger("ExitWall");
    }

    [System.Serializable]
    struct attackAirForce_DATA
    {
        public int count;
        public float minValue;
        public float maxValue;
        public int maxCount;
    };

    public void movimientoEntradaStage(GLOBAL_TYPE.TIPO_ENTRADA _ESTADO )
    {
        //if (m_ESTADO == GLOBAL_TYPE.ESTADOS.entrandoScene)
        //{
            Debug.Log("m_tipoEntrada "+ _ESTADO);
            //Debug.Break();
            switch (_ESTADO)
            {
                case GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay:
                    {
                        //m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_caminando:
                    {
                        //Debug.Log("DER CAMINANDO");
                        m_changeMirada.miradaPj(1);
                        m_rigidbody_A.velocity = Vector2.right * 5f;
                        m_animator.SetFloat("Velocity_X", 1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_caminando:
                    {
                        //Debug.Log("IZ CAMINANDO");
                        m_changeMirada.miradaPj(-1);
                        m_rigidbody_A.velocity = Vector2.left * 5f;
                        m_animator.SetFloat("Velocity_X", 1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo:
                    {
                        m_animator.SetFloat("velocity_Y", -1);
                        m_changeMirada.miradaPj(-1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo:
                    {
                        m_animator.SetFloat("velocity_Y", -1);
                        m_changeMirada.miradaPj(1);
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_salto:
                    {
                    //Debug.Break();
                        if (!saltoInicial)//nbecesita saltar al salir tambien, no solamente al entrar
                        {
                            saltoInicial = true;
                            m_animator.SetTrigger("startJump");
                            m_changeMirada.miradaPj(-1);
                            m_rigidbody_A.velocity = new Vector2(-10, potenciaSalto);//new Vector2(-12, potenciaSalto*0.8f);
                        }

                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_salto:
                    {
                    //Debug.Break();
                    //Debug.Log("**************");
                        if (!saltoInicial) {
                            saltoInicial = true;
                            m_animator.SetTrigger("startJump");
                            m_changeMirada.miradaPj(1);
                            m_rigidbody_A.velocity = new Vector2(10, potenciaSalto);
                        }

                        break;
                    }
            case GLOBAL_TYPE.TIPO_ENTRADA.Inmersion:
                {
                    m_rigidbody_A.velocity = Vector2.zero;
                    break;
                }
            }
    }

    private void getInput_Axis_LEFT_button(float currentValor_X, float valorInput_Vertical)
    {
        //Debug.Log($"m_isGrounded: {m_isGrounded} | X; {Mathf.Abs(valueInterno_Axis.x)} | Y: {valueInterno_Axis.y}");
        if (m_ESTADO == ESTADOS.movementNormal &&
            m_isGrounded && Mathf.Abs(valueInterno_Axis.x) < 0.3f && valueInterno_Axis.y <= 0
            && !m_groundedClass.EsSueloSolido
            && !m_Bool_checkRayscastWithEvent.Curr_bool
            )
        {
            //Debug.Log("Abajo!");
            m_animator.ResetTrigger("traspasandoTru");
            m_animator.SetTrigger("traspasandoTru");
            m_rigidbody_A.velocity = Vector2.zero;
            PlatformEffector2D effect = m_groundedClass.Curr_Effector2D;
            effect.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            //m_animator.SetTrigger("startJump");
        }
    }


    private void getInput_Axis_LEFT(float currentValor_X, float valorInput_Vertical)
    {
        if (m_ESTADO== GLOBAL_TYPE.ESTADOS.Mapa) return;
        valueInterno_Axis.x = currentValor_X;
        valueInterno_Axis.y = valorInput_Vertical;
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.entrandoScene) return;
        if (Mathf.Abs(currentValor_X) > limiteInput_movX)
            valorInput_Horizontal = currentValor_X;
        else valorInput_Horizontal = 0;
        //}
        if(m_ESTADO==GLOBAL_TYPE.ESTADOS.movementNormal)
            m_animator.SetFloat("Velocity_X", Mathf.Abs(valorInput_Horizontal));
        else
            m_animator.SetFloat("Velocity_X", 0);
        m_animator.SetFloat("Axis_Y", valorInput_Vertical);
    }
    private void setZerotInput_Axis_LEFT(InputAction.CallbackContext ctx)
    {
        valueInterno_Axis.x = 0;
        valorInput_Horizontal = 0;
        m_animator.SetFloat("Velocity_X", 0);
        valorInput_Vertical = 0;
        m_animator.SetFloat("Axis_Y", 0);
    }
    private void setZerotInput_Axis_LEFT()
    {
        valorInput_Horizontal = 0;
        m_animator.SetFloat("Velocity_X", 0);
        valorInput_Vertical = 0;
        m_animator.SetFloat("Axis_Y", 0);
    }
    private void jump( InputAction.CallbackContext ctx )
    {
        if ((m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal && move && vivo) && (m_isGrounded || (m_currentCoyoteTime < m_coyoteTime && IsFalling())))
        {
            ActionJump();
            return;
        }
        if(m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair)
        {
            ExitStair();
            ActionJump();
            return;
        }
        if(m_ESTADO == GLOBAL_TYPE.ESTADOS.onWall && curr_coolDown_JumpPared < 0)
        {
            CancelarPared();
            ActionJump(true);
        }

    }
    private void ActionJump(bool saltoPared=false)
    {
        Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_salto_start);
        m_at_arranar.SetTrigger("jump");
        stopJumping = false;
        m_animator.SetTrigger("startJump");
        
        if (!m_isGrounded) m_rigidbody_A.velocity = new Vector2(m_rigidbody_A.velocity.x, 0);
        if (saltoPared)
        {
            curr_coolDown_JumpPared = coolDown_JumpPared;
            //TODO
            GameObject objPolvo = m_op_Jump_pared.emitirObj(1f, m_pivoteSaltoGO_pared.transform.position, returnObj: true);
            float impulosX = potenciaSalto * 1.1f;
            if (m_changeMirada.getMirada() == LADO.iz)
            {
                //Todo
                objPolvo.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                objPolvo.transform.GetChild(0).localScale = Vector3.one;
                impulosX *= -1;
            }
            m_rigidbody_A.AddForce(Vector2.up * potenciaSalto * 0.8f + Vector2.right * impulosX, ForceMode2D.Impulse);
        }
        else
        {
            m_rigidbody_A.AddForce(Vector2.up * potenciaSalto, ForceMode2D.Impulse);
            m_op_Jump.emitirObj(1f, m_pivoteSaltoGO.transform.position);
        }
        transform.SetParent(null);
    }
    private void detenerJump(InputAction.CallbackContext ctx)
    {
        if (m_ESTADO != GLOBAL_TYPE.ESTADOS.movementNormal)
        {
            return;
        }
        if (!stopJumping) {
            if (m_rigidbody_A.velocity.y > 1)
            {
                Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_salto_cancel);
            }
            stopJumping = true;
            m_rigidbody_A.velocity = new Vector3(m_rigidbody_A.velocity.x, m_rigidbody_A.velocity.y * 0.5f);
        }
    }

    //public bool puedeConversar()
    //{
    //    return m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal;
    //}

    public void recibirDanio(dataDanio m_dataDanio, bool soloEmpuje)
    {
        float impacto;
        if (m_dataDanio.getLado() == GLOBAL_TYPE.LADO.iz) impacto = m_dataDanio.getImpactoEmpuje();
        else impacto = -m_dataDanio.getImpactoEmpuje();

        m_rigidbody_A.velocity = Vector3.zero;
        current_timeShoot = -1;
        if (m_dataDanio.tipo_danio == GLOBAL_TYPE.TIPO_DANIO.lava) {
            m_rigidbody_A.velocity = new Vector2(0, m_rigidbody_A.velocity.y * 0.5f);
        }
        else
        {
            if(m_ESTADO != GLOBAL_TYPE.ESTADOS.herida)
            {
                m_rigidbody_A.velocity = new Vector3(m_rigidbody_A.velocity.x + impacto, m_rigidbody_A.velocity.y * 0.5f);
            }
        }
        if (!soloEmpuje && m_ESTADO != GLOBAL_TYPE.ESTADOS.herida)
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
        m_animator.SetBool("Bloquear", true);
        //m_magnesis.enabled = false;
        m_rigidbody_A.velocity = Vector2.zero;
        m_boxCollisionPlatform.enabled = false;
        m_rigidbody_A.isKinematic = true;
        m_animEventSP.MoveToFront();
        m_staminaPsiquica.DesactivarScript();
        move = false;
        m_animator.SetTrigger("morir");
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

    //public GLOBAL_TYPE.ESTADOS test_getEstado()
    //{
    //    return m_ESTADO;
    //}
    public void activarMovimiento()
    {
        m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        m_PowerManager.UpdateData();

        setControls();
    }
    public void setTipoEntrada(GLOBAL_TYPE.TIPO_ENTRADA tipoEntrada)
    {
        m_tipoEntrada = tipoEntrada;
        if(m_ESTADO != GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            m_ESTADO = GLOBAL_TYPE.ESTADOS.entrandoScene;
            if (m_ControlPJ != null) { 
                m_ControlPJ.PLAYER.Disable();
                m_ControlPJ = null;
            }
            movimientoEntradaStage(tipoEntrada);
        }
    }
    public void FinishPowerDisparo()
    {
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.POWER_Disparo 
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.POWER_Bomba 
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.POWER_Teletransportacion
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.POWER_Inmersoin
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.POWER_Quinto
            || m_ESTADO == GLOBAL_TYPE.ESTADOS.InteraccionGenerica
            )
        {
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        }
    }
    public void SetState(GLOBAL_TYPE.ESTADOS _state, int index_interaccionGenerica=0)
    {
        m_ESTADO = _state;
        if(_state== GLOBAL_TYPE.ESTADOS.InteraccionGenerica)
        {
            m_rigidbody_A.velocity = Vector2.zero;
            m_animator.SetInteger("InterractionGenerica_value", index_interaccionGenerica);
            m_animator.SetTrigger("Interaccion_generica_tr");
        }
        if (_state == GLOBAL_TYPE.ESTADOS.POWER_Disparo || _state == GLOBAL_TYPE.ESTADOS.POWER_Bomba)
        {

            m_rigidbody_A.velocity = Vector2.zero;
        }
    }
    public void StopMovement()
    {
        m_rigidbody_A.velocity = Vector2.zero;
    }

    public void cogerItem()
    {
        MASTER_REFERENCE.instance.CameraController.SetCameraCerca_PAUSE();
        m_rigidbody_A.velocity = Vector2.zero;
        ApplyForce_X(Vector3.up, 35f);
        desactivarControles();
        m_animator.SetTrigger("GetItem");
        m_ESTADO = GLOBAL_TYPE.ESTADOS.cogerItem;
    }

    public void ExitTernminarItem()
    {
        MASTER_REFERENCE.instance.CameraController.SetCameraGameplay_normal();
        m_animator.SetTrigger("GetItem_EXIT");
    }

    internal void ApplyForce(Vector3 dir, float pushPower)
    {
        if (m_animator.GetFloat("Axis_Y") >= 0 /*|| m_isGrounded*/) return;
        m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
        swordFinished();
        current_timeShoot = -1;
        m_animator.SetTrigger("startJump");
        m_rigidbody_A.AddForce(dir * pushPower, ForceMode2D.Impulse);

    }
    internal void ApplyForce_X(Vector3 dir, float pushPower)
    {
        m_rigidbody_A.AddForce(dir * pushPower, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        //Debug.Log("m_isMoveablePlatform : "+ m_isMoveablePlatform);
        if (m_isMoveablePlatform && m_isGrounded)
        {
            transform.SetParent(m_groundedClass.GetPlatformMoveable());
            //transform.position = new Vector3(transform.position.x, m_grounded.GetYPosition(), transform.position.z);
            transform.position = new Vector3(transform.position.x, m_groundedClass.GetYPosition(), transform.position.z);
        }
        else
        {
            transform.SetParent(null);
        }
    }

    public bool CanStartStair_UP()
    {
        return (m_ESTADO== GLOBAL_TYPE.ESTADOS.movementNormal
            /*!m_isGrounded && */ 
            && inputLeftAxis.y > 0.75f && Mathf.Abs(inputLeftAxis.x) < 0.2f);
    }
    public bool CanStartStair_DOWN()
    {
        //Debug.Log($"m_isGrounded: {m_isGrounded} | inputLeftAxis.y < -0.75f: {inputLeftAxis.y < -0.75f} | Mathf.Abs(inputLeftAxis.x): {Mathf.Abs(inputLeftAxis.x)< 0.2f}");
        return (m_ESTADO == GLOBAL_TYPE.ESTADOS.movementNormal
            && m_isGrounded 
            && inputLeftAxis.y < -0.75f && Mathf.Abs(inputLeftAxis.x) < 0.2f);
    }
    
    public void StartStair(Stairs_UP curr_Stairs)
    {
        valueInterno_Axis.x = 0;
        m_animator.SetFloat("Velocity_X", 0);

        //m_PO_Enter.emitirObj(1f,transform.position, true, true);
        this.curr_Stairs = curr_Stairs;
        m_animator.ResetTrigger("ExitStair");
        m_ESTADO = GLOBAL_TYPE.ESTADOS.Stair;
        m_rigidbody_A.velocity = Vector2.zero;
        m_rigidbody_A.gravityScale = 0;
        m_animator.SetTrigger("StarttStair");
        m_animator.SetBool("Bool_Stair", true);

        valueInterno_Axis.x = 0;
        m_animator.SetFloat("Velocity_X", 0);
    }
    public void ExitStair()
    {
        if(curr_Stairs!=null && (m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair || m_ESTADO == GLOBAL_TYPE.ESTADOS.danio))
        {
            //m_PO_Exit.emitirObj(1f, transform.position, true, true);
            curr_Stairs.DesactivarEscalera();
            m_ESTADO = GLOBAL_TYPE.ESTADOS.movementNormal;
            m_animator.ResetTrigger("StarttStair");
            m_animator.SetTrigger("ExitStair");
            m_rigidbody_A.gravityScale = m_originalGravityScale;
            m_animator.SetBool("Bool_Stair", false);
            m_changeMirada.miradaPj(valorInput_Horizontal, true, true);
        }
    }
    internal void ExitStairToTop()
    {
        if(m_ESTADO != GLOBAL_TYPE.ESTADOS.Stair)
        {
            return;
        }
        ExitStair();
        m_animator.SetTrigger("startJump");
        m_rigidbody_A.AddForce(Vector2.up * 35f, ForceMode2D.Impulse);
        //Debug.Log("Top escalera!");
    }
    internal void ExitStairToDown()
    {
        ExitStair();
        m_animator.SetTrigger("startJump");
        m_rigidbody_A.AddForce(Vector2.up * 22f, ForceMode2D.Impulse);
        //Debug.Log("Top escalera!");
    }
    internal bool CanTopStair()
    {
        return m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair && inputLeftAxis.y > 0.7f;
    }
    internal bool CanExecutePower()
    {
        return m_ESTADO != GLOBAL_TYPE.ESTADOS.muerto && m_ESTADO != GLOBAL_TYPE.ESTADOS.muerto;
    }
    private void BtnCancel()
    {
        switch (m_ESTADO)
        {
            case GLOBAL_TYPE.ESTADOS.Stair:
                {
                    ExitStair();
                    break;
                }            
        }
    }
    internal void GetDanio()
    {
        if (m_ESTADO == GLOBAL_TYPE.ESTADOS.Stair || m_ESTADO == GLOBAL_TYPE.ESTADOS.danio)
        {
            ExitStair();
        }
    }
}
