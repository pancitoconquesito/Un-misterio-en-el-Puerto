using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASTER_REFERENCE : MonoBehaviour
{
    public static MASTER_REFERENCE instance;
    
    [Header("PJ")]
    [SerializeField] GameObject m_GO_PJ;
    [SerializeField] movementPJ m_movementPJ;
    [SerializeField] vida_PJ m_vidaPJ;
    [SerializeField] PowerManager m_PowerManager;
    [SerializeField] changeMirada m_changeMirada;

    [Header("PJ-sombra")]
    [SerializeField] SpriteRenderer sombra_ini;
    [SerializeField] SpriteRenderer sombra_midd;
    [SerializeField] SpriteRenderer sombra_lejos;

    [Header("UI")]
    [SerializeField] Ui_power m_Ui_power;
    [SerializeField] Ui_Anim m_Ui_Anim;
    [SerializeField] UI_Context m_UI_Context;
    [SerializeField] Conversacion_context m_Conversacion_context;

    [Header("Others")]
    [SerializeField] Poder_Teletransportacion m_Poder_Teletransportacion;
    [SerializeField] CameraController m_CameraController;
    [SerializeField] TimeManager m_TimeManager;

    [Header("Audio")]
    [SerializeField] AudioManagerContext m_AudioManagerContext;
    public movementPJ MovementPJ { get => m_movementPJ; set => m_movementPJ = value; }
    public Ui_power Ui_power { get => m_Ui_power; set => m_Ui_power = value; }
    public Poder_Teletransportacion Poder_Teletransportacion { get => m_Poder_Teletransportacion; set => m_Poder_Teletransportacion = value; }
    public CameraController CameraController { get => m_CameraController; set => m_CameraController = value; }
    public TimeManager TimeManager { get => m_TimeManager; set => m_TimeManager = value; }
    public Ui_Anim Ui_Anim { get => m_Ui_Anim; set => m_Ui_Anim = value; }
    public Conversacion_context Conversacion_context { get => m_Conversacion_context; set => m_Conversacion_context = value; }
    public UI_Context UI_Context { get => m_UI_Context; set => m_UI_Context = value; }
    public vida_PJ VidaPJ { get => m_vidaPJ; set => m_vidaPJ = value; }
    public PowerManager PowerManager { get => m_PowerManager; set => m_PowerManager = value; }
    public GameObject GO_PJ { get => m_GO_PJ; set => m_GO_PJ = value; }
    public AudioManagerContext AudioManagerContext { get => m_AudioManagerContext; set => m_AudioManagerContext = value; }
    public changeMirada ChangeMirada { get => m_changeMirada; set => m_changeMirada = value; }
    public SpriteRenderer Sombra_ini { get => sombra_ini; set => sombra_ini = value; }
    public SpriteRenderer Sombra_midd { get => sombra_midd; set => sombra_midd = value; }
    public SpriteRenderer Sombra_lejos { get => sombra_lejos; set => sombra_lejos = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        AudioManagerContext = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioManagerContext>();
    }

    public List<string> m_list_values;
    public void AddValueGUI(string value)
    {
        m_list_values.Add(value);
    }

    public void chageValue(string key, string newValue)
    {
        foreach (var item in m_list_values)
        {
            //string currKey = item.Split(':').
        }
    }
    //void OnGUI()
    //{
    //    GUIStyle estilo = new GUIStyle();
    //    estilo.fontSize = 24;  // Tamaño de la fuente
    //    estilo.normal.textColor = Color.black;  // Color del texto
    //    foreach (var item in m_list_values)
    //    {
    //        GUI.Label(new Rect(10, 10, 200, 40), item, estilo);
    //    }
    //}

}
