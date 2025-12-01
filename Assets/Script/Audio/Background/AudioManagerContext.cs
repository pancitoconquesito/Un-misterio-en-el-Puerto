using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerContext : MonoBehaviour
{
    [SerializeField] Audio_backgroundPlayer m_Audio_backgroundPlayer;
    [SerializeField] Audio_FX_Manager m_Audio_FX_Manager;

    public Audio_backgroundPlayer Audio_backgroundPlayer { get => m_Audio_backgroundPlayer; set => m_Audio_backgroundPlayer = value; }
    public Audio_FX_Manager Audio_FX_Manager { get => m_Audio_FX_Manager; set => m_Audio_FX_Manager = value; }
}
