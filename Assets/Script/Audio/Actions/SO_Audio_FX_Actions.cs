using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Audio_FX_", menuName = "Audio/FX/Actions")]
public class SO_Audio_FX_Actions : ScriptableObject
{
    public Sound_FX_BANK.Sound_FX_Actions Actions_Conversacion_start;
    public Sound_FX_BANK.Sound_FX_Actions Actions_Conversacion_exit;
    public Sound_FX_BANK.Sound_FX_Actions Actions_Conversacion_continue;
    public Sound_FX_BANK.Sound_FX_Actions Actions_Conversacion_endParrafo;
    public Sound_FX_BANK.Sound_FX_Actions Actions_Conversacion_endConversacion;
    //sonidoConversacion_test
}
