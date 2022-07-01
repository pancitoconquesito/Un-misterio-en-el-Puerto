using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class setValueWithSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private Slider m_slider;
    public enum CANAL_AUDIO{background, fx, voces}
    [SerializeField] private CANAL_AUDIO m_canalAudio;
    void Start()
    {
        m_slider.onValueChanged.AddListener(delegate { onSliderChange(); });
        switch (m_canalAudio)
        {
            case CANAL_AUDIO.background:
                {
                    m_slider.value = testAudio.instancia.getVolumenSave_background();
                    break;
                }
            case CANAL_AUDIO.fx:
                {
                    m_slider.value = testAudio.instancia.getVolumenSave_fx();
                    break;
                }
            case CANAL_AUDIO.voces:
                {
                    m_slider.value = testAudio.instancia.getVolumenSave_voces();
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onSliderChange()
    {
        m_slider.value = GLOBAL_TYPE.Round(m_slider.value,1);
        texto.text= m_slider.value + " / 100";
        updateVolumen(m_canalAudio, m_slider.value);
    }
    private void updateVolumen(CANAL_AUDIO canal, float volumen)
    {
        switch (canal)
        {
            case CANAL_AUDIO.background:
                {
                    testAudio.instancia.setVolumenBackground(m_slider.value);
                    break;
                }
            case CANAL_AUDIO.fx:
                {
                    testAudio.instancia.setVolumen_FX(m_slider.value);
                    break;
                }
            case CANAL_AUDIO.voces:
                {
                    testAudio.instancia.setVolumenVoces(m_slider.value);
                    break;
                }
        }
    }
}
