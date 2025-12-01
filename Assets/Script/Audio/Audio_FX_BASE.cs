using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Audio_FX_BASE 
{
    public Audio_FX_Manager m_Audio_FX_Manager = null;
    public string m_subFolder;
    public Audio_FX_BASE(string subFolder)
    {
        m_subFolder = subFolder;
    }
    public void TriggerPlay<T>(T sound) where T: Enum
    {
        if (m_Audio_FX_Manager == null) m_Audio_FX_Manager = MASTER_REFERENCE.instance.AudioManagerContext.Audio_FX_Manager;
        m_Audio_FX_Manager.PlayFX(sound, m_subFolder);
    }
    public void R_TriggerPlay<T>(List<T> l_sound) where T: Enum
    {
        int total = l_sound.Count;
        int r = UnityEngine.Random.Range(0, total);
        if (m_Audio_FX_Manager == null) m_Audio_FX_Manager = MASTER_REFERENCE.instance.AudioManagerContext.Audio_FX_Manager;
        m_Audio_FX_Manager.PlayFX(l_sound[r], m_subFolder);
    }

    public T GetEnumByName<T>(string valor) where T : struct, Enum
    {
        if (Enum.TryParse<T>(valor, out T resultado))
        {
            return resultado;
        }
        else
        {
            return default(T);
        }
    }
    public void UpdateStaticFields<TConfig>(TConfig config, Type targetType)
    {
        var targetFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
        var configFields = typeof(TConfig).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var configField in configFields)
        {
            var value = configField.GetValue(config);
            var targetField = targetFields.FirstOrDefault(f => f.Name == configField.Name);

            if (targetField != null)
            {
                targetField.SetValue(null, value);
            }
        }
    }
    public void PlaySounrByString<T>(string value) where T : struct, Enum
    {
        TriggerPlay(GetEnumByName<T>(value));
    }
}
