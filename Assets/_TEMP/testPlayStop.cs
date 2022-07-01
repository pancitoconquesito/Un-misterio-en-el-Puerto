using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class testPlayStop : MonoBehaviour
{
    public enum TIPO_CANAL
    {
        background, fx, voces
    }
    [SerializeField] private TIPO_CANAL m_tipoCanal;
    [SerializeField] private Color m_colorPlay;
    [SerializeField] private Color m_colorStop;
    [SerializeField] private Button m_btn;
    [SerializeField] private TextMeshProUGUI m_textBtn;
    [SerializeField] private TMP_Dropdown m_dropdown;
    [SerializeField] private AudioClip[] Lista_audios;
    [SerializeField] private TextMeshProUGUI m_labelDropdown;
    private string[] nombresAudios;

    private enum ESTADO_PLAY {  play, stop}
    private ESTADO_PLAY m_estado;
    void Start()
    {
        nombresAudios = new string[Lista_audios.Length];
        for (int i = 0; i < Lista_audios.Length; i++)
        {
            nombresAudios[i] = Lista_audios[i].name;
        }
        m_dropdown.options.Clear();
        for (int i = 0; i < nombresAudios.Length; i++)
        {
            m_dropdown.options.Add(new TMP_Dropdown.OptionData() { text = nombresAudios[i] });
        }
        m_labelDropdown.text = nombresAudios[0];

        m_estado = ESTADO_PLAY.stop;
        ColorBlock cb = m_btn.colors;
        cb.normalColor = m_colorPlay;
        cb.highlightedColor = m_colorPlay;
        m_btn.colors = cb;
    }

    private void updateEstado()
    {
        if (m_estado==ESTADO_PLAY.stop)
        {
            switch (m_tipoCanal)
            {
                case TIPO_CANAL.background:
                    {
                        testAudio.instancia.detenerBackground();
                        break;
                    }
                case TIPO_CANAL.fx:
                    {
                        testAudio.instancia.detenerFX();
                        break;
                    }
                case TIPO_CANAL.voces:
                    {
                        testAudio.instancia.detenerVoces();
                        break;
                    }
            }
        }
        else
        {
            switch (m_tipoCanal)
            {
                case TIPO_CANAL.background:
                    {
                        testAudio.instancia.playBackground(Lista_audios[m_dropdown.value]);
                        break;
                    }
                case TIPO_CANAL.fx:
                    {
                        testAudio.instancia.playFX(Lista_audios[m_dropdown.value]);
                        break;
                    }
                case TIPO_CANAL.voces:
                    {
                        testAudio.instancia.playVoces(Lista_audios[m_dropdown.value]);
                        break;
                    }
            }
        }
    }

    public void BTN_PRESS()
    {
        ColorBlock cb = m_btn.colors;

        if (m_estado == ESTADO_PLAY.stop)
        {
            m_estado = ESTADO_PLAY.play;
            cb.normalColor = m_colorStop;
            cb.highlightedColor = m_colorStop;
            m_textBtn.text = "STOP";
            
        }
        else
        {
            m_estado = ESTADO_PLAY.stop;
            cb.normalColor = m_colorPlay;
            cb.highlightedColor = m_colorPlay;
            m_textBtn.text = "Play";
        }
        m_btn.colors = cb;
        updateEstado();
    }

    public void OnDropDownChanged()
    {
        updateEstado();
    }
}
