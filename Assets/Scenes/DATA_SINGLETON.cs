using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATA_SINGLETON : MonoBehaviour
{
    public int id_entrada_siguienteEtapa;
    public int vidaPj;
    public int vidaMAXIMA_pj;
    public GLOBAL_TYPE.TIPO_ENTRADA m_tipoEntrada;
    public CambiarScene m_cambiarScene;
    public string stageInitialSaveRoom;

    void Awake()
    {

        m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;

        
    }
    private void Start()
    {
        DATA.instance.save_load_system.load_();
        vidaMAXIMA_pj = DATA.instance.save_load_system.m_dataGame.m_DATA_PROGRESS.cantidadDeCorazonesTotales;
        vidaPj = vidaMAXIMA_pj;
        stageInitialSaveRoom = DATA.instance.save_load_system.m_dataGame.m_DATA_PROGRESS.nameStageSaveRoom;

        if (m_cambiarScene != null)
            m_cambiarScene.setNameScene(stageInitialSaveRoom);
    }
    void Update()
    {
        
    }
    public void resetDataMorir()
    {
        vidaPj = vidaMAXIMA_pj;
    }
}
