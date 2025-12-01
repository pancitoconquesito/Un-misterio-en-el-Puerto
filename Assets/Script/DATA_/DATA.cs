using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class DATA : MonoBehaviour
{
    public static event Action<GLOBAL_TYPE.IDIOMA> OnSetIdioma;

    public static DATA instance;
    [SerializeField] [Scene] List<string> l_tag_IndexNeko;
    [SerializeField] private ContextoSingleton contexto;
    bool isSceneIndexNEKO;
    testIdiomaSAVE_LOAD idioma_data;
    DataPrefabsConfigurations m_DataPrefabsConfigurations;
    public SAVE_LOAD_SYSTEM save_load_system;
    

    public DataPrefabsConfigurations DataPrefabsConfigurations { get => m_DataPrefabsConfigurations; set => m_DataPrefabsConfigurations = value; }
    public bool IsSceneNormal_noNeko { get => isSceneIndexNEKO; set => isSceneIndexNEKO = value; }

    public ContextoSingleton GetContext()
    {
        return contexto;
    }
    
    private void Awake()
    {
        idioma_data = new testIdiomaSAVE_LOAD();
        DataPrefabsConfigurations = new DataPrefabsConfigurations();
        instance = this;
        UpdateCurrentPower_DATA();
        //string nameMainMenu = SceneManager.GetActiveScene().name;
        //IsSceneNormal_noNeko = true;
        //foreach (string item in l_tag_IndexNeko)
        //{
        //    Debug.Log($"nameMainMenu: {nameMainMenu} | item: {item}");
        //    if (nameMainMenu == item)
        //    {
        //        IsSceneNormal_noNeko = false;
        //        break;
        //    }
        //}

        //Debug.Log("IsSceneNormal_noNeko: " + IsSceneNormal_noNeko);
    }

    public void SetIndiceSiguientePosicion(int valor)
    {
        int indiceNeko = save_load_system.DataGame.DATA_PROGRESS.IndiceNEKO = valor;
        print("ahora vale : "+ indiceNeko);
    }
    public int GetIndiceSiguientePosicion()
    {
        return save_load_system.DataGame.DATA_PROGRESS.IndiceNEKO;
    }

    public GLOBAL_TYPE.IDIOMA getIdioma(){
        return idioma_data.getIdioma();
    }
    public int getVidaActual()
    {
        return contexto.cantidadVidaPJ;
    }
    void Start()
    {
        OnSetIdioma?.Invoke(getIdioma());
    }
    public void updateVidaPJ(int valor)
    {
        contexto.actualizarVida(valor);
    }
    public int GetMaxCantidadVida()
    {
        return save_load_system.DataGame.DATA_PROGRESS.CantidadDeCorazonesTotales;
    }
    public void UpdateCurrentPower_DATA()
    {
        contexto.UpdateCurrentPower(save_load_system.DataGame.DATA_PJ);
    }
    public void UpdateCurrentPower_UI()
    {
        //TODO: lo obtendra de UI y lo guardara en DATA
        contexto.UpdateCurrentPower(save_load_system.DataGame.DATA_PJ);
    }
}
