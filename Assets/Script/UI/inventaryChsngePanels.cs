using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
public class inventaryChsngePanels : MonoBehaviour
{

    private GameObject[] paneles;
    private int totalPaneles;
    private int currentPanel;

    private NewControls m_Control_Inventary;

    [Header("-- botones --")]
    [SerializeField] private GameObject botonDefaultSelected_exit_GO;

    private void Awake()
    {
        
    }

    public void activarInventario()
    {

        updatePanel();
        activado = true;

        m_Control_Inventary = new NewControls();
        m_Control_Inventary.INVENTARIO.Enable();

        m_Control_Inventary.INVENTARIO.MovePanels.started += ctx => inputMovePanel(ctx.ReadValue<float>());
    }
    public void desactivarInventario()
    {
        activado = false;
        desactivarPanel();
        currentPanel = 0;
        m_Control_Inventary.INVENTARIO.MovePanels.Disable();
        m_Control_Inventary.INVENTARIO.Disable();
    }
    private bool activado = false;
    private void Start()
    {
        totalPaneles = transform.childCount;
        paneles = new GameObject[totalPaneles];
        for (int i = 0; i < totalPaneles; i++)
        {
            paneles[i] = transform.GetChild(i).gameObject;
        }
        currentPanel = 0;
        //updatePanel();
    }
    
    private void inputMovePanel(float valor)
    {
        if (activado)
        {
            desactivarPanel();
            if (valor < 0)
            {
                currentPanel--;
            }
            else
            {
                currentPanel++;
            }
            if (currentPanel >= totalPaneles) currentPanel = 0;
            if (currentPanel < 0) currentPanel = currentPanel = totalPaneles - 1;

            updatePanel();
        }
    }
    private void updatePanel()
    {
        paneles[currentPanel].SetActive(true);
        switch (currentPanel)
        {
            case 2:
                {
                    //EventSystem.current.SetSelectedGameObject(botonDefaultSelected_exit_GO);
                    break;
                }
        }
    }
    private void desactivarPanel()
    {
        paneles[currentPanel].SetActive(false);

    }
}
