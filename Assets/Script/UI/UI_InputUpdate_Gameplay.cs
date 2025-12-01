using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_InputUpdate_Gameplay : MonoBehaviour
{
    
    [Header("--Pause--")]
    [SerializeField] Image img_pause;
    [SerializeField] Sprite sp_pause_Teclado;
    [SerializeField] Sprite sp_pause_Gamepad;

    [Header("--Dash--")]
    [SerializeField] Image img_dash;
    [SerializeField] Sprite sp_dash_Teclado;
    [SerializeField] Sprite sp_dash_Gamepad;

    [Header("--Telekniesis--")]
    [SerializeField] Image img_Telekiniesis;
    [SerializeField] Sprite sp_Telekiniesis_Teclado;
    [SerializeField] Sprite sp_Telekiniesis_Gamepad;

    [Header("--Poder--")]
    [SerializeField] Image img_poder;
    [SerializeField] Sprite sp_poder_Teclado;
    [SerializeField] Sprite sp_poder_Gamepad;

    [Header("--Poder IZ--")]
    [SerializeField] Image img_poderIZ;
    [SerializeField] Sprite sp_poderIZ_Teclado;
    [SerializeField] Sprite sp_poderIZ_Gamepad;

    [Header("--Poder DER--")]
    [SerializeField] Image img_poderDER;
    [SerializeField] Sprite sp_poderDER_Teclado;
    [SerializeField] Sprite sp_poderDER_Gamepad;


    private void Start()
    {
        InputDeviceDetector.OnChangedInput += UpdateSprites;
    }
    private void OnDisable()
    {
        InputDeviceDetector.OnChangedInput -= UpdateSprites;
    }
    private void OnDestroy()
    {
        InputDeviceDetector.OnChangedInput -= UpdateSprites;
    }
    public void UpdateSprites(InputDeviceDetector.TYPE_INPUT m_curr_input)
    {
        Console.WriteLine($"[UpdateSprites] UI SetButtons: {m_curr_input}");

        switch (m_curr_input)
        {
            case InputDeviceDetector.TYPE_INPUT.Gamepad:
                {
                    img_pause.sprite = sp_pause_Gamepad;
                    img_dash.sprite = sp_dash_Gamepad;
                    img_Telekiniesis.sprite = sp_Telekiniesis_Gamepad;
                    break;
                }
            case InputDeviceDetector.TYPE_INPUT.Keyboard:
                {
                    img_pause.sprite = sp_pause_Teclado;
                    img_dash.sprite = sp_dash_Teclado;
                    img_Telekiniesis.sprite = sp_Telekiniesis_Teclado;
                    break;
                }
            case InputDeviceDetector.TYPE_INPUT.XBOX:
                {
                    img_pause.sprite = sp_pause_Gamepad;
                    img_dash.sprite = sp_dash_Gamepad;
                    img_Telekiniesis.sprite = sp_Telekiniesis_Gamepad;

                    break;
                }
            case InputDeviceDetector.TYPE_INPUT.SWITCH:
                {

                    break;
                }
            case InputDeviceDetector.TYPE_INPUT.PS4:
                {

                    break;
                }

            default:
                {
                    //!TODO Gamepad?
                    break;
                }
        }
    }
}
