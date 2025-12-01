using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
public class StatesEnemy_Gestor : MonoBehaviour
{
    public enum States
    {
        NONE, EsperaPatrulla, Alert, Perseguir, Estrategias, NoAlert,
    }
    //Idle
    //patrullando
    //alert
    //persiguiendo
    //select-idle-estrategia
    //realizado-estrategia
    //none
    public enum TipoArea
    {
        Rectangular, Circular
    }

    [Header("BASE ---------------------------------------")]
    [SerializeField] RecibiDanio_cb recibiDanio_cb;
    [SerializeField] Estrategias_Config estrategiasConfig;
    [SerializeField] Animator animator;
    [SerializeField] bool activo;
    [SerializeField] bool hasPerseguir;
    [SerializeField] bool has_soloPerseguir_NoEstrategia;
    [SerializeField] bool hasAlert;
    [SerializeField] float distanciaAlert = 120f;


    [Header("params ---------------------------------------")]
    [SerializeField] float tiempo_estrategias;
    [SerializeField] Vector2 tiempo_estrategias_rango;
    [SerializeField] bool hasPatrullar;
    [SerializeField] MovimientoPatrulla movimientoPatrulla;

    [Header("Perseguir ---------------------------------------")]
    [SerializeField] TipoArea tipoArea= TipoArea.Rectangular;
    [SerializeField] bool ignoreY;
    [SerializeField] List<Vector2> distanciasMinimas;//de menor a mas grande!
    [SerializeField] float velocidadPerseguir;
    [SerializeField] [Range(0.01f, 1f)] float alphaColorAreaDistancia;
    //[SerializeField] [Range(0f, 1f)] float porcentajeReanudarTiempoEsperaEstrategia;
    [SerializeField] LookAt2D_Rotator lookAt2D_Rotator;

    [Header("Perseguir avanzado ------------------------")]
    [SerializeField] GameObject go_checkerRayCast;
    [SerializeField] bool hasLimitePerseguir;
    [SerializeField] TipoArea tipoArea_limitePerseguir = TipoArea.Rectangular;
    [SerializeField] Vector2 distanciaMinima_limitePerseguir;
    [SerializeField] bool ignoreY_imitePerseguir;


    List<CheckerRayCast> checkerRayCast;
    CheckerRayCast checkerRayCast_der_down;
    CheckerRayCast checkerRayCast_der_up;
    CheckerRayCast checkerRayCast_iz_up;
    CheckerRayCast checkerRayCast_iz_down;
    CheckerRayCast checkerRayCast_up_left;
    CheckerRayCast checkerRayCast_up_right;
    CheckerRayCast checkerRayCast_down_left;
    CheckerRayCast checkerRayCast_down_right;


    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    [ShowNonSerializedField] States state;

    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Rigidbody2D rb;
    float curr_tiempo_estrategias;
    Transform target_transform;
    float segundoIntentoEstrategia = 1;
    float curr_segundoIntentoEstrategia = 1;
    int curr_indiceArea = 0;
    Vector2 finalMov_perseguir = Vector2.zero;
    Vector2 velocidadImpacto = Vector2.zero;
    bool recibiendoDanio = false;
    bool quitandoDanio = false;
    Vector2 originalPosition = Vector2.zero;
    private void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
        recibiDanio_cb.OnMorir += OnMorir;
        recibiDanio_cb.OnRecibirDanio += OnRecibirDanio;

        if (go_checkerRayCast != null)
        {
            checkerRayCast = go_checkerRayCast.GetComponents<CheckerRayCast>().ToList();
        }
        if(checkerRayCast!=null && checkerRayCast.Count > 0)
        {
            checkerRayCast_der_down = checkerRayCast.Find(x => x.Nombre == "der_down");
            checkerRayCast_der_up = checkerRayCast.Find(x => x.Nombre == "der_up");
            checkerRayCast_iz_up = checkerRayCast.Find(x => x.Nombre == "iz_up");
            checkerRayCast_iz_down = checkerRayCast.Find(x => x.Nombre == "iz_down");
            checkerRayCast_up_left = checkerRayCast.Find(x => x.Nombre == "up_left");
            checkerRayCast_up_right = checkerRayCast.Find(x => x.Nombre == "up_right");
            checkerRayCast_down_left = checkerRayCast.Find(x => x.Nombre == "down_left");
            checkerRayCast_down_right = checkerRayCast.Find(x => x.Nombre == "down_right");
        }

