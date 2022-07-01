using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemsCol : MonoBehaviour
{
    //[SerializeField]private 
    [SerializeField] private ui_itemObtenido m_ui_itemObtenido;
    [SerializeField] private SO_ITEM m_so_item;
    [SerializeField] private GameObject ui_item_GO;
    [SerializeField] private float tiempoDespuesDeColisionar;
    public UnityEvent alColisionar;
    public UnityEvent XTiempoDespuesDeColisionar;
    private movementPJ m_movementPJ;
    void Start()
    {
        m_movementPJ=GameObject.FindGameObjectWithTag("Player").GetComponent<movementPJ>();

        if (DATA.instance.save_load_system.isItemObtenido(m_so_item.ID_ITEM))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("ejecutar_eventoXtiempoDespuesDeCol",tiempoDespuesDeColisionar);
            XTiempoDespuesDeColisionar.Invoke();
            Destroy(gameObject);
            m_movementPJ.cogerItem();
            ui_item_GO.SetActive(true);
            m_ui_itemObtenido.setValues(m_so_item);
            alColisionar.Invoke();
        }
    }
    private void ejecutar_eventoXtiempoDespuesDeCol()
    {
        XTiempoDespuesDeColisionar.Invoke();
    }
}
