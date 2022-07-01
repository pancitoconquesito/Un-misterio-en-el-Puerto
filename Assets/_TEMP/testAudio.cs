using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAudio : MonoBehaviour
{
    public static testAudio instancia;
    [SerializeField] private AudioSource m_audioSRC_background;
    [SerializeField] private AudioSource m_audioSRC_FX;
    [SerializeField] private AudioSource m_audioSRC_voces;
    private float _MASTER;
    private void Awake()
    {
        instancia = this;

        volumeSave_background = PlayerPrefs.GetFloat("volumen_background", 100f);
        volumeSave_fx = PlayerPrefs.GetFloat("volumen_fx", 100f);
        volumeSave_voces = PlayerPrefs.GetFloat("volumen_voces", 100f);
    }
    private float volumeSave_background;
    private float volumeSave_fx;
    private float volumeSave_voces;
    private void Start()
    {
        //cargar volumen desde prefabs
        _MASTER = 1f;
    }
    public float getVolumenSave_background() { return volumeSave_background; }
    public float getVolumenSave_fx() { return volumeSave_fx; }
    public float getVolumenSave_voces() { return volumeSave_voces; }


    [SerializeField] private UnityEngine.UI.Slider[] sliderTEST;
    public void BTN_updateSaveVolumen()
    {
        //TODO
        print("back : "+ sliderTEST[0].value);
        volumeSave_background = sliderTEST[0].value;
        volumeSave_fx = sliderTEST[1].value;
        volumeSave_voces = sliderTEST[2].value;
        updateSaveVolumen();
    }
    private void updateSaveVolumen()
    {
        PlayerPrefs.SetFloat("volumen_background", volumeSave_background);
        PlayerPrefs.SetFloat("volumen_fx", volumeSave_fx);
        PlayerPrefs.SetFloat("volumen_voces", volumeSave_voces);
    }
    public void setVolumenBackground(float valor)
    {
        if (valor > 1) valor /= 100;
        m_audioSRC_background.volume = valor * _MASTER;
    }
    
    public void setVolumen_FX(float valor)
    {
        if (valor > 1) valor /= 100;
        m_audioSRC_FX.volume = valor * _MASTER;
    }
    public void setVolumenVoces(float valor)
    {
        if (valor > 1) valor /= 100;
        m_audioSRC_voces.volume = valor * _MASTER;
    }


    //STOP
    public void detenerBackground() { m_audioSRC_background.Stop(); }
    public void detenerFX() { m_audioSRC_FX.Stop(); }
    public void detenerVoces() { m_audioSRC_voces.Stop(); }
    //PLAY
    public void playBackground(AudioClip _audioClip) { m_audioSRC_background.Stop(); m_audioSRC_background.clip = _audioClip; m_audioSRC_background.Play(); }
    public void playFX(AudioClip _audioClip) { m_audioSRC_FX.Stop(); m_audioSRC_FX.clip = _audioClip; m_audioSRC_FX.Play();}
    public void playVoces(AudioClip _audioClip) { m_audioSRC_voces.Stop(); m_audioSRC_voces.clip = _audioClip; m_audioSRC_voces.Play(); }
}
