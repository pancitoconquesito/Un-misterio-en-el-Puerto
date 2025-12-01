using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using System;

public class ParedRompible_memoria : MonoBehaviour, IDamageable
{
    public enum Letras
    {
        A,B,C,D,E,F,G,H,I,J,K,L
    }
    [SerializeField] Letras m_letras;
    [SerializeField] int m_maxVida;
    [SerializeField] Animator m_anim;
    [SerializeField] float m_timing;
    [SerializeField] ObjectPooling m_particulas;
    [SerializeField] BoxCollider2D m_boxCollider_TRIGGER;
    [SerializeField] BoxCollider2D m_boxCollider_SOLIDO;
    [ShowNonSerializedField] int m_curr_vida;
    string key;
    bool curr_value;
    float m_curr_timing;
    bool habilitado;
    DATA_OBJ_Persistentes objClass;
    private void Awake()
    {
        m_curr_vida = m_maxVida;
        habilitado = true;
        m_curr_timing = m_timing;
    }

    private void Start()
    {
        key = $"{SceneManager.GetActiveScene().name}[{ m_letras.ToString()}]";
        objClass=DATA.instance.save_load_system.DataGame.DATA_OBJ_Persistentes;
        curr_value = objClass.GetValueByKey(key);
        if (curr_value)
        {
            DestruirSinAnim();
        }
    }
    private void Update()
    {
        if(habilitado && m_curr_timing > -1)    m_curr_timing -= Time.deltaTime;
    }
    public bool IsEnemy() => false;


    public bool RecibirDanio_I(dataDanio m_dataDanio)
    {
        if(!habilitado || m_curr_timing>0)
        {
            return false;
        }

        m_curr_vida -= m_dataDanio.danio;
        Debug.Log("danio;: "+ m_dataDanio.danio);
        m_curr_timing = m_timing;
        m_anim.SetTrigger("Danio");
        m_particulas.emitirObj(0.8f);

        if (m_curr_vida < 0)
        {
            DestruirAnim();
            SaveCambio();
        }

        return true;
    }

    private void SaveCambio()
    {
        objClass.Changevalue(key, true);
        DATA.instance.save_load_system.save_();
        Debug.Log("Pared destruida guardada en memoria");
    }

    void DestruirAnim()
    {
        m_boxCollider_TRIGGER.enabled = false;
        m_boxCollider_SOLIDO.enabled = false;
        habilitado = false;
    }
    void DestruirSinAnim()
    {
        m_boxCollider_TRIGGER.enabled = false;
        m_boxCollider_SOLIDO.enabled = false;
        habilitado = false;
    }
}
