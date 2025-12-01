using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using NaughtyAttributes;
[System.Serializable]
[CreateAssetMenu(fileName = "Audio_FX_", menuName = "Audio/FX/PJ")]
public class SO_Audio_FX_PJ : ScriptableObject
{
    [Header("-----Basic")]
    public Sound_FX_BANK.Sound_FX_Names PJ_dash;
    public Sound_FX_BANK.Sound_FX_Names PJ_salto_start;
    public Sound_FX_BANK.Sound_FX_Names PJ_salto_cancel;
    public Sound_FX_BANK.Sound_FX_Names PJ_landed_soft;
    public Sound_FX_BANK.Sound_FX_Names PJ_attack_start;
    public Sound_FX_BANK.Sound_FX_Names PJ_attack_colision;
    public Sound_FX_BANK.Sound_FX_Names PJ_danio;
    public Sound_FX_BANK.Sound_FX_Names PJ_death;
    public List<Sound_FX_BANK.Sound_FX_Names> PJ_l_pisadas;


    [Header("-----Poderes")]
    public Sound_FX_BANK.Sound_FX_Names PJ_disparo;
    //public Sound_FX_BANK.Sound_FX_Names disparo_cancel;
    //public Sound_FX_BANK.Sound_FX_Names disparo_colision;
    //public Sound_FX_BANK.Sound_FX_Names explosion;


    //[Button("SetValues")]

    //public void SetValues()
    //{
    //    // Obtener todos los campos de la clase actual
    //    var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    //    foreach (var field in fields)
    //    {
    //        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
    //        {
    //            continue;  // Saltar la iteración si es una lista
    //        }
    //        if (Enum.TryParse(typeof(Sound_FX_BANK.Sound_FX_UI), field.Name, out var enumValue))
    //        {
    //            field.SetValue(this, enumValue);
    //        }
    //    }
    //}

    [Button("SetValues")]
    public void SetValues()
    {
        PJ_l_pisadas = new List<Sound_FX_BANK.Sound_FX_Names>();
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var enumValues = Enum.GetValues(typeof(Sound_FX_BANK.Sound_FX_Names)).Cast<Sound_FX_BANK.Sound_FX_Names>();
        foreach (var field in fields)
        {
            if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                string listName = field.Name;
                var listInstance = field.GetValue(this) ?? Activator.CreateInstance(field.FieldType);
                var addMethod = field.FieldType.GetMethod("Add");
                var matchingEnumValues = enumValues.Where(e => e.ToString().StartsWith(listName));
                foreach (var enumValue in matchingEnumValues)
                {
                    addMethod.Invoke(listInstance, new object[] { enumValue }); // Convertir a object[]
                }
                field.SetValue(this, listInstance);
            }
            else
            {
                if (Enum.TryParse(typeof(Sound_FX_BANK.Sound_FX_Names), field.Name, out var enumValue))
                {
                    field.SetValue(this, enumValue);
                }
            }
        }
    }


}
