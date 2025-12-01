using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText_idioma : MonoBehaviour
{
    [SerializeField] List<SO_text_IDIOMAS> l_textos;
    void Awake()
    {
        DATA.OnSetIdioma += SetIdioma;
    }
    void SetIdioma(GLOBAL_TYPE.IDIOMA idioma)
    {
        switch (idioma)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    foreach (var item in l_textos)
                    {
                        item.textMesh.text = item.texto_ESPANOL;
                    }
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    foreach (var item in l_textos)
                    {
                        item.textMesh.text = item.texto_INGLES;
                    }
                    break;
                }
        }

        
    }

}
