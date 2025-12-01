using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_backgroundContext : MonoBehaviour
{
    int vol_background;

    public int Vol_background { get => vol_background; }

    void Start()
    {
        vol_background = DATA.instance.DataPrefabsConfigurations.Vol_background;
    }

    public void SetVol_background(int newValue)
    {
        vol_background = newValue;
        DATA.instance.DataPrefabsConfigurations.Set_Vol_background(vol_background);
    }

    void Update()
    {
        
    }
}
