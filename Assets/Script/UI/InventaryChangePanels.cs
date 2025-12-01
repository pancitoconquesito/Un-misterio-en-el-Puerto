using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventaryChangePanels : MonoBehaviour
{
    [SerializeField] private GameObject m_contenedoresPaneles;
    private GameObject[] paneles;
    private int totalPaneles;
    private int currentPanel;

    private NewControls m_Control_Inventary;

    [SerializeField] private GameObject botonDefaultSelected_exit_GO;
    //[SerializeField] private EventSystem m_ES;
    [Header("-- PROGRESS --")]
    [SerializeField] private Canvas m_canvas_PROGRESS;
    [SerializeField] private GameObject First_GO_PROGRESS;
    [SerializeField] private GameObject m_GO_SELECTOR;
    [SerializeField] private RectTransform SELECTOR;
    [SerializeField] private TextMeshProUGUI m_titulo;
    [SerializeField] private TextMeshProUGUI m_descripcion;


    [Header("--Coleccion--")]
    [SerializeField] private GameObject First_GO_Coleccion;

    [Header("--Salir--")]
    [SerializeField] private GameObject First_GO_Salir;


    private Animator m_anim;
    CameraController m_cameraController;
    GameObject lastSelectedObject = null;

    bool canDoMove = false;
    Ui_Anim m_Ui_Anim;
    float tiemStart = 0;
    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        EventSystem.current.SetSelectedGameObject(null);
        lastSelectedObject = null;
        canDoMove = false;
    }
    
    public void activarInventario()
    {
        Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_start);
        m_anim.SetTrigger("enter");
        updatePanel(0);
        activado = true;//estar en pausa
        canDoMove = true;//poder moverse dentro de la pausa

        //desactivar UI - gameplay
        if (m_Ui_Anim == null)
        {
            m_Ui_Anim = MASTER_REFERENCE.instance.Ui_Anim;
        }
        m_Ui_Anim.DesaparecerUI();
        if (m_cameraController == null)
        {
            m_cameraController = MASTER_REFERENCE.instance.CameraController;
        }
        m_cameraController.SetCameraCerca_PAUSE();

        m_Control_Inventary = new NewControls();
        m_Control_Inventary.INVENTARIO.Enable();

        m_Control_Inventary.INVENTARIO.MovePanels.started += ctx => inputMovePanel(ctx.ReadValue<float>());

        EventSystem.current.SetSelectedGameObject(First_GO_PROGRESS);
        lastSelectedObject = null;


        GameObject selectedObject = First_GO_PROGRESS;
        RectTransform selectedTransform = selectedObject.GetComponent<RectTransform>();
        SELECTOR.position = selectedTransform.position;
        SELECTOR.sizeDelta = selectedTransform.sizeDelta + new Vector2(10f, 10f);

    }
    void Update()
    {
        // Obtener el objeto actualmente seleccionado
        GameObject currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedObject == null)
        {
            m_GO_SELECTOR.SetActive(false);
            return;
        }
        else
        {
            m_GO_SELECTOR.SetActive(currentPanel == 0);
        }
        // Si ha cambiado el objeto seleccionado
        if (currentSelectedObject != lastSelectedObject)
        {
            if (canDoMove && tiemStart>1f)
            {
                Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_moveIn);
            }
            UpdateSelector();
            lastSelectedObject = currentSelectedObject; // Actualizar el objeto seleccionado
        }
        tiemStart += Time.deltaTime;
    }
    private void UpdateSelector()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        RectTransform selectedTransform = selectedObject.GetComponent<RectTransform>();
        SELECTOR.position = selectedTransform.position;
        SELECTOR.sizeDelta = selectedTransform.sizeDelta+new Vector2(10f,10f);

        DataDescripcion_UI dataDescripcion = selectedObject.GetComponent<DataDescripcion_UI>();
        if (dataDescripcion != null)
        {
            m_titulo.text = dataDescripcion.Titulo;
            m_descripcion.text = dataDescripcion.Descripcion;
        }

    }

    public void desactivarInventario_Si_estaActivado(bool aparecerUI = true, bool cameraNormal = true)
    {
        if (activado)
        {
            canDoMove = false;
            desactivarInventario(aparecerUI, cameraNormal);
        }
    }
    private void OnDestroy()
    {
        if (corr_ReactivarInteracion != null)
        {
            StopCoroutine(corr_ReactivarInteracion);
            corr_ReactivarInteracion = null;  // Limpiar referencia
        }
    }
    public void ANIM_QuitarInventarioSAlir()
    {
        if (corr_ReactivarInteracion != null)
        {
            StopCoroutine(corr_ReactivarInteracion);
            corr_ReactivarInteracion = null;  // Limpiar referencia
        }
        tiemStart = 0;
        m_anim.SetTrigger("exit");

        activado = false;
        //desactivarPanel();
        currentPanel = 0;
        if (m_Control_Inventary != null)
        {
            m_Control_Inventary.INVENTARIO.MovePanels.Disable();
            m_Control_Inventary.INVENTARIO.Disable();
        }
    }
    public void desactivarInventario(bool aparecerUI=true, bool cameraNormal=true)
    {
        if (corr_ReactivarInteracion != null)
        {
            StopCoroutine(corr_ReactivarInteracion);
            corr_ReactivarInteracion = null;  // Limpiar referencia
        }
        tiemStart = 0;
        m_anim.SetTrigger("exit");
        Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_exit);
        //activar UI - gameplay
        if (aparecerUI)
        {
            if (m_Ui_Anim == null)
            {
                m_Ui_Anim = MASTER_REFERENCE.instance.Ui_Anim;
            }
            m_Ui_Anim.AparecerUI();
        }
        if (cameraNormal)
        {
            if (m_cameraController == null)
            {
                m_cameraController = MASTER_REFERENCE.instance.CameraController;
            }
            m_cameraController.SetCameraGameplay_normal();
        }

        activado = false;
        //desactivarPanel();
        currentPanel = 0;
        if (m_Control_Inventary != null)
        {
            m_Control_Inventary.INVENTARIO.MovePanels.Disable();
            m_Control_Inventary.INVENTARIO.Disable();
        }
    }
    private bool activado = false;
    private void Start()
    {
        m_Ui_Anim = MASTER_REFERENCE.instance.Ui_Anim;
        m_cameraController = MASTER_REFERENCE.instance.CameraController;

        totalPaneles = m_contenedoresPaneles.transform.childCount;
        paneles = new GameObject[totalPaneles];
        for (int i = 0; i < totalPaneles; i++)
        {
            paneles[i] = m_contenedoresPaneles.transform.GetChild(i).gameObject;
        }
        currentPanel = 0;
        //updatePanel();
    }
    
    


    private void inputMovePanel(float valor)
    {
        if (activado && canDoMove)
        {
            //Debug.Log("valor: "+ valor);
            
            canDoMove = false;
            Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_changePanel);
            //desactivarPanel();
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

            updatePanel((int)valor);

            switch (currentPanel)
            {
                case 0:
                    {
                        EventSystem.current.SetSelectedGameObject(First_GO_PROGRESS);
                        break;
                    }
                case 1:
                    {
                        EventSystem.current.SetSelectedGameObject(First_GO_Coleccion);
                        break;
                    }
                case 2:
                    {
                        EventSystem.current.SetSelectedGameObject(First_GO_Salir);
                        break;
                    }
            }

            StartCoroutine(ReactivarInteracion(0.25f));
            //corr_ReactivarInteracion = Invoke("ReactivarInteracion", 0.25f);
        }
    }
    private Coroutine corr_ReactivarInteracion;
    IEnumerator ReactivarInteracion(float delay)
    {
        if (activado)
        {
            yield return new WaitForSeconds(delay);
            canDoMove = true;
        }
    }
    private void updatePanel(int direccion)
    {
        m_anim = GetComponent<Animator>();
        //print($"currentPanel: {currentPanel} | totalPaneles: {totalPaneles}");
        switch (direccion)
        {
            case -1:
                {
                    m_anim.SetTrigger("L_" + currentPanel);
                    break;
                }
            case 0:
                {
                    //nada
                    break;
                }
            case 1:
                {
                    m_anim.SetTrigger("R_"+ currentPanel);
                    break;
                }
        }

        //paneles[currentPanel].SetActive(true);
        //switch (currentPanel)
        //{
        //    case 2:
        //        {
        //            //EventSystem.current.SetSelectedGameObject(botonDefaultSelected_exit_GO);
        //            break;
        //        }
        //}
    }

    //private void desactivarPanel()
    //{
    //    //paneles[currentPanel].SetActive(false);
    //}
}
