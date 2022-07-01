using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventaryPanel_Idioma : MonoBehaviour
{
    [SerializeField] private SO_text_IDIOMAS[] textos;
    private void OnEnable()
    {
        GLOBAL_TYPE.IDIOMA m_idioma = DATA.instance.getIdioma();
        switch (m_idioma)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    for (int i = 0; i < textos.Length; i++) textos[i].textMesh.text = textos[i].texto_ESPANOL;
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    for (int i = 0; i < textos.Length; i++) textos[i].textMesh.text = textos[i].texto_INGLES;
                    break;
                }
        }
    }
}
