using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Audio_FX_Manager : MonoBehaviour
{
    [SerializeField] SO_Audio_FX_PJ m_SO_Audio_FX_PJ;
    [SerializeField] SO_Audio_FX_UI m_SO_Audio_FX_UI;
    [SerializeField] SO_Audio_FX_Actions m_SO_Audio_FX_Actions;
    [SerializeField] int totalAudiSRC;
    List<NodeAudioFX> l_NodesAudio;

    private void Awake()
    {
        Audio_FX_PJ.Configure(m_SO_Audio_FX_PJ, "PJ");
        Audio_FX_UI.Configure(m_SO_Audio_FX_UI, "UI");
        Audio_FX_Actions.Configure(m_SO_Audio_FX_Actions, "ACTIONS");
    }

    private void Start()
    {
        float volumenFX = DATA.instance.DataPrefabsConfigurations.Vol_fx / 100;

        l_NodesAudio = new List<NodeAudioFX>();
        foreach (var _ in Enumerable.Range(0, totalAudiSRC))
        {
            AudioSource audioSRC = gameObject.AddComponent<AudioSource>();
            audioSRC.volume = volumenFX;
            audioSRC.playOnAwake = false;
            audioSRC.enabled = false;
            NodeAudioFX newNode = new NodeAudioFX();
            newNode.audioSRC = audioSRC;
            newNode.InUSE = false;
            l_NodesAudio.Add(newNode);
        }
    }

    private void Update()
    {
        foreach (NodeAudioFX item in l_NodesAudio)
        {
            item.InUSE = item.audioSRC.isPlaying;
            if (!item.InUSE && item.audioSRC.enabled)
            {
                item.audioSRC.enabled = false;
            }
        }
    }


    public void PlayFX<T>(T fxName, string subFolder) where T: Enum
    {
        foreach (NodeAudioFX item in l_NodesAudio)
        {
            if (!item.audioSRC.isActiveAndEnabled)
            {
                item.audioSRC.enabled = true;
                string audioClipPath = $"AUDIO/FX/{subFolder}/";
                audioClipPath += Sound_FX_BANK.GetNameAudio(fxName);
                //Debug.Log($"audioClipPath: {audioClipPath}");
                AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);
                if (audioClip == null)
                {
                    Debug.Log("PathAudio NO encontrado | ruta: "+ audioClipPath);
                }
                item.audioSRC.clip = audioClip;
                item.audioSRC.PlayOneShot(audioClip);
                break;
            }
        }
    }

    
}
public class NodeAudioFX
{
    public AudioSource audioSRC;
    public bool InUSE;
}
