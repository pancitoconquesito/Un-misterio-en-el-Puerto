using UnityEngine;
using System;
using System.Collections;

public class RecibiDanio_cb : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject[] m_l_goDestroy;
    [SerializeField] dataDanio.QuienEmiteDanio queTipoSoy;
    [SerializeField] int cantidadVida;
    [SerializeField][Range(0.1f, 1f)] float cadenciaRecibeDanio = 0.3f;
    [SerializeField] bool isEnemy;
    [SerializeField] ObjectPooling op_onRecibir;
    [SerializeField] ObjectPooling op_onMorir;
    [SerializeField] FlashSprite flashSprite;
    [SerializeField] NODE_GO_AlMorir[] op_l_alMorir;
    [SerializeField] bool destruirObjeto = true;
    [SerializeField] float delayDestroy = 0.5f;
    [SerializeField] float factorRetrocesoAlRecibirDanio = 5f;
    //[SerializeField] Rigidbody2D rb;
    //[SerializeField] MovimientoPatrulla movimientoPatrulla;


    public event Action<Vector2> OnRecibirDanio;
    public event Action OnMorir;
    bool isVivo=true;
    float curr_cadenciaRecibeDanio;
    public bool IsVivo { get => isVivo; set => isVivo = value; }

    public bool IsEnemy()=>isEnemy;

    private void Start()
    {
        curr_cadenciaRecibeDanio = cadenciaRecibeDanio;
    }
    private void Update()
    {
        if(isVivo && curr_cadenciaRecibeDanio>0f)
        {
            curr_cadenciaRecibeDanio -= Time.deltaTime;
        }
    }

    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if (cantidadVida <= 0 || curr_cadenciaRecibeDanio>0f || m_dataDanio.QuienAtaca == queTipoSoy)
        {
            return false;
        }

        cantidadVida -= m_dataDanio.danio;
        if (op_onRecibir != null)
        {
            op_onRecibir.emitirObj(0.8f, true);
        }
        curr_cadenciaRecibeDanio = cadenciaRecibeDanio;
        if (flashSprite != null)
        {
            flashSprite.Flashear();
        }
        Vector3 dirEmpuje = (transform.position - m_dataDanio.m_transformAtacante.position).normalized;
        Vector2 velocidadImpacto = m_dataDanio.getImpactoEmpuje() * dirEmpuje * factorRetrocesoAlRecibirDanio;
        //movimientoPatrulla.SetFuerzaImpacto(velocidadImpacto);
        OnRecibirDanio?.Invoke(velocidadImpacto);
        if (cantidadVida <= 0)
        {
            Morir();
        }
        return true;
    }

    private void Morir()
    {
        IsVivo = false;
        if (op_onMorir != null)
        {
            op_onMorir.emitirObj(0.8f);
        }
        OnMorir?.Invoke();
        Debug.Log("Mori!");

        if (op_l_alMorir != null && op_l_alMorir.Length > 0)
        {
            foreach (var item in op_l_alMorir)
            {
                StartCoroutine(SpawnPO(item.go, item.delay));
            }
        }
        if (destruirObjeto)
        {
            foreach (var item in m_l_goDestroy)
            {
                Destroy(item, delayDestroy);
            }
        }
    }

    IEnumerator SpawnPO(GameObject go, float delay)
    {
        //Debug.Log("SpawnPO " + delay);
        yield return new WaitForSeconds(delay);
        Instantiate(go, transform.position, Quaternion.identity);
    }
}
