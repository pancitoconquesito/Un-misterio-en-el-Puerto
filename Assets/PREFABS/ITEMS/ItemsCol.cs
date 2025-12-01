using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemsCol : MonoBehaviour
{
    [SerializeField] DATA_ITEMS.ITEMS m_item;
    [SerializeField] SO_ITEM m_so_item;
    [SerializeField] float tiempoDespuesDeColisionar;

    Collider2D m_collider;
    ui_itemObtenido m_ui_itemObtenido;
    GameObject ui_item_GO;
    public UnityEvent alColisionar;
    public UnityEvent XTiempoDespuesDeColisionar;
    private movementPJ m_movementPJ;

    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        m_movementPJ = GameObject.FindGameObjectWithTag("Player").GetComponent<movementPJ>();
        m_ui_itemObtenido = MASTER_REFERENCE.instance.UI_Context.Ui_itemObtenido;
        ui_item_GO = m_ui_itemObtenido.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //XTiempoDespuesDeColisionar.Invoke();
            //Destroy(gameObject);
            m_collider.enabled = false;
            m_movementPJ.cogerItem();

            alColisionar.Invoke();
            DATA.instance.save_load_system.SaveItem(m_item.ToString(), true);


            Invoke("Ejecutar_anim", 0.7f+tiempoDespuesDeColisionar);


        }
    }
    private void Ejecutar_anim()
    {
        ui_item_GO.SetActive(true);
        m_ui_itemObtenido.setValues(m_so_item);
        alColisionar.Invoke();
        Invoke("ejecutar_eventoXtiempoDespuesDeCol", tiempoDespuesDeColisionar);
    }
    private void ejecutar_eventoXtiempoDespuesDeCol()
    {
        XTiempoDespuesDeColisionar.Invoke();
    }
}
