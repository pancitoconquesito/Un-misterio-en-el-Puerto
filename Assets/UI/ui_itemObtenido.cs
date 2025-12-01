using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ui_itemObtenido : MonoBehaviour
{
    [SerializeField] private Image imagenItem;
    [SerializeField] private TextMeshProUGUI nombre;
    [SerializeField] private TextMeshProUGUI descripcion;
    private movementPJ m_movementPJ;
    
    private void Start()
    {
        m_movementPJ=GameObject.FindGameObjectWithTag("Player").GetComponent<movementPJ>();
    }
    public void setValues(SO_ITEM item)
    {
        switch (DATA.instance.getIdioma())
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    imagenItem.sprite = item.infoEspanol.sp_imagen;
                    nombre.text = item.infoEspanol.nombre;
                    descripcion.text = item.infoEspanol.descripcion;

                    break;
                }
        }


        Invoke("sacarPantalla",4);
    }
    public void sacarPantalla()
    {
        m_movementPJ.activarMovimiento();
        m_movementPJ.ExitTernminarItem();
        this.gameObject.SetActive(false);
    }
}
