using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using static Audio_backgroundPlayer;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif


[System.Serializable]
[CreateAssetMenu(fileName = "DATA_Hardcore_SCENE", menuName = "DATA/Hardcore/SCENE")]
public class SO_ScaneData : ScriptableObject
{
    public List<SO_NODE_SO_ScaneData> Scenes_DATA;

    [Button]
    public void AddNewScene()
    {
        #if UNITY_EDITOR
        string nameScene = EditorSceneManager.GetActiveScene().name;
        SO_NODE_SO_ScaneData newSceneData = new SO_NODE_SO_ScaneData();
        newSceneData.m_nameScene = nameScene;
        newSceneData.m_colorBackground = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor;
        newSceneData.m_audioBackgroundEnum = AUDIO_BACKGROUND.Crossroads;
        Scenes_DATA.Add(newSceneData);
#endif

    }
}


[System.Serializable]
public class SO_NODE_SO_ScaneData
{
    [Scene]public string m_nameScene;
    public Color m_colorBackground;
    public AUDIO_BACKGROUND m_audioBackgroundEnum;
}

