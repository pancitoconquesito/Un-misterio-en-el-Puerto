using UnityEngine;
using TMPro;
public class UI_TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject UI_tituloZona_GO;
    [SerializeField] private TextMeshProUGUI textoTitulo;
    [SerializeField] private Animator animatorUI_Titulo;
    [TextArea(minLines: 2, maxLines: 4)] [SerializeField] private string textoLugar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UI_tituloZona_GO.SetActive(true);
            textoTitulo.text = textoLugar;
            animatorUI_Titulo.SetTrigger("Show");
        }
    }
}
