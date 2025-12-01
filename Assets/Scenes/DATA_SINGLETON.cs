using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATA_SINGLETON : MonoBehaviour
{
    int id_entrada_siguienteEtapa;
    int id_entrada_siguienteEtapa_NEKO;
    int vidaPj;
    int vidaMAXIMA_pj;
    GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;
    CambiarScene m_cambiarScene;
    string stageInitialSaveRoom;
    string m_currAudioBACK;
    int curr_indexPoder;

    public int Curr_indexPoder { get => curr_indexPoder; set => curr_indexPoder = value; }
    public InputDeviceDetector.TYPE_INPUT Curr_Input { get => m_curr_Input; set => m_curr_Input = value; }

    public string CurrAudioBACK { get => m_currAudioBACK; set => m_currAudioBACK=value; }
    public int Id_entrada_siguienteEtapa { get => id_entrada_siguienteEtapa; set => id_entrada_siguienteEtapa = value; }
    public int Id_entrada_siguienteEtapa_NEKO { get => id_entrada_siguienteEtapa_NEKO; set => id_entrada_siguienteEtapa_NEKO = value; }
    public int VidaPj { get => vidaPj; set => vidaPj = value; }
    public GLOBAL_TYPE.TIPO_ENTRADA TipoEntrada { get => m_tipoEntrada; set => m_tipoEntrada = value; }
    public string StageInitialSaveRoom { get => stageInitialSaveRoom; set => stageInitialSaveRoom = value; }
    public int VidaMAXIMA_pj { get => vidaMAXIMA_pj; set => vidaMAXIMA_pj = value; }

    void Awake()
    {


        //DATA.instance.save_load_system.load_();
        VidaMAXIMA_pj = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.CantidadDeCorazonesTotales;
        VidaPj = VidaMAXIMA_pj;
        StageInitialSaveRoom = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.NameStageSaveRoom;

        if (m_cambiarScene != null)
            m_cambiarScene.setNameScene(StageInitialSaveRoom);

    }


    InputDeviceDetector.TYPE_INPUT m_curr_Input;
    //public void UpdateButtons(InputDeviceDetector.TYPE_INPUT m_curr_input)
    //{
    //    this.Curr_Input = m_curr_input;
    //    //UI
    //    UI_InputUpdate_Context ui_InputUpdate =  MASTER_REFERENCE.instance.UI_Context.UI_InputUpdate;
    //    ui_InputUpdate.UpdateSprites(m_curr_input);
    //}

    private void Start()
    {
        //Debug.Log("Start Singleton");
        //TipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;
    }
    void Update()
    {
        
    }
    public void resetDataMorir()
    {
        VidaPj = VidaMAXIMA_pj;
    }
}
