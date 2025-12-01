using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Conversacion_context : MonoBehaviour
{
    [SerializeField] private GameObject ui_conversacion;
    [SerializeField] private TextMeshProUGUI m_dialogoTEXT;
    [SerializeField] private Image img_PJ;
    [SerializeField] private Image img_NPC;
    [SerializeField] private Image globo;
    [SerializeField] private Image btnNXT;
    [SerializeField] private GameObject contenedorNameLeft;
    [SerializeField] private TextMeshProUGUI nameLeft;
    [SerializeField] private GameObject contenedorNameRight;
    [SerializeField] private TextMeshProUGUI nameRight;
    [SerializeField] private Animator m_anim_btn_ui;
    [SerializeField] private Ui_Anim m_uiAnim;

    public GameObject Ui_conversacion { get => ui_conversacion; set => ui_conversacion = value; }
    public TextMeshProUGUI DialogoTEXT { get => m_dialogoTEXT; set => m_dialogoTEXT = value; }
    public Image Img_PJ { get => img_PJ; set => img_PJ = value; }
    public Image Img_NPC { get => img_NPC; set => img_NPC = value; }
    public Image Globo { get => globo; set => globo = value; }
    public Image BtnNXT { get => btnNXT; set => btnNXT = value; }
    public TextMeshProUGUI NameLeft { get => nameLeft; set => nameLeft = value; }
    public TextMeshProUGUI NameRight { get => nameRight; set => nameRight = value; }
    public GameObject ContenedorNameLeft { get => contenedorNameLeft; set => contenedorNameLeft = value; }
    public GameObject ContenedorNameRight { get => contenedorNameRight; set => contenedorNameRight = value; }
    public Animator Anim_btn_ui { get => m_anim_btn_ui; set => m_anim_btn_ui = value; }
    public Ui_Anim UiAnim { get => m_uiAnim; set => m_uiAnim = value; }
}
