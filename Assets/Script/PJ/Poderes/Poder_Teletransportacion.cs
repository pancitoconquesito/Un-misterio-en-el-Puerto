using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Poder_Teletransportacion : Generic_Poder, IPOWER
{
    public enum EstadosTeletransportacion
    {
        None, Start, MoveCircle, Moving, End
    }
    EstadosTeletransportacion m_EstadosTeletransportacion=EstadosTeletransportacion.None;
    [SerializeField] private float m_coste;
    [SerializeField] private GameObject MovingCircle;
    [SerializeField] private float empujeInicial;
    [SerializeField] private GameObject pivoteCamara;
    [SerializeField] private movementPJ m_movementPJ;
    [SerializeField] private Image m_energy;
    [SerializeField] private float timeMax;
    [SerializeField] private LineRenderer m_LineRenderer;
    [SerializeField] private int seccionesCount;
    [SerializeField] private pivoteCameraController m_pivoteCameraController;
    [SerializeField] private float maxDistanceTele;
    [SerializeField] private Animator m_animatorPJ;
    [SerializeField] private vida_PJ m_vidaDanio_PJ;
    [SerializeField] private Rigidbody2D rb_player;
    [SerializeField] private BoxCollider2D colliderPlayer;
    [SerializeField] private float timeMovingStep;
    [SerializeField] GameObject m_cara_GO;
    private float currentEnergy;

    private Rigidbody2D rg_MovingCircle;
    private Vector2 inputAxis;
    private Vector2 inictialPos, finalPos;
    private Vector2[] m_L_pos;
    private int currIndexMoving;
    SpriteRenderer spriteRenderer_cara;
    private void Awake()
    {
        base.m_PowerManager.PODER_TeletransportacionPsiquica = this;
        rg_MovingCircle = MovingCircle.GetComponent<Rigidbody2D>();
        m_L_pos = new Vector2[seccionesCount];
    }
    void Start()
    {
        //cargar desde DATA}
        //m_coste
        spriteRenderer_cara = m_cara_GO.GetComponent<SpriteRenderer>();
        m_PowerManager.StaminaPsiquica.Coste_Teletransportacion = m_coste;//change
        base.curr_cadencia = 0;
    }

    public void TryExecute(movementPJ m_movementPJ)
    {
        if (m_EstadosTeletransportacion == EstadosTeletransportacion.None && base.TryExecutePower(m_movementPJ, m_coste))
        {
            Execute(m_PowerManager.ChangeMirada.getMirada());
        }
        else
        {

        }
    }
    public void Execute(GLOBAL_TYPE.LADO _lado)
    {
        //player desactivar
        m_movementPJ.IsEnabledFisicas(false);
        m_EstadosTeletransportacion = EstadosTeletransportacion.Start;
        lado = _lado;
        base.ExecutePower(_lado);
    }

    //llamar desde animator
    public void MoveCircle()
    {
        if (m_EstadosTeletransportacion == EstadosTeletransportacion.MoveCircle) return;
        m_EstadosTeletransportacion = EstadosTeletransportacion.MoveCircle;
        //inicital position
        inictialPos = m_movementPJ.transform.position;
        //aparecer obj
        MovingCircle.SetActive(true);
        //empujarlo un poco hacia adelante del pj
        if (lado == GLOBAL_TYPE.LADO.iz)
        {
            currConstVelocity = -Vector2.right;
        }
        else
        {
            currConstVelocity = Vector2.right;
        }
        //cambiar pivote camara
        pivoteCamara.transform.SetParent(MovingCircle.transform);

        currentEnergy = timeMax;
    }
    private Vector2 currConstVelocity;
    private void Update()
    {
        if (base.curr_cadencia > -1) base.curr_cadencia -= Time.deltaTime;
        if (m_EstadosTeletransportacion== EstadosTeletransportacion.MoveCircle)
        {
            inputAxis = m_movementPJ.InputLeftAxis;
            if(inputAxis.magnitude>0.1f)
                currConstVelocity = inputAxis;
            rg_MovingCircle.velocity = currConstVelocity * empujeInicial;
            //quitar poder psquico cad x tiempo x cantidad
            float currFactor = currentEnergy / timeMax;
            m_energy.fillAmount = currFactor;

            if (currentEnergy > 0) { 
                currentEnergy -= Time.deltaTime;
                float limite = 1f / seccionesCount;
                int posicion = (int)(((1- currFactor) /limite));
                //Debug.Log("Posicion : "+posicion);
                m_L_pos[posicion] = MovingCircle.transform.position;



                Vector2 posObj = new Vector2(m_movementPJ.transform.position.x, m_movementPJ.transform.position.y);
                Vector2 direction = inputAxis.normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //m_cara_GO.transform.rotation = Quaternion.Euler(0, 0, angle);


                if (inputAxis.sqrMagnitude > 0.01f)
                {
                    // Calcula el ángulo del joystick en grados
                    float rawAngle = Mathf.Atan2(inputAxis.y, inputAxis.x) * Mathf.Rad2Deg;

                    // Ajusta el ángulo para que 0° corresponda a la derecha
                    rawAngle -= 90;

                    // Normaliza el ángulo al rango [0°, 360°]
                    if (rawAngle < 0)
                        rawAngle += 360;

                    // SpriteRenderer del objeto
                    

                    // Determina si el personaje debe "voltearse"
                    // Asigna flip

                    // Clasifica la dirección según los rangos
                    float snappedAngle = 0;
                    if (rawAngle >= 337.5f || rawAngle < 22.5f) snappedAngle = 0;       // Derecha
                    else if (rawAngle >= 22.5f && rawAngle < 67.5f) snappedAngle = 45;  // Arriba-Derecha
                    else if (rawAngle >= 67.5f && rawAngle < 112.5f) snappedAngle = 90; // Arriba
                    else if (rawAngle >= 112.5f && rawAngle < 157.5f) snappedAngle = 135; // Arriba-Izquierda
                    else if (rawAngle >= 157.5f && rawAngle < 202.5f) snappedAngle = 180; // Izquierda
                    else if (rawAngle >= 202.5f && rawAngle < 247.5f) snappedAngle = 225; // Abajo-Izquierda
                    else if (rawAngle >= 247.5f && rawAngle < 292.5f) snappedAngle = 270; // Abajo
                    else if (rawAngle >= 292.5f && rawAngle < 337.5f) snappedAngle = 315; // Abajo-Derecha


                    bool flipX = false; // Si el ángulo está entre 90° y 270° mirar hacia la izquierda
                    bool flipY = false;
                    if (snappedAngle== 270)
                    {
                        flipX = true;
                        flipY = true;
                    }

                    //Debug.Log($"dir: {snappedAngle}");
                    spriteRenderer_cara.flipX = flipX;
                    spriteRenderer_cara.flipY = flipY;
                    // Asigna la rotación al objeto
                    m_cara_GO.transform.rotation = Quaternion.Euler(0, 0, flipX ? 360 - snappedAngle : snappedAngle);
                }

            } 
            else
            {
                Moving();
            }
        }
        if (m_EstadosTeletransportacion == EstadosTeletransportacion.Moving)
        {
            //recorrer
            if (currIndexMoving == seccionesCount)
            {
                End();
                return;
            }
            if (Vector2.Distance(new Vector2(m_movementPJ.transform.position.x, m_movementPJ.transform.position.y), m_L_pos[currIndexMoving]) < 0.075f)
            {
                currIndexMoving++;
                if(currIndexMoving == seccionesCount)
                {
                    End();
                    return;
                }
                else
                {
                    //Debug.Log("currIndexMoving : "+ currIndexMoving);
                    int moveId = LeanTween.move(m_movementPJ.gameObject, m_L_pos[currIndexMoving], timeMovingStep).setEase(LeanTweenType.linear).id;
                }
                //moveToNxtPosition
            }

            //rb_player.AddForce((new Vector2(transform.position.x, transform.position.y) - finalPos).normalized * 5f);
            //verficar fin de recorrido End()
        }
    }
    public void Moving()
    {
        if (m_EstadosTeletransportacion == EstadosTeletransportacion.Moving) return;
        m_EstadosTeletransportacion = EstadosTeletransportacion.Moving;
        //invulnerable
        m_vidaDanio_PJ.PuedeRecibirDanio = false;
        rb_player.isKinematic = true;
        colliderPlayer.isTrigger = true;
        currIndexMoving = 1;

        m_energy.fillAmount = 0;
        inputAxis = Vector2.zero;
        rg_MovingCircle.velocity = Vector2.zero;
        //set final position
        finalPos = MovingCircle.transform.position;

        //move camara
        pivoteCamara.transform.SetParent(m_movementPJ.transform);
        float timeToMoveCamera = Vector2.Distance(inictialPos, finalPos) / maxDistanceTele;
        //Debug.Log("Distance : " + Vector2.Distance(inictialPos, finalPos) + " | time : " + timeToMoveCamera);
        m_pivoteCameraController.returnInitialLocalPosition(timeToMoveCamera);

        //formar line de recorrido
        m_LineRenderer.enabled = true;
        m_LineRenderer.transform.SetParent(null);
        m_LineRenderer.transform.position = Vector2.zero;

        m_LineRenderer.positionCount = seccionesCount;
        for (int i = 0; i < m_L_pos.Length; i++)
        {
            m_LineRenderer.SetPosition(i, m_L_pos[i]);
        }
        //cambiar animacion
        m_animatorPJ.SetTrigger("TELE_MOVING");

        //quitar coso
        MovingCircle.transform.localPosition = Vector2.zero;
        MovingCircle.SetActive(false);

        int moveId = LeanTween.move(m_movementPJ.gameObject, m_L_pos[currIndexMoving], timeMovingStep).setEase(LeanTweenType.easeOutExpo).id;
        //int moveId = LeanTween.move(m_movementPJ.gameObject, finalPos, 5).setEase(LeanTweenType.easeOutExpo).id;
     
    }
    private void End()
    {
        if (m_EstadosTeletransportacion == EstadosTeletransportacion.End) return;
        m_EstadosTeletransportacion = EstadosTeletransportacion.End;
        //ocultar obj

        //cambiar animacion
        m_animatorPJ.SetTrigger("TELE_END");

        //al terminar debe volver a ser vulnerable
        //eliminar line recorrido
        m_LineRenderer.positionCount = 0;
        //despues de un tiempo

    }
    public void EndEnd()
    {
        //Debug.Log("ENDEND");
        m_vidaDanio_PJ.PuedeRecibirDanio = true;
        rb_player.isKinematic = false;
        rb_player.velocity = new Vector2(0, -10f);
        colliderPlayer.isTrigger = false;
        m_EstadosTeletransportacion = EstadosTeletransportacion.None;
        m_LineRenderer.enabled = false;

        m_movementPJ.IsEnabledFisicas(true);
    }
}
