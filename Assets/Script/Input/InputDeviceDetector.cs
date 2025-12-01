using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class InputDeviceDetector : MonoBehaviour
{
    public static event Action<InputDeviceDetector.TYPE_INPUT> OnChangedInput;

    DATA_SINGLETON dATA_SINGLETON;
    public enum TYPE_INPUT
    {
        Keyboard=0, 
        Gamepad=1, 
        XBOX=2, 
        PS4=3, 
        SWITCH=4, 
        Other=5
    }
    TYPE_INPUT m_curr_input;
    TYPE_INPUT m_last_input;
    private InputAction inputAction;
    public TYPE_INPUT Curr_input { get => m_curr_input; set => m_curr_input = value; }
    //private void Awake()
    //{
    //}
    private void Start()
    {
        m_curr_input = DATA.instance.DataPrefabsConfigurations.Current_input;
        m_last_input = m_curr_input;
        OnChangedInput?.Invoke(m_curr_input);
        //dATA_SINGLETON = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        //dATA_SINGLETON.UpdateButtons(m_curr_input);//! TODO
    }
    private void OnEnable()
    {
        inputAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<button>");
        inputAction.performed += OnAnyInput;
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.performed -= OnAnyInput;
        inputAction.Disable();
    }
    private void OnAnyInput(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        if (device is Keyboard)
        {
            m_curr_input = TYPE_INPUT.Keyboard;
        }
        else if (device is Gamepad)
        {
            var gamepad = (Gamepad)device;
            if (gamepad.name.Contains("DualShock"))
            {
                Debug.Log("Input received from PS4 Controller");
                m_curr_input = TYPE_INPUT.PS4;
            }
            else if (gamepad.name.Contains("XInputControllerWindows"))
            {
                //Debug.Log("Input received from Xbox Controller");
                m_curr_input = TYPE_INPUT.XBOX;
            }
            else if (gamepad.name.Contains("SwitchProControllerHID") || gamepad.name.Contains("JoyCon"))
            {
                Debug.Log("Input received from Nintendo Switch Controller");
                m_curr_input = TYPE_INPUT.SWITCH;
            }
            else
            {
                Debug.Log("Input received from a Gamepad");
                m_curr_input = TYPE_INPUT.Other;
            }
        }
        if(m_last_input != m_curr_input)
        {
            m_last_input = m_curr_input;
            CambioDeInput();
        }
    }

    private void CambioDeInput()
    {
        OnChangedInput?.Invoke(m_curr_input);
        DATA.instance.DataPrefabsConfigurations.SetInput(m_curr_input);
    }
}