        originalPosition = transform.position;
    }

    void Start()
    {
        curr_tiempo_estrategias = tiempo_estrategias;
        SetTarget(MASTER_REFERENCE.instance.GO_PJ.transform);
        state = States.EsperaPatrulla;//! manejar de otra mnera
        animator.SetFloat("BT_Estrategia", 1);
        animator.SetFloat("BT_Move", 1);
        if (tipoArea == TipoArea.Circular)
        {
            if (distanciasMinimas != null && distanciasMinimas.Count > 0)
            {
                for (int i = 0; i < distanciasMinimas.Count; i++)
                {
                    distanciasMinimas[i] = new Vector2(distanciasMinimas[i].x, distanciasMinimas[i].x);
                }
            }
        }
    }

    private void OnDisable()
    {
        recibiDanio_cb.OnMorir -= OnMorir;
        recibiDanio_cb.OnRecibirDanio -= OnRecibirDanio;
        StopAllCoroutines();
    }

    void OnMorir()
    {
        rb.velocity = Vector2.zero;
        movimientoPatrulla.SetPatrulla(false);
        animator.SetTrigger("tr_death");
    }
    internal void OnRecibirDanio(Vector2 velocidadImpacto)
    {
        this.velocidadImpacto = velocidadImpacto;
        recibiendoDanio = true;
    }

    void Update()
    {
        if (!activo || !recibiDanio_cb.IsVivo)
        {
            return;
        }

        //check suelo
        animator.SetBool("ground", true);

        switch (state)
        {
            case States.EsperaPatrulla:
                {

                    if (hasPerseguir)
                    {
                        UpdateBoos_hasPerseguir();
                    }
                    else
                    {
                        movimientoPatrulla.SetPatrulla(true);
                    }
                    break;
                }
            case States.Alert:
                {
                    Vector2 dir = target_transform.position - transform.position;
                    if (ignoreY)
                    {
                        dir.y = transform.position.y;
                    }
                    dir = dir.normalized;
                    lookAt2D_Rotator.LookAtDirection(dir);

                    break;
                }
            case States.Perseguir:
                {
                    movimientoPatrulla.SetPatrulla(false);

                    if (has_soloPerseguir_NoEstrategia)
                    {
                        if (!CheckDistancias().result)
                        {
                            state = States.EsperaPatrulla;
                        }
                    }
                    else
                    {
                        bool puedeComenzarUnaEstrategia = CheckPuedeComenzarEstrategia();
                        //Estrategia
                        //float nRandomOffset = Random.Range(tiempo_estrategias_rango.x, tiempo_estrategias_rango.y);
                        //bool puedeHacerEstrategia = CheckTiempoEstrategia(nRandomOffset);
                        //var CheckDistancias_result = CheckDistancias();
                        //bool randomExitoso = false;
                        //List<NodeEstrategia> nodosEstreategias = estrategiasConfig.L_estrategias.FindAll(x => x.indexArea == CheckDistancias_result.indexArea_Estrategia).ToList();
                        //float nRandom_probabilidad = Random.Range(0f, 1f);
                        //int nRandom_index = Random.Range(0, nodosEstreategias.Count);
                        //if (nodosEstreategias[nRandom_index].probabilidad > nRandom_probabilidad)//porcentajeReanudarTiempoEsperaEstrategia
                        //{
                        //    randomExitoso = true;
                        //}
                        ////Debug.Log($"randomExitoso: {randomExitoso} | puedeHacerEstrategia: {puedeHacerEstrategia}");
                        if(puedeComenzarUnaEstrategia)
                        {
                            curr_tiempo_estrategias = tiempo_estrategias;
                            SetBeginEstrategia();
                            return;
                        }
                    }
                    //mirada
                    if (!recibiendoDanio)
                    {
                        lookAt2D_Rotator.LookAtDirection(rb.velocity);
                    }

                    //retornar a patrulla?
                    if (hasLimitePerseguir)
                    {
                        bool fueraDelLimite = false;
                        if(ignoreY_imitePerseguir)
                        {
                            originalPosition.y = transform.position.y;
                        }
                        float distanciaPjOrigin = Vector2.Distance(transform.position, originalPosition);
                        fueraDelLimite = distanciaPjOrigin > distanciaMinima_limitePerseguir.magnitude;
                        if (fueraDelLimite)
                        {
                            state = States.EsperaPatrulla;
                            return;
                        }
                    }


                    //movimiento
                    Vector2 dir = target_transform.position - transform.position;
                    if (ignoreY)
                    {
                        dir.y = transform.position.y;
                    }
                    dir = dir.normalized;
                    if (dir.x > 0)
                    {
                        if (dir.x <1)
                        {
                            dir.x = 1;
                        }
                    }
                    else
                    {
                        if (dir.x > -1)
                        {
                            dir.x = -1;
                        }
                    }

                    //direccion vs colisionadores
                    if (checkerRayCast != null && checkerRayCast.Count > 0)
                    {
                        Vector2 saveDir = dir;
                        if (checkerRayCast_der_down.IsColisionando && saveDir.x > 0 && saveDir.y > 0)
                        {
                            dir = Vector2.up;
                        }
                        if(checkerRayCast_der_up.IsColisionando && saveDir.x > 0 && saveDir.y <= 0)
                        {
                            dir = Vector2.down;
                        }
                        if (checkerRayCast_iz_down.IsColisionando && saveDir.x < 0 && saveDir.y > 0)
                        {
                            dir = Vector2.up;
                        }
                        if (checkerRayCast_iz_up.IsColisionando && saveDir.x < 0 && saveDir.y < 0)
                        {
                            dir = Vector2.down;
                        }
                        if(checkerRayCast_up_left.IsColisionando && saveDir.y > 0 && saveDir.x > 0){
                            dir = Vector2.right;
                        }
                        if(checkerRayCast_up_right.IsColisionando && saveDir.y > 0 && saveDir.x < 0)
                        {
                            dir = Vector2.left;
                        }
                        if(checkerRayCast_down_left.IsColisionando && saveDir.y<0&& saveDir.x>0)
                        {
                            dir = Vector2.right;
                        }
                        if (checkerRayCast_down_right.IsColisionando && saveDir.y < 0 && saveDir.x < 0)
                        {
                            dir = Vector2.left;
                        }
                    }

                    finalMov_perseguir = dir * velocidadPerseguir;
                    break;
                }
            case States.Estrategias:
                {
                    movimientoPatrulla.SetPatrulla(false);
                    bool puedeComenzarUnaEstrategia = CheckPuedeComenzarEstrategia();

                    if (puedeComenzarUnaEstrategia)
                    {
                        curr_tiempo_estrategias = tiempo_estrategias;
                        SetBeginEstrategia();
                    }

                    break;
                }
        }
        //retroceso danio
        if (recibiendoDanio)
        {
            if (!quitandoDanio)
            {
                quitandoDanio = true;
                Invoke("quitarDanio", 0.3f);
            }
        }
        //set velocity
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (hasAlert)
        {
            float distancia = Vector2.Distance(target_transform.position, transform.position);
            if(distancia > distanciaAlert)
            {
                tuvoAlert = false;
            }
        }
    }

    private bool CheckPuedeComenzarEstrategia()
    {
        //Estrategia
        float nRandomOffset = Random.Range(tiempo_estrategias_rango.x, tiempo_estrategias_rango.y);
        bool puedeHacerEstrategia = CheckTiempoEstrategia(nRandomOffset);
        var CheckDistancias_result = CheckDistancias();
        bool randomExitoso = false;
        List<NodeEstrategia> nodosEstreategias = estrategiasConfig.L_estrategias.FindAll(x => x.indexArea == CheckDistancias_result.indexArea_Estrategia).ToList();
        float nRandom_probabilidad = Random.Range(0f, 1f);
        int nRandom_index = Random.Range(0, nodosEstreategias.Count);
        if (nodosEstreategias[nRandom_index].probabilidad > nRandom_probabilidad)//porcentajeReanudarTiempoEsperaEstrategia
        {
            randomExitoso = true;
        }
        bool puedeComenzarUnaEstrategia = false;
        if (randomExitoso && puedeHacerEstrategia && CheckDistancias_result.result)
        {
            puedeComenzarUnaEstrategia = true;
        }

        return puedeComenzarUnaEstrategia;
    }

    private void FixedUpdate()
    {
        if (!activo || !recibiDanio_cb.IsVivo)
        {
            return;
        }

        switch (state)
        {
            case States.Perseguir:
                {
                    if (!recibiendoDanio)
                    {
                        rb.velocity = finalMov_perseguir;
                    }
                    else
                    {
                        rb.velocity = velocidadImpacto;
                    }

                    break;
                }
        }
    }

    private void quitarDanio()
    {
        if (recibiendoDanio) recibiendoDanio = false;
        quitandoDanio = false;
    }

    bool CheckTiempoEstrategia(float offset)
    {
        if (curr_tiempo_estrategias > -1f)
        {
            curr_tiempo_estrategias -= Time.deltaTime;
        }
        return curr_tiempo_estrategias + offset < 0;
    }

    (bool result, int indexArea_Estrategia) CheckDistancias()
    {
        bool isIn = false;
        int indexArea_Estrategia = 0;
        Vector2 DistanciaAlTarget = new Vector2(Mathf.Abs(target_transform.position.x - transform.position.x),
            Mathf.Abs(target_transform.position.y - transform.position.y));

        for (int i = 0; i < distanciasMinimas.Count; i++)
        {
            bool estaDentro = false;
            if (tipoArea == TipoArea.Rectangular)
            {
                estaDentro = DistanciaAlTarget.x < distanciasMinimas[i].x &&
                DistanciaAlTarget.y < distanciasMinimas[i].y;
                indexArea_Estrategia = i;
            }
            else
            {
                estaDentro = Vector2.Distance((Vector2)target_transform.position, (Vector2)transform.position)
                    < distanciasMinimas[i].magnitude;
                indexArea_Estrategia = i;
            }

            if (estaDentro)
            {
                isIn = true;
                curr_indiceArea = i;
                break;
            }
        }
        float distancia = Vector2.Distance(target_transform.position, transform.position);

        return (isIn, indexArea_Estrategia);
    }

    //bool CheckIsDistanciaMasCercana()
    //{
    //    Vector2 vectorDiff = new Vector2(Mathf.Abs(target_transform.position.x - transform.position.x),
    //        Mathf.Abs(target_transform.position.y - transform.position.y));

    //    bool estaDentro = false;
    //    if (tipoArea == TipoArea.Rectangular)
    //    {
    //        estaDentro = vectorDiff.x < distanciasMinimas[0].x &&
    //            vectorDiff.y < distanciasMinimas[0].y;
    //    }
    //    else
    //    {
    //        estaDentro = vectorDiff.magnitude > distanciasMinimas[0].magnitude;
    //    }

    //    return estaDentro;
    //}
    bool tuvoAlert = false;
    void SetBeginEstrategia()
    {
        //if (has_soloPerseguir_NoEstrategia)
        //{
        //    return;
        //}

        //rb.velocity = Vector2.zero;  
        //if (hasAlert && state != States.Alert && !tuvoAlert)
        //{
        //    tuvoAlert = true;
        //    state = States.Alert;
        //    animator.SetTrigger("tr_alert");
        //}
        //else
        //{
        //    state = States.Estrategias;
        //    float indexEstrategiaSeleccionada = (float)estrategiasConfig.SeleccionarEstrategia(curr_indiceArea);
        //    animator.SetFloat("BT_Estrategia", indexEstrategiaSeleccionada);
        //    animator.SetTrigger("tr_estrategia");
        //}
    }

    private void UpdateBoos_hasPerseguir()
    {
        var checkDistancias_result = CheckDistancias();


        if (checkDistancias_result.result)//esta dentro de las areas
        {
            if (has_soloPerseguir_NoEstrategia && state != States.Perseguir)
            {
                state = States.Perseguir;
                animator.ResetTrigger("tr_move");
                animator.SetTrigger("tr_move");
                return;
            }

            //bool ultimaPosicion = CheckIsDistanciaMasCercana();
            bool ultimaPosicion = checkDistancias_result.indexArea_Estrategia==0;
            if (ultimaPosicion)//! ultima es la mas cercana, que seria [0]
            {
                Debug.Log("ultimaPosicion");
                SetBeginEstrategia();
            }
            else
            {
                //retornar a patrulla?
                bool fueraDelLimite = false;
                if (hasLimitePerseguir)
                {
                    if (ignoreY_imitePerseguir)
                    {
                        originalPosition.y = transform.position.y;
                    }
                    float distanciaPjOrigin = Vector2.Distance(transform.position, originalPosition);
                    fueraDelLimite = distanciaPjOrigin > distanciaMinima_limitePerseguir.magnitude;
                    //Debug.Log($"distanciaPjOrigin: {distanciaPjOrigin} | distnaceMag: {distanciaMinima_limitePerseguir.magnitude}");
                    if (fueraDelLimite)
                    {
                        //state = States.EsperaPatrulla;
                        //state = States.EsperaPatrulla;
                        //movimientoPatrulla.SetPatrulla(true);

                        bool puedeComenzarUnaEstrategia = CheckPuedeComenzarEstrategia();

                        if (puedeComenzarUnaEstrategia)
                        {
                            SetBeginEstrategia();
                        }
                        else
                        {
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                            {
                                animator.SetTrigger("tr_idle");
                            }
                        }

                        Debug.Log("fueraDelLimite!!!!!--------------");
                        return;
                    }
                }

                state = States.Perseguir;
                animator.ResetTrigger("tr_move");
                animator.SetTrigger("tr_move");
            }
            //Debug.Log("Estoy dentro");
            movimientoPatrulla.SetPatrulla(false);
        }
        else
        {
            if (has_soloPerseguir_NoEstrategia)
            {
                state = States.EsperaPatrulla;
            }

            if (hasPatrullar)
            {
                movimientoPatrulla.SetPatrulla(true);
            }
            //Debug.Log("NO Estoy dentro");
        }
    }

    private void SetTarget(Transform _transform) => target_transform = _transform;
    public void SetState(States _state) => state = _state;
    private void OnDrawGizmos()
    {
        if(distanciasMinimas!=null && distanciasMinimas.Count > 0)
        {
            Gizmos.color = Color.white;
            int indice = 0;

            if(tipoArea == TipoArea.Circular){
                for (int i = 0; i < distanciasMinimas.Count; i++)
                {
                    distanciasMinimas[i] = new Vector2(distanciasMinimas[i].x, distanciasMinimas[i].x);
                }
            }

            foreach (var vector in distanciasMinimas)
            {
                int currIndice = indice % GLOBAL_TYPE.lista_Colores_A.Count;
                indice++;
                Gizmos.color = new Color(GLOBAL_TYPE.lista_Colores_A[currIndice].r, GLOBAL_TYPE.lista_Colores_A[currIndice].g, GLOBAL_TYPE.lista_Colores_A[currIndice].b, alphaColorAreaDistancia);
                
                if (tipoArea== TipoArea.Rectangular)
                {
                    Gizmos.DrawWireCube(transform.position, new Vector3(vector.x, vector.y, 0) * 2f);
                    Gizmos.DrawCube(transform.position, new Vector3(vector.x, vector.y, 0)*2f);
                }
                else
                {
                    Gizmos.DrawSphere(transform.position, vector.magnitude);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + vector.magnitude));
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + vector.magnitude, transform.position.y));
                }
            }
        }


        if (distanciaMinima_limitePerseguir != null )
        {
            Gizmos.color = Color.white;
            int indice = 0;

            if (tipoArea_limitePerseguir == TipoArea.Circular)
            {
                distanciaMinima_limitePerseguir = new Vector2(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.x);
            }

            indice = GLOBAL_TYPE.lista_Colores_A.Count -1 ;
            Gizmos.color = new Color(GLOBAL_TYPE.lista_Colores_A[indice].r, GLOBAL_TYPE.lista_Colores_A[indice].g, GLOBAL_TYPE.lista_Colores_A[indice].b, alphaColorAreaDistancia);

            Vector2 originVector = originalPosition;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                originVector = transform.position;
            }
            else
            {
                //Debug.Log("En el Editor y el juego está corriendo");
            }
#endif
            if (tipoArea == TipoArea.Rectangular)
            {
                Gizmos.DrawWireCube(originVector, new Vector3(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.y, 0) * 2f);
                Gizmos.DrawCube(originVector, new Vector3(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.y, 0) * 2f);
            }
            else
            {
                Gizmos.DrawSphere(originVector, distanciaMinima_limitePerseguir.magnitude);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(originVector, new Vector2(originVector.x, originVector.y + distanciaMinima_limitePerseguir.magnitude));
                Gizmos.DrawLine(originVector, new Vector2(originVector.x + distanciaMinima_limitePerseguir.magnitude, originVector.y));
            }
        }
    }

    //private void SetMirada()
    //{
    //    GLOBAL_TYPE.LADO currLado = transform.position.x < target_transform.transform.position.x ? GLOBAL_TYPE.LADO.der : GLOBAL_TYPE.LADO.iz;
    //    changeMirada.miradaPj(currLado, true);
    //}
}

[System.Serializable]
public class NODE_GO_AlMorir
{
    public GameObject go;
    public float delay;
}