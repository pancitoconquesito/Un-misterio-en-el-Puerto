using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;


public class ZonaNeko_tr : InterationBase
{
    [SerializeField] int indexNeko;
    protected override void StartInteraction()
    {
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        //singleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;
        //singleton.Id_entrada_siguienteEtapa = indexNeko;
        singleton.Id_entrada_siguienteEtapa_NEKO = indexNeko;

        singleton.StageInitialSaveRoom = nombreEscenaActual;
        string audioBack_string = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer.GetCurrNameBACK();
        singleton.CurrAudioBACK = audioBack_string;
        //Debug.Log($"CurrAudioBACK: {audioBack_string}");


        DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom = nombreEscenaActual;
        DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground = audioBack_string;
        DATA.instance.SetIndiceSiguientePosicion(indexNeko);
        DATA.instance.save_load_system.save_();


        Invoke("TerminarInteracion", 1f);
    }


}
