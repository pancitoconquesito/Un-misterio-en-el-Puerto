using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnPJ : MonoBehaviour
{
    [SerializeField]private float delayRespawn;
    Audio_backgroundPlayer audioBACK;
    Audio_backgroundPlayer.AUDIO_BACKGROUND audioSceneRespwn;
    string str_audioSceneRespwn;
    int indiceNeko;
    string stageInitialSaveRoom;
    private void Start()
    {
        audioBACK = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer;
        //audioBACK = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;

        //data
        str_audioSceneRespwn = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground;
        indiceNeko = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.IndiceNEKO;
        stageInitialSaveRoom = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;

        //singleton
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        //singleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;
        singleton.Id_entrada_siguienteEtapa_NEKO = indiceNeko;
        
    }
    public void respawn()
    {
        str_audioSceneRespwn = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground;
        audioBACK.StartPlayAudio(str_audioSceneRespwn, 3, 3);
        //data
        str_audioSceneRespwn = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground;
        indiceNeko = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.IndiceNEKO;
        stageInitialSaveRoom = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;
        Invoke("changeScene", delayRespawn);
    }
    private void changeScene()
    {
        //singleton
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        singleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;
        singleton.Id_entrada_siguienteEtapa = indiceNeko;
        singleton.Id_entrada_siguienteEtapa_NEKO = indiceNeko;

        string scenName = stageInitialSaveRoom;
        //Debug.Log("iNITIAL INDEX: " + indexNeko);
        SceneManager.LoadScene(scenName);
    }
}
