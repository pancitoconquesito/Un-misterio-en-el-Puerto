using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class SetLayerNS : MonoBehaviour
{
    public enum TipoEnemigo
    {
        Normal,
        Atraviesa_tru
    }

    [OnValueChanged("ActualizarLayer")]
    public TipoEnemigo tipo;

    [ReadOnly]
    [Tooltip("Layer asignado automáticamente según el tipo de enemigo.")]
    public string layerAsignado;


    [Button]
    private void ActualizarLayer()
    {
        switch (tipo)
        {
            case TipoEnemigo.Normal:
                layerAsignado = "Platform_NS";
                break;
            case TipoEnemigo.Atraviesa_tru:
                layerAsignado = "Platform_NS_suelo";
                break;
        }

        int layerIndex = LayerMask.NameToLayer(layerAsignado);
        if (layerIndex != -1)
            gameObject.layer = layerIndex;
        else
            Debug.LogWarning($"El layer '{layerAsignado}' no existe en el proyecto.");
    }

    private void Reset()
    {
        ActualizarLayer(); // Para que se asigne automáticamente al agregar el componente
    }
}
