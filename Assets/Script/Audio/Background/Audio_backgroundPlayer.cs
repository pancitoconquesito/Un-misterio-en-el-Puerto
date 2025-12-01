using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Audio_backgroundPlayer : MonoBehaviour
{
    [SerializeField] bool m_playOnStart;
    [SerializeField] AUDIO_BACKGROUND m_audioOnPlay;
    [SerializeField] float tiempoInterpolacion;
    AudioSource audioSRC;
    int valueVol;
    [ShowNonSerializedField] AUDIO_BACKGROUND currentAudio;
    [ShowNonSerializedField] AUDIO_BACKGROUND lastAudio;
    
    public bool PlayOnStart { get => m_playOnStart; set => m_playOnStart = value; }
    public AUDIO_BACKGROUND AudioOnPlay { get => m_audioOnPlay; set => m_audioOnPlay = value; }
    public AUDIO_BACKGROUND CurrentAudio { get => currentAudio;  }
    
    private void Start()
    {
        MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer = this;
        valueVol = DATA.instance.DataPrefabsConfigurations.Vol_background;
        audioSRC = GetComponent<AudioSource>();
        if (m_playOnStart)
        {
            currentAudio = AudioOnPlay;
            lastAudio = currentAudio;
            //StartCoroutine(PlayAudio(currentAudio, 0.2f));
            string audioClipPath = "AUDIO/Background/";
            audioClipPath += GetNameAudio<AUDIO_BACKGROUND>(currentAudio);
            AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);
            audioSRC.clip = audioClip;
            Debug.Log($"valueVol / 100: {valueVol / 100} | tiempoInterpolacion: {tiempoInterpolacion}");

            audioSRC.Play();
        }
    }
    //void OnDestroy()
    //{
    //    LeanTween.cancel(gameObject);
    //    LeanTween.cancelAll();
    //}
    public void StartPlayAudio(string audioEnum)
    {
        StartPlayAudio(GetAudioBackgroundEnum(audioEnum));
    }
    public void StartPlayAudio(string _audioEnum, float tiempoBajada, float tiempoSubida)
    {
        AUDIO_BACKGROUND audioEnum = GetAudioBackgroundEnum(_audioEnum);
        audioSRC = GetComponent<AudioSource>();
        MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer = this;
        

        if (lastAudio == audioEnum)
        {
            //Debug.Log("Audio cancelado | audioEnum: " + audioEnum);
            LeanTween.value(gameObject, valueVol / 100, valueVol / 100 * 0.4f, tiempoBajada)
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
            StartCoroutine(SubirAudio(tiempoBajada, tiempoSubida));

            return;
        }

        //bajar audio
        if (currentAudio != AUDIO_BACKGROUND.NONE)
        {
            LeanTween.value(gameObject, valueVol / 100, 0f, tiempoBajada)
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
            StartCoroutine(PlayAudio(audioEnum, tiempoBajada));
        }
        else
        {
            //subir nuevo audio
            StartCoroutine(PlayAudio(audioEnum, tiempoBajada));
        }
    }
    public void BajarAudio()
    {
        LeanTween.value(gameObject, valueVol / 100, valueVol / 100 * 0.4f, 0.5f)
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
    }
    public void StartPlayAudio(AUDIO_BACKGROUND audioEnum)
    {
        audioSRC = GetComponent<AudioSource>();
        MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer = this;
        valueVol = DATA.instance.DataPrefabsConfigurations.Vol_background;
        //Debug.Log($"lastAudio: {lastAudio} | audioEnum: {audioEnum}");
        if (lastAudio == audioEnum)
        {
            //Debug.Log("Audio cancelado | audioEnum: "+ audioEnum);
            LeanTween.value(gameObject, valueVol / 100, valueVol / 100 *0.4f,  1.4f)
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
            StartCoroutine(SubirAudio( 1.6f, 1f));

            return;
        }

        //bajar audio
        if (currentAudio != AUDIO_BACKGROUND.NONE)
        {
            //Debug.Log("bajar audio background");
            LeanTween.value(gameObject, valueVol / 100, 0f, 1.5f)//tiempoInterpolacion
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
            StartCoroutine(PlayAudio(audioEnum, 1.75f));
        }
        else
        {
            //subir nuevo audio
            StartCoroutine(PlayAudio(audioEnum, 0.2f));
        }
    }
    public void StartInitialBackground(AUDIO_BACKGROUND audioEnum)
    {
        if (lastAudio != AUDIO_BACKGROUND.NONE && lastAudio != AUDIO_BACKGROUND.MainMenu)
        {
            return;
        }
        audioSRC = GetComponent<AudioSource>();
        MASTER_REFERENCE.instance.AudioManagerContext.Audio_backgroundPlayer = this;
        valueVol = DATA.instance.DataPrefabsConfigurations.Vol_background;
        audioSRC.volume = 0;
        //if (lastAudio == AUDIO_BACKGROUND.NONE || lastAudio == AUDIO_BACKGROUND.MainMenu)
        //{
        //}
        //else
        //{
        //    audioSRC.volume = valueVol / 100 * 0.4f;
        //}
        //Debug.Log($"lastAudio: {lastAudio} | audioEnum: {audioEnum}");
        //if (lastAudio == audioEnum)
        //{
        //    //Debug.Log("Audio cancelado | audioEnum: "+ audioEnum);
        //    LeanTween.value(gameObject, valueVol / 100, valueVol / 100 * 0.4f, 0.5f)
        //        .setOnUpdate((float volume) =>
        //        {
        //            audioSRC.volume = volume;
        //        })
        //        .setEase(LeanTweenType.easeInOutCubic);
        //    StartCoroutine(SubirAudio(1f, 1f));

        //    return;
        //}

        //bajar audio
        //if (currentAudio != AUDIO_BACKGROUND.NONE)
        //{
            //Debug.Log("bajar audio background");
            //LeanTween.value(gameObject, valueVol / 100, 0f, tiempoInterpolacion * 0.75f)
            //    .setOnUpdate((float volume) =>
            //    {
            //        audioSRC.volume = volume;
            //    })
            //    .setEase(LeanTweenType.easeInOutCubic);
            StartCoroutine(PlayAudio(audioEnum, 0.15f));
        //}
        //else
        //{
        //    //subir nuevo audio
        //    StartCoroutine(PlayAudio(audioEnum, 0.2f));
        //}
    }

    IEnumerator SubirAudio(float delay, float tiempoInterpolacion)
    {
        yield return new WaitForSecondsRealtime(delay);
        LeanTween.value(gameObject, valueVol / 100 * 0.4f, valueVol / 100, tiempoInterpolacion)
                .setOnUpdate((float volume) =>
                {
                    audioSRC.volume = volume;
                })
                .setEase(LeanTweenType.easeInOutCubic);
    }
    IEnumerator PlayAudio(AUDIO_BACKGROUND audioEnum, float delay)
    {
        currentAudio = audioEnum;
        lastAudio = currentAudio;

        yield return new WaitForSecondsRealtime(delay);


        string audioClipPath = "AUDIO/Background/";
        audioClipPath += GetNameAudio<AUDIO_BACKGROUND>(audioEnum);
        AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);
        audioSRC.clip = audioClip;
        audioSRC.Play();
        //Debug.Log($"====> subir audio background: {audioEnum}| delay: {delay} | valueVol: {valueVol/100} | tiempoInterpolacion: {tiempoInterpolacion}");
        LeanTween.value(gameObject, 0f, valueVol / 100, tiempoInterpolacion)
            .setOnUpdate((float volume) =>
            {
                //Debug.Log("*");
                audioSRC.volume = volume;
            })
            .setEase(LeanTweenType.easeInOutCubic);
    }


    public string GetCurrNameBACK() => GetNameAudio<AUDIO_BACKGROUND>(currentAudio);

    string GetNameAudio<T>(AUDIO_BACKGROUND audioEnum) where T: Enum
    {
        if (!audioEnum.Equals(default(T)))
        {
            return audioEnum.ToString();
        }
        else
        {
            return "_default";
        }
    }
    public AUDIO_BACKGROUND GetAudioBackgroundEnum(string enumName)
    {
        if (Enum.TryParse(enumName, out AUDIO_BACKGROUND result))
        {
            return result;
        }
        else
        {
            return AUDIO_BACKGROUND.NONE; // Asegúrate de que exista un valor por defecto en tu enum
        }
    }

    public void Bajarvolumen(float interpolacion)
    {
        LeanTween.value(gameObject, valueVol / 100, 0f, interpolacion)
        .setOnUpdate((float volume) =>
        {
            audioSRC.volume = volume;
        })
        .setEase(LeanTweenType.easeInOutCubic);
    }
    public enum AUDIO_BACKGROUND
    {
        NONE = 0,
        Crossroads = 1,
        Dirtmouth = 2,
        MainMenu
    }
}

