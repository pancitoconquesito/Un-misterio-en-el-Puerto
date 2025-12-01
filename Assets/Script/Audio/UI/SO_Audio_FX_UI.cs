using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using NaughtyAttributes;
[System.Serializable]
[CreateAssetMenu(fileName = "Audio_FX_", menuName = "Audio/FX/UI")]
public class SO_Audio_FX_UI : ScriptableObject
{
    //inventario
    public Sound_FX_BANK.Sound_FX_UI UI_start;
    public Sound_FX_BANK.Sound_FX_UI UI_exit;
    public Sound_FX_BANK.Sound_FX_UI UI_moveIn;
    public Sound_FX_BANK.Sound_FX_UI UI_changePanel;
    public Sound_FX_BANK.Sound_FX_UI UI_select;
    //mapa
    public Sound_FX_BANK.Sound_FX_UI UI_MAPA_enter;
    public Sound_FX_BANK.Sound_FX_UI UI_MAPA_exit;
    public Sound_FX_BANK.Sound_FX_UI UI_MAPA_moving;
    public Sound_FX_BANK.Sound_FX_UI UI_MAPA_ZoomIn;
    public Sound_FX_BANK.Sound_FX_UI UI_MAPA_ZoomOut;

    [Button("SetValues")]
    public void SetValues()
    {
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (Enum.TryParse(typeof(Sound_FX_BANK.Sound_FX_UI), field.Name, out var enumValue))
            {
                field.SetValue(this, enumValue);
            }
        }
    }

}
