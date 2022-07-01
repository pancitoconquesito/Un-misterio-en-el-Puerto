using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class conversacion : MonoBehaviour
{
    [SerializeField] private movementPJ movePJ;
    [SerializeField] private Transform m_transformPJ;
    [SerializeField] private GameObject ui_conversacion;
    [SerializeField] private TextMeshProUGUI m_dialogoTEXT;
    [SerializeField] private SO_conversacion m_conversacion;
    [SerializeField] private Image img_PJ;
    [SerializeField] private Image img_NPC;
    [SerializeField] private Image globo;
    [SerializeField] private Image btnNXT;
    [SerializeField] private TextMeshProUGUI nameLeft;
    [SerializeField] private TextMeshProUGUI nameRight;
    [SerializeField] private GameObject contenedorNameLeft;
    [SerializeField] private GameObject contenedorNameRight;
    [SerializeField]private BoxCollider2D m_box_alejarPj;
    [SerializeField] private Transform m_transform_yo_npc_visual;
    [SerializeField] private bool npcVisualMira_IZ;
    [SerializeField] private Animator m_anim_btn_ui;

    private NewControls m_ControlConversacion;
    private Coroutine rutinaMostrarTexto;

    //private Sprite sp_next;
    //private Sprite sp_close;
    private Vector2 scaleNPC_visual;
    private void Awake()
    {
        //setControls();
        m_estados = estados.fuera;
    }
    private void Start()
    {
        scaleNPC_visual = m_transform_yo_npc_visual.localScale;
    }
    private void setControls()
    {
        m_ControlConversacion = new NewControls();
        m_ControlConversacion.Enable();

        m_ControlConversacion.CONVERSACION.Next.started += _ => nextButton();
        m_ControlConversacion.CONVERSACION.Enter.started += _ => enterConversacion();
    }

    private GLOBAL_TYPE.IDIOMA m_idiomaActual;

    private void enterConversacion()
    {
        if (m_estados == estados.stay && movePJ.puedeConversar())
        {
            m_box_alejarPj.enabled=true;
            cambiarMirada();
            getIdioma();
            StopAllCoroutines();
            m_estados = estados.enter;
            comenzarConversacion();
        }
    }
    private void cambiarMirada()
    {
        GLOBAL_TYPE.LADO ladoMiradaPj = GLOBAL_TYPE.LADO.iz;
        if (m_transformPJ.position.x < transform.position.x)//pj a la derecha de npc, pj mira a la derecha
        {
            ladoMiradaPj = GLOBAL_TYPE.LADO.der;
            if (npcVisualMira_IZ) m_transform_yo_npc_visual.localScale = new Vector2(scaleNPC_visual.x, scaleNPC_visual.y);
            else m_transform_yo_npc_visual.localScale = new Vector2(-scaleNPC_visual.x, scaleNPC_visual.y);
        }
        else {
            ladoMiradaPj = GLOBAL_TYPE.LADO.iz;
            if (npcVisualMira_IZ) m_transform_yo_npc_visual.localScale = new Vector2(-scaleNPC_visual.x, scaleNPC_visual.y);
            else m_transform_yo_npc_visual.localScale = new Vector2(scaleNPC_visual.x, scaleNPC_visual.y);
        } 

        movePJ.setCambioMirada(ladoMiradaPj);
    }
    private void getIdioma()
    {
        m_idiomaActual=DATA.instance.getIdioma();
        print("Conversacion en idioma :"+m_idiomaActual);
    }
    private void nextButton()
    {
        if (textComplete)
        {
            //StopCoroutine(mostrarTexto());
            currentParrafo++;
            btnNXT.enabled = false;
            updateParrafo();
        }
        else
        {
            textComplete = true;
            if(rutinaMostrarTexto!=null)
                StopCoroutine(rutinaMostrarTexto);
            switch (m_idiomaActual)
            {
                case GLOBAL_TYPE.IDIOMA.espanol:
                    {
                        m_dialogoTEXT.text = m_conversacion.parrafos[currentSTEP].texto_ESPANOL[currentParrafo];
                        break;
                    }
                case GLOBAL_TYPE.IDIOMA.ingles:
                    {
                        m_dialogoTEXT.text = m_conversacion.parrafos[currentSTEP].texto_INGLES[currentParrafo];
                        break;
                    }
            }
            mostrarNTX();
        }
    }
    private void OnEnable()
    {
        //setControls();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        if(m_ControlConversacion!=null)
            m_ControlConversacion.Disable();
    }
    public void comenzarConversacion()
    {
        movePJ.enterConversacion();
        ui_conversacion.SetActive(true);
        largoSTEP = m_conversacion.parrafos.Length;
        currentSTEP = 0;
        switch (m_idiomaActual)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    largoParrafo = m_conversacion.parrafos[currentSTEP].texto_ESPANOL.Length;

                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    largoParrafo = m_conversacion.parrafos[currentSTEP].texto_INGLES.Length;
                    break;
                }
        }
        currentParrafo = 0;

        updateStep();
        updateParrafo();
    }
    private int largoSTEP;
    private int currentSTEP;

    private int largoParrafo;
    private int currentParrafo;
    private void updateStep()
    {

        img_PJ.sprite = Resources.Load<Sprite>("Conv_PJ/" + m_conversacion.parrafos[currentSTEP].PJ_img);
        img_NPC.sprite = Resources.Load<Sprite>("Conv_NPC/" + m_conversacion.parrafos[currentSTEP].NPC_img);
        globo.sprite = Resources.Load<Sprite>("Conv_globo/" + m_conversacion.parrafos[currentSTEP].sp_globo);

        if (m_conversacion.parrafos[currentSTEP].isTalkingPJ)
        {
            img_PJ.color = GLOBAL_TYPE.dialogoHablado;
            img_NPC.color = GLOBAL_TYPE.dialogoSilenciado;
            contenedorNameLeft.SetActive(true);
            contenedorNameRight.SetActive(false);
            nameLeft.text = GLOBAL_TYPE.getNameNPC(m_conversacion.parrafos[currentSTEP].nombreNPC);


            
        }
        else
        {
            img_PJ.color = GLOBAL_TYPE.dialogoSilenciado;
            img_NPC.color = GLOBAL_TYPE.dialogoHablado;
            contenedorNameLeft.SetActive(false);
            contenedorNameRight.SetActive(true);
            nameRight.text = GLOBAL_TYPE.getNameNPC(m_conversacion.parrafos[currentSTEP].nombreNPC);
        }

        
    }
    private void updateParrafo()
    {
        if (currentParrafo >= largoParrafo)
        {
            currentSTEP++;
            if (currentSTEP >= largoSTEP)
            {
                //print("- fin conversacion -");
                StopAllCoroutines();
                m_ControlConversacion.Disable();
                ui_conversacion.SetActive(false);
                Invoke("cerrarConversacion",0.5f);
                return;
            }
            else
            {
                updateStep();
                switch (m_idiomaActual)
                {
                    case GLOBAL_TYPE.IDIOMA.espanol:
                        {
                            largoParrafo = m_conversacion.parrafos[currentSTEP].texto_ESPANOL.Length;
                            break;
                        }
                    case GLOBAL_TYPE.IDIOMA.ingles:
                        {
                            largoParrafo = m_conversacion.parrafos[currentSTEP].texto_INGLES.Length;
                            break;
                        }
                }
                currentParrafo = 0;
            }
        }
        rutinaMostrarTexto=StartCoroutine(mostrarTexto());
    }
    private bool textComplete=false;
    IEnumerator mostrarTexto()
    {
        btnNXT.enabled = false;
        textComplete = false;
        int largoTexto =0;
        switch (m_idiomaActual)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    largoTexto = m_conversacion.parrafos[currentSTEP].texto_ESPANOL[currentParrafo].Length;
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    largoTexto = m_conversacion.parrafos[currentSTEP].texto_INGLES[currentParrafo].Length;
                    break;
                }
        }
        string currentTexto = "";
        for (int i = 0; i < largoTexto; i++)
        {
            switch (m_idiomaActual)
            {
                case GLOBAL_TYPE.IDIOMA.espanol:
                    {
                        currentTexto += m_conversacion.parrafos[currentSTEP].texto_ESPANOL[currentParrafo][i];
                        break;
                    }
                case GLOBAL_TYPE.IDIOMA.ingles:
                    {
                        currentTexto += m_conversacion.parrafos[currentSTEP].texto_INGLES[currentParrafo][i];
                        break;
                    }
            }
            m_dialogoTEXT.text = currentTexto;
            yield return new WaitForSeconds(0.02f);
        }
        mostrarNTX();
        textComplete = true;
    } 
    private void mostrarNTX()
    {
        btnNXT.enabled = true;
        int largoParrafoActual = m_conversacion.parrafos[currentSTEP].texto_ESPANOL.Length;
        
        //if (currentSTEP+1 < largoSTEP)
        if(currentSTEP + 1 < largoSTEP || currentParrafo+1 < largoParrafoActual)
            btnNXT.sprite = Resources.Load<Sprite>("Conv_globo/sp_nxt");
        else
            btnNXT.sprite = Resources.Load<Sprite>("Conv_globo/sp_cls");
    }
    private void cerrarConversacion()
    {
        m_box_alejarPj.enabled = false;
        StopAllCoroutines();
        movePJ.salirConversacion();//add tiempo
        m_estados = estados.fuera;
    }

    private enum estados
    {
        fuera,
        stay,
        enter
    }
    private estados m_estados;
    //private bool enter = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(m_estados== estados.fuera && collision.CompareTag("Player"))
        {
            m_anim_btn_ui.SetTrigger("enter");
            m_estados = estados.stay;
            setControls();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_anim_btn_ui.SetTrigger("exit");
            m_estados = estados.fuera;
            m_ControlConversacion.Disable();
        }
    }
   
}
