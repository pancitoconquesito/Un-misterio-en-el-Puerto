using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CreateAssetMenu(fileName = "parrafo", menuName = "Conversacion/parrafo")]
[System.Serializable]
public class SO_parrafo
{
    public bool isTalkingPJ;
    public GLOBAL_TYPE.nombreNPC nombreNPC;
    public GLOBAL_TYPE.anim_sp_PJ PJ_img;//
    public GLOBAL_TYPE.anim_sp_NPC NPC_img;//
    public GLOBAL_TYPE.tipo_globo sp_globo;//
    [TextArea(minLines: 3, maxLines: 6)] public string[] texto_ESPANOL;
    [TextArea(minLines: 3, maxLines: 6)] public string[] texto_INGLES;
}
