using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MoveMapa : MonoBehaviour
{
    [System.Serializable]
    public struct LimitesMoveMapa
    {
        public float TOP;
        public float Left;
        public float Right;
        public float Down;
    }
    [SerializeField] LimitesMoveMapa m_LimitesMoveMapa;
    [SerializeField] RectTransform m_rectTransform_CONTENT_SCALE;
    [SerializeField] RectTransform m_rectTransform;
    [SerializeField] float speed = 300f;
    [SerializeField] float factor_zoom = 1;
    [SerializeField] Vector2 m_limitScale_min;
    [SerializeField] Vector2 m_limitScale_max;
    Interpolacion_alpha_image m_alpahaSprite;
    float m_ZoomValue = 0;
    Vector2 AxisINput_Left;
    NewControls m_control;
    bool habilitado=false;
    public Interpolacion_alpha_image AlpahaSprite { get => m_alpahaSprite; set => m_alpahaSprite = value; }
    enum ZoomState
    {
        none, zoomIn, zoomOut
    }
    ZoomState m_stateEnum = ZoomState.none;
    ZoomState m_Last_stateEnum = ZoomState.none;

    void Awake()
    {
        m_alpahaSprite = GetComponent<Interpolacion_alpha_image>();
        Transform padre = m_rectTransform.transform;
        for (int i = 0; i < padre.childCount; i++)
        {
            padre.GetChild(i).gameObject.SetActive(false);
        }

        float width = m_rectTransform.rect.width;
        float height = m_rectTransform.rect.height;
        m_LimitesMoveMapa.TOP = height / 2 * 0.9f;
        m_LimitesMoveMapa.Down = -m_LimitesMoveMapa.TOP;
    }

    internal void StartMapa()
    {
        Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_MAPA_enter);
        SetPosition();
        SetControls(true);
        habilitado = true;
    }
    internal void EndMapa()
    {
        Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_MAPA_exit);
        habilitado = false;
        SetControls(false);
    }
    void SetControls(bool value)
    {
        if (value)
        {
            m_control = new NewControls();
            m_control.MAPA.Enable();
            m_control.MAPA.Movement_Axis_LEFT.performed += ctx => MoveMapa_Input(ctx.ReadValue<Vector2>());
            m_control.MAPA.Movement_Axis_LEFT.canceled += ctx => MoveMapa_Input(Vector2.zero);

            m_control.MAPA.Zoom.performed += ctx => Zoom(ctx.ReadValue<float>(), true);
            m_control.MAPA.Zoom.canceled += ctx => Zoom(0, false);

        }
        else
        {
            if (m_control != null)
            {
                m_control = null;
            }
        }
    }

    private void Zoom(float value, bool enable)
    {
        if (!enable)
        {
            m_ZoomValue = 0;
            m_stateEnum = ZoomState.none;
            m_Last_stateEnum = m_stateEnum;
            return;
        }
        if (value>0.0f)
        {
            m_ZoomValue = 1;
            m_stateEnum = ZoomState.zoomIn;
            if (m_Last_stateEnum != m_stateEnum )
            {
                m_Last_stateEnum = m_stateEnum;
                Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_MAPA_ZoomIn);
            }
        }
        else
        {
            m_stateEnum = ZoomState.zoomOut;
            m_ZoomValue = -1;
            if (m_Last_stateEnum != m_stateEnum)
            {
                m_Last_stateEnum = m_stateEnum;
                Audio_FX_UI.PlaySound(Sound_FX_BANK.Sound_FX_UI.UI_MAPA_ZoomOut);
            }
        }
        
    }

    private void MoveMapa_Input(Vector2 AxisINput_Left)=>this.AxisINput_Left = AxisINput_Left;


    void Update()
    {
        if (!habilitado)
        {
            return;
        }

        //Movement
        Vector2 movement = AxisINput_Left * speed * Time.deltaTime;
        if(
            ((m_rectTransform.anchoredPosition + movement).y > m_LimitesMoveMapa.TOP)
            ||
            ((m_rectTransform.anchoredPosition + movement).y < m_LimitesMoveMapa.Down)
            )
        {
            movement.y = 0;
        }
        if(
            ((m_rectTransform.anchoredPosition + movement).x > m_LimitesMoveMapa.Left)
            ||
            ((m_rectTransform.anchoredPosition + movement).x < m_LimitesMoveMapa.Right)
            )
        {
            movement.x = 0;
        }
        //Debug.Log($"m_rectTransform.anchoredPosition: {m_rectTransform.anchoredPosition}");
        m_rectTransform.anchoredPosition += movement;


        //Size
        Vector3 scale = m_rectTransform_CONTENT_SCALE.localScale;
        //zoom in
        if (m_limitScale_min.x > scale.x && m_ZoomValue<0)
        {
            m_ZoomValue = 0;
        }
        //zoom out
        if (m_limitScale_max.x < scale.x && m_ZoomValue > 0)
        {
            m_ZoomValue = 0;
        }
        m_rectTransform_CONTENT_SCALE.localScale = scale + (Vector3.one * m_ZoomValue * factor_zoom * Time.deltaTime);
    }

    void SetPosition()
    {
        //reset size, 
        m_rectTransform_CONTENT_SCALE.localScale = Vector3.one;

        //set position
        string currScene = SceneManager.GetActiveScene().name;
        Transform padre = m_rectTransform.transform;
        RectTransform rect_src = null;
        for (int i = 0; i < padre.childCount; i++)
        {
            if (padre.GetChild(i).name == currScene)
            {
                rect_src = padre.GetChild(i).GetComponent<RectTransform>();
                break;
            }
        }
        if (rect_src == null)
        {
            Debug.LogWarning("Hijo no encontrado con nombre: " + currScene);
            return;
        }
        Canvas canvas = m_rectTransform.GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta;
        Vector2 hijoSize = rect_src.sizeDelta;
        Vector2 padreSize = m_rectTransform.sizeDelta;
        Vector2 canvasCenter = new Vector2(canvasSize.x / 2f, canvasSize.y / 2f);
        Vector3 hijoGlobalPos = rect_src.position;
        Vector3 hijoLocalEnCanvas = canvasRect.InverseTransformPoint(hijoGlobalPos);
        Vector3 offset = hijoLocalEnCanvas;
        m_rectTransform.anchoredPosition -= new Vector2(offset.x, offset.y);
        Refresh(canvas);
        for (int i = 0; i < padre.childCount; i++)
        {
            GameObject rect_obj = padre.GetChild(i).gameObject;
            rect_obj.SetActive(true);
        }
    }
    public void Refresh(Canvas myCanvas)
    {
        Canvas.ForceUpdateCanvases(); // Forzar la actualización
        LayoutRebuilder.ForceRebuildLayoutImmediate(myCanvas.transform as RectTransform);
    }
}
