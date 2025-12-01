using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GLOBAL_TYPE;

public class SceneConfig : MonoBehaviour
{
    //[SerializeField] Color m_colorBackground;
    [Header("Luces pjmAPA")]
    [SerializeField] TipoSombra tipoSombra;

    Camera m_mainCamera;
    string currScene;
    Audio_backgroundPlayer audioBackground;
    private void Start()
    {
        if(tipoSombra == TipoSombra.menu)
        {
            return;
        }
        SpriteRenderer sombra_ini = MASTER_REFERENCE.instance.Sombra_ini;
        SpriteRenderer sombra_midd = MASTER_REFERENCE.instance.Sombra_midd;
        SpriteRenderer sombra_lejos = MASTER_REFERENCE.instance.Sombra_lejos;
        sombra_ini.color = GLOBAL_TYPE.GetColor_sombra(tipoSombra, Sombra.ini);
        sombra_midd.color = GLOBAL_TYPE.GetColor_sombra(tipoSombra, Sombra.ini);
        sombra_lejos.color = GLOBAL_TYPE.GetColor_sombra(tipoSombra, Sombra.lejos);

        currScene = SceneManager.GetActiveScene().name;
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        SO_ScaneData dtaScene_SO = Resources.Load<SO_ScaneData>("DATA/DATA_Hardcore_SCENE");
        audioBackground = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>().Audio_backgroundPlayer;
        foreach (var item in dtaScene_SO.Scenes_DATA)
        {
            if(item.m_nameScene == currScene)
            {
                m_mainCamera.backgroundColor = item.m_colorBackground;
                audioBackground.StartInitialBackground(item.m_audioBackgroundEnum);
            }
        }
    }


}