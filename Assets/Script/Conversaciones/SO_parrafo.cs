using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CreateAssetMenu(fileName = "parrafo", menuName = "Conversacion/parrafo")]
[System.Serializable]
public class SO_parrafo
{
    [Header("PJ")]
    public bool isTalkingPJ;
    public GLOBAL_TYPE.anim_sp_EMOCIONES PJ_Emocion_img;//
    [Header("NPC")]
    public GLOBAL_TYPE.nombreNPC nombreNPC;
    public GLOBAL_TYPE.anim_sp_EMOCIONES NPC_Emocion_img;//
    [Header("Comun")]
    public GLOBAL_TYPE.tipo_globo sp_globo;//
    [TextArea(minLines: 3, maxLines: 6)] public List<string> texto_ESPANOL;
    [TextArea(minLines: 3, maxLines: 6)] public List<string> texto_INGLES;
}
