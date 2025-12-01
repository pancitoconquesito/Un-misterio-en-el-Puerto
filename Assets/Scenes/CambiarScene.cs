using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class CambiarScene : MonoBehaviour
{
    [SerializeField][Scene] private string nameStage;
    [SerializeField] private float delay;
    [SerializeField] private bool StartChange;

    public string NameStage { get => nameStage; set => nameStage = value; }

    private void Start()
    {
        if(StartChange) Invoke("changeStageWithOutDATA", delay);
    }
    public void setNameScene(string value)
    {
        nameStage = value;
    }
    public void changeScene()
    {
        Invoke("changeStageNow",delay);
    }
    public void changeScene(string _name)
    {
        nameStage = _name;
        Invoke("changeStageNow", delay);
    }
    public void changeScene(float _delay)
    {
        Invoke("changeStageNow", _delay);
    }
    public void changeScene(string _name, float _delay)
    {
        nameStage = _name;
        Invoke("changeStageNow()", _delay);
    }
    private void changeStageNow()
    {
        if(nameStage=="asdf") nameStage= DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;
        SceneManager.LoadScene(nameStage);
    }
    public void ChangeSceneDesdeMain()
    {
        //obtener indexNXTScene desde DATA, pasarselo a singleton
        int indeceNeko = DATA.instance.GetIndiceSiguientePosicion();
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        singleton.Id_entrada_siguienteEtapa_NEKO = singleton.Id_entrada_siguienteEtapa_NEKO;
        singleton.Id_entrada_siguienteEtapa = singleton.Id_entrada_siguienteEtapa_NEKO;
        singleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.nada;

        nameStage = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;
        changeScene();
    }
    public void ChangeSceneHacia_Neko()
    {
        int indeceNeko = DATA.instance.GetIndiceSiguientePosicion();
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        singleton.Id_entrada_siguienteEtapa_NEKO = singleton.Id_entrada_siguienteEtapa_NEKO;
        singleton.Id_entrada_siguienteEtapa = singleton.Id_entrada_siguienteEtapa_NEKO;
        singleton.TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.nada;

        string nameNekoMusic = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground;
        Audio_backgroundPlayer audioPlayer_background = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;
        audioPlayer_background.StartPlayAudio(nameNekoMusic);

        MASTER_REFERENCE.instance.Ui_Anim.DesaparecerUI_Bloquear();
        MASTER_REFERENCE.instance.UI_Context.At_transiciones.SetTrigger("finishStage");

        nameStage = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;////////!!!
        changeScene();
    }
    private void changeStageWithOutDATA()
    {
        SceneManager.LoadScene(nameStage);
    }

    public void SetAudioHaciaMenu()
    {
        Audio_backgroundPlayer audioPlayer_background = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;
        MASTER_REFERENCE.instance.Ui_Anim.DesaparecerUI_Bloquear();
        MASTER_REFERENCE.instance.UI_Context.At_transiciones.SetTrigger("finishStage");
        audioPlayer_background.StartPlayAudio(Audio_backgroundPlayer.AUDIO_BACKGROUND.MainMenu);
        changeScene();
    }
    public void SetAudioDesdeMenu()
    {
        Audio_backgroundPlayer audioPlayer_background = MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer;
        string audioBCAK = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameBackground;
        Debug.Log($"audioBCAK: {audioBCAK}");
        Audio_backgroundPlayer.AUDIO_BACKGROUND audioBACK_enum = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer.GetAudioBackgroundEnum(audioBCAK);

        audioPlayer_background.Bajarvolumen(2);
        //audioPlayer_background.StartPlayAudio(audioBACK_enum);
    }
}
