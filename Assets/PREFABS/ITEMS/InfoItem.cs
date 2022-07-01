using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InfoItem
{
    public string nombre;
    public Sprite sp_imagen;
    [TextArea(minLines: 2, maxLines: 4)] public string descripcion;
}
