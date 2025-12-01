using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CambiarScene))]
[RequireComponent(typeof(SpriteRenderer))]
public class GestorCambioScene : MonoBehaviour
{
    public enum TYPE
    {
        SceneChange=0,
        Inmersion = 1
    }
    [SerializeField] TYPE m_type = TYPE.SceneChange;
    [SerializeField][Tag] string m_tagPJ="Player";
    Audio_backgroundPlayer.AUDIO_BACKGROUND nxt_music;
    [SerializeField] private int IndiceSiguientePosicion;
    [SerializeField] private GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;

    Animator m_animatorTransicion;
    BoxCollider2D mBoxCollider2D;
    CambiarScene m_cambiarScene;
    bool completado = false;
    movementPJ m_movementPJ;
    Animator anim_PJ;
    changeMirada m_changeMirada;
    Audio_backgroundPlayer audioPlayer_background;
    string currScene;
    // Start is called before the first frame update
    private void Awake()
    {
        mBoxCollider2D = GetComponent<BoxCollider2D>();
        m_cambiarScene = GetComponent<CambiarScene>();
    }
    void Start()
    {
        //currScene = SceneManager.GetActiveScene().name;
        SO_ScaneData dtaScene_SO = Resources.Load<SO_ScaneData>("DATA/DATA_Hardcore_SCENE");
        audioPlayer_background = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer;
        foreach (var item in dtaScene_SO.Scenes_DATA)
        {
            if (item.m_nameScene == m_cambiarScene.NameStage)
            {
                nxt_music = item.m_audioBackgroundEnum;
            }
        }


        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
        anim_PJ = m_movementPJ.AnimatorPJ;
        m_changeMirada = MASTER_REFERENCE.instance.ChangeMirada;
        m_animatorTransicion = MASTER_REFERENCE.instance.UI_Context.At_transiciones;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!completado && collision.CompareTag(m_tagPJ) && m_movementPJ.GetState()!=GLOBAL_TYPE.ESTADOS.entrandoScene)
        {
            anim_PJ.SetBool("SuccessInmersion", m_type==TYPE.Inmersion);
            completado = true;
            DATA_SINGLETON m_dataSingleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
            m_dataSingleton.Id_entrada_siguienteEtapa = IndiceSiguientePosicion;
            //m_dataSingleton.TipoEntrada = m_tipoEntrada;

            switch (m_tipoEntrada)
            {
                case GLOBAL_TYPE.TIPO_ENTRADA.iz_caminando:
                {
                    m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_caminando;
                    m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_caminando;
                    break;
                }
                case GLOBAL_TYPE.TIPO_ENTRADA.der_caminando:
                    {
                        m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_caminando;
                        m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_caminando;
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.CAYENDO:
                    {
                        if (m_changeMirada.getMirada() == GLOBAL_TYPE.LADO.iz)
                        {
                            m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo;
                            m_tipoEntrada= GLOBAL_TYPE.TIPO_ENTRADA.iz_cayendo;
                        }
                        else
                        {
                            m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo;
                            m_tipoEntrada= GLOBAL_TYPE.TIPO_ENTRADA.der_cayendo;
                        }
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.SALTANDO:
                    {
                        if (m_changeMirada.getMirada() == GLOBAL_TYPE.LADO.iz)
                        {
                            m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_salto;
                            m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.iz_salto;
                        }
                        else
                        {
                            m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_salto;
                            m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.der_salto;
                        }
                        break;
                    }
                case GLOBAL_TYPE.TIPO_ENTRADA.Inmersion:
                    {
                        m_dataSingleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.Inmersion;
                        m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.Inmersion;
                        break;
                    }
            }
            m_animatorTransicion.SetTrigger("finishStage");
            m_movementPJ.setTipoEntrada(m_tipoEntrada);
            //GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().TipoEntrada=m_tipoEntrada;

            //actualziar currentPower
            DATA.instance.UpdateCurrentPower_UI();

            audioPlayer_background = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;
            //audioPlayer_background.BajarAudio();
            audioPlayer_background.StartPlayAudio(nxt_music);

            //Debug.Break();

            Invoke("aaa",2f);
        }
    }
    private void aaa()
    {
        m_cambiarScene.changeScene();
    }

    //public void ChangeToMainMenu()
    //{
    //    audioPlayer_background = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;
    //    audioPlayer_background.StartPlayAudio(Audio_backgroundPlayer.AUDIO_BACKGROUND.MainMenu);
    //}
}
