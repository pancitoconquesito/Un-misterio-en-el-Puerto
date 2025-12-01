using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class TestAnim_LeanUI : MonoBehaviour
{
    public RectTransform imageRectTransform; // Asigna la imagen desde el inspector
    public float intensity = 10f; // Intensidad del temblor
    public float duration = 0.5f; // Duración del temblor
    public float waveIntensity = 0.05f;

    public enum Level
    {
        BAJO=0, 
        MEDIO=1, 
        ALTO=2
    }

    [System.Serializable]
    public struct NodeParam
    {
        public float intensity;
        public float duration;
        public int countLoop;
    }

    public List<NodeParam> param_Nervioso;
    public List<NodeParam> param_Angry;


    public void Nervioso(Level lvl)
    {
        int index = (int)lvl;
        Nerviso(param_Nervioso[index].intensity, param_Nervioso[index].duration, param_Nervioso[index].countLoop);
    }
    void Nerviso(float _intensity, float _duration, int _countLoop)//25, 0.25f
    {
        Vector3 originalPosition = imageRectTransform.localPosition;
        LeanTween.moveLocal(imageRectTransform.gameObject, originalPosition + new Vector3(Random.Range(-_intensity, _intensity), Random.Range(-_intensity, _intensity), 0), _duration / (float)_countLoop)
            .setLoopPingPong(_countLoop)
            .setEase(LeanTweenType.linear)
            .setOnComplete(() => imageRectTransform.localPosition = originalPosition);
    }

    //
    public void AngryMovement(Level lvl)
    {
        int index = (int)lvl;
        Nerviso(param_Angry[index].intensity, param_Angry[index].duration, param_Angry[index].countLoop);
    }
    void AngryMovement(float _intensity, float _duration, int _countLoop)//25, 0.25
    {
        Vector3 originalPosition = imageRectTransform.localPosition;
        LeanTween.moveLocalX(imageRectTransform.gameObject, originalPosition.x + _intensity, _duration / (float)_countLoop)
            .setLoopPingPong(_countLoop) // 4 ciclos de ida y vuelta (rápido)
            .setEase(LeanTweenType.easeShake)
            .setOnComplete(() => imageRectTransform.localPosition = originalPosition); // Restaura la posición original

        LeanTween.rotateZ(imageRectTransform.gameObject, 5f, _duration / (float)_countLoop)
            .setLoopPingPong(_countLoop)
            .setEase(LeanTweenType.easeShake);
    }

    //
    [Button]
    public void Alerta()
    {
        SadMovement(40, 0.1f, 1);
    }
    [Button]
    public void Sad()
    {
        SadMovement(30,0.7f, 1);
    }
    public void SadMovement(float _intensity, float _duration, int _countLoop)
    {
        Vector3 originalPosition = imageRectTransform.localPosition;
        LeanTween.moveLocalY(imageRectTransform.gameObject, originalPosition.y - _intensity, _duration / 2f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong(_countLoop) // Movimiento de ida y vuelta (caída y ligera subida)
            .setOnComplete(() => imageRectTransform.localPosition = originalPosition); // Restaura la posición original    }


    }


    [Button]
    public void StartWaveEffect()//wave 0.1, 0.4f durancion
    {
        LeanTween.scaleX(imageRectTransform.gameObject, 1 + waveIntensity, duration / 2)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong(2);

        LeanTween.scaleY(imageRectTransform.gameObject, 1 - waveIntensity, duration / 2)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong(2);
    }
}
