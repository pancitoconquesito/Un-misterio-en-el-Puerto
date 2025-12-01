using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPrefabsConfigurations 
{
    InputDeviceDetector.TYPE_INPUT current_input;
    int vol_background;
    int vol_fx;
    public InputDeviceDetector.TYPE_INPUT Current_input { get => current_input;  }
    public int Vol_background { get => vol_background;  }
    public int Vol_fx { get => vol_fx; }

    public DataPrefabsConfigurations()
    {
        int valorInput = PlayerPrefs.GetInt("input", 0);
        current_input = (InputDeviceDetector.TYPE_INPUT)valorInput;// InputDeviceDetector.TYPE_INPUT.Keyboard;

        vol_background = PlayerPrefs.GetInt("vol_background", 100);
        vol_fx = PlayerPrefs.GetInt("vol_fx", 100);
    }

    public void SetInput(InputDeviceDetector.TYPE_INPUT m_curr_input)
    {
        current_input = m_curr_input;
        PlayerPrefs.SetInt("input", (int)m_curr_input);
    }
    public void Set_Vol_background(int newValue)
    {
        vol_background = newValue;
        PlayerPrefs.SetInt("vol_background", vol_background);
    }
    public void Set_Vol_fx(int newValue)
    {
        vol_fx = newValue;
        PlayerPrefs.SetInt("vol_fx", vol_fx);
    }
}
