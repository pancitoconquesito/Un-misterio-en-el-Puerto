using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Globalization;

public class conversacion : MonoBehaviour
{
    private enum estados
    {
        fuera,
        stay,
        enter
    }


    [SerializeField] private GLOBAL_TYPE.Tags_CONVERSACIONES m_Tags_CONVERSACIONES;
    [SerializeField] private bool npcVisualMira_IZ;
    [SerializeField] private Transform m_transform_yo_npc_visual;
    [SerializeField] private BoxCollider2D m_box_alejarPj;
    [SerializeField] private Animator m_animTarget;
    SO_parrafo[] PARRAFOS;
    movementPJ movePJ;
    Transform m_transformPJ;
    GameObject ui_conversacion;
    TextMeshProUGUI m_dialogoTEXT;
    Image img_PJ;
    Image img_NPC;
    Image globo;
    Image btnNXT;
    TextMeshProUGUI nameLeft;
    TextMeshProUGUI nameRight;
    GameObject contenedorNameLeft;
    GameObject contenedorNameRight;
    Animator m_anim_btn_ui;
    Ui_Anim m_uiAnim;
    NewControls m_ControlConversacion;
    Coroutine rutinaMostrarTexto;
    int largoSTEP;
    int currentSTEP;
    int largoParrafo;
    int currentParrafo;
    Vector2 scaleNPC_visual;
    GLOBAL_TYPE.IDIOMA m_idiomaActual;
    bool textComplete = false;
    float vel_ORIGINAL = 0.02f;
    bool permiteAdelantar;
    private estados m_estados;
    List<Operaciones> m_l_operaciones;
    int LT_PJ_SS = -1;
    int LT_PJ_Move = -1;
    int LT_NPC_SS = -1;
    int LT_NPC_Move = -1;
    string completeFormaterCurrText;
    Color originalColor;

    private void Awake()
    {
        //setControls();
        m_estados = estados.fuera;
    }

    private void Start()
    {
        scaleNPC_visual = m_transform_yo_npc_visual.localScale;
        GameObject PJGO = GameObject.FindGameObjectWithTag("Player").gameObject;
        movePJ = PJGO.GetComponent<movementPJ>();
        m_transformPJ = PJGO.transform;
        Conversacion_context conversacion_context = MASTER_REFERENCE.instance.Conversacion_context;
        ui_conversacion = conversacion_context.Ui_conversacion;
        m_dialogoTEXT = conversacion_context.DialogoTEXT;
        img_PJ = conversacion_context.Img_PJ;
        img_NPC = conversacion_context.Img_NPC;
        globo = conversacion_context.Globo;
        btnNXT = conversacion_context.BtnNXT;
        nameLeft = conversacion_context.NameLeft;
        nameRight = conversacion_context.NameRight;
        contenedorNameLeft = conversacion_context.ContenedorNameLeft;
        contenedorNameRight = conversacion_context.ContenedorNameRight;
        m_anim_btn_ui = conversacion_context.Anim_btn_ui;
        m_uiAnim = conversacion_context.UiAnim;
    }

    void GetParrafos()
    {
        PARRAFOS = CsvReader.MostrarFilaPorId(m_Tags_CONVERSACIONES.ToString(), CsvReader.FilesCSV.TEST, DATA.instance.getIdioma()).ToArray();
    }

    private void setControls(bool value)
    {
        if (value)
        {
            if (m_ControlConversacion == null)
            {
                m_ControlConversacion = new NewControls();
                m_ControlConversacion.Enable();

                m_ControlConversacion.CONVERSACION.Next.started += _ => nextButton();
                m_ControlConversacion.CONVERSACION.Enter.started += _ => enterConversacion();
            }
        }
        else
        {
            if (m_ControlConversacion != null)
            {
                m_ControlConversacion.CONVERSACION.Next.started -= _ => nextButton();
                m_ControlConversacion.CONVERSACION.Enter.started -= _ => enterConversacion();
                m_ControlConversacion.Disable();
                m_ControlConversacion = null;
            }
        }
    }

    private void enterConversacion()
    {
        if (m_estados == estados.stay && movePJ.PuedeConversar())
        {
            permiteAdelantar = true;
            Audio_FX_Actions.Play_Conversacion_Start();
            originalColor = m_dialogoTEXT.color;
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
    private void getIdioma()=>m_idiomaActual=DATA.instance.getIdioma();
    private void nextButton()
    {
        if (m_estados != estados.enter)
        {
            return;
        }
        if (textComplete)
        {
            //StopCoroutine(mostrarTexto());
            currentParrafo++;
            btnNXT.enabled = false;
            updateParrafo();
        }
        else
        {
            if (!permiteAdelantar)
            {
                //no permite adelantar
                return;
            }
            textComplete = true;
            if(rutinaMostrarTexto!=null)
                StopCoroutine(rutinaMostrarTexto);
            switch (m_idiomaActual)
            {
                case GLOBAL_TYPE.IDIOMA.espanol:
                    {
                        //m_dialogoTEXT.text = m_conversacion.parrafos[currentSTEP].texto_ESPANOL[currentParrafo];
                        m_dialogoTEXT.text = completeFormaterCurrText;
                        break;
                    }
                case GLOBAL_TYPE.IDIOMA.ingles:
                    {
                        //m_dialogoTEXT.text = m_conversacion.parrafos[currentSTEP].texto_INGLES[currentParrafo];
                        m_dialogoTEXT.text = completeFormaterCurrText;
                        break;
                    }
            }
            //Audio_FX_Actions.Play_Conversacion_EndParrafo();
            mostrarNTX();
        }
        Audio_FX_Actions.Play_Conversacion_Continue();
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
        //m_animTarget.SetTrigger("start");
        m_anim_btn_ui.SetTrigger("enter");
        m_animTarget.SetTrigger("exit");
        m_uiAnim.DesaparecerUI();
        movePJ.enterConversacion();
        ui_conversacion.SetActive(true);
        GetParrafos();
        largoSTEP = PARRAFOS.Length;
        currentSTEP = 0;
        switch (m_idiomaActual)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    largoParrafo = PARRAFOS[currentSTEP].texto_ESPANOL.Count;

                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    largoParrafo = PARRAFOS[currentSTEP].texto_INGLES.Count;
                    break;
                }
        }
        currentParrafo = 0;
        updateStep();
        updateParrafo();
    }

    private void updateStep()
    {
        img_PJ.sprite = Resources.Load<Sprite>("Conv_PJ/" + PARRAFOS[currentSTEP].PJ_Emocion_img);
        img_NPC.sprite = Resources.Load<Sprite>("Conv_NPC/" + PARRAFOS[currentSTEP].nombreNPC+"_"+ PARRAFOS[currentSTEP].NPC_Emocion_img);
        globo.sprite = Resources.Load<Sprite>("Conv_globo/" + PARRAFOS[currentSTEP].sp_globo);

        if (PARRAFOS[currentSTEP].isTalkingPJ)
        {
            img_PJ.color = GLOBAL_TYPE.dialogoHablado;
            img_NPC.color = GLOBAL_TYPE.dialogoSilenciado;
            contenedorNameLeft.SetActive(true);
            contenedorNameRight.SetActive(false);
            nameLeft.text = GLOBAL_TYPE.getNameNPC(GLOBAL_TYPE.nombreNPC.PJ);


            if (LT_NPC_SS != -1)
            {
                LeanTween.cancel(LT_NPC_SS);
            }
            if (LT_NPC_Move != -1)
            {
                LeanTween.cancel(LT_NPC_Move);
            }
            LT_PJ_SS = LeanTween.scale(img_PJ.rectTransform, new Vector3(1.01f, 0.99f, 1f), 0.25f)//!parametrizar velocidad
            .setEase(LeanTweenType.easeInOutSine)    // Suaviza la animación
            .setLoopPingPong()
            .id;
            LT_PJ_Move = LeanTween.moveY(img_PJ.rectTransform, img_PJ.rectTransform.anchoredPosition.y + 30f, 0.25f)
            .setEase(LeanTweenType.easeInOutSine)   
            .setLoopPingPong()
            .id;
        }
        else
        {
            img_PJ.color = GLOBAL_TYPE.dialogoSilenciado;
            img_NPC.color = GLOBAL_TYPE.dialogoHablado;
            contenedorNameLeft.SetActive(false);
            contenedorNameRight.SetActive(true);
            nameRight.text = GLOBAL_TYPE.getNameNPC(PARRAFOS[currentSTEP].nombreNPC);

            if (LT_PJ_SS != -1)
            {
                LeanTween.cancel(LT_PJ_SS);
            }
            if (LT_PJ_Move != -1)
            {
                LeanTween.cancel(LT_PJ_Move);
            }
            LT_NPC_SS = LeanTween.scale(img_NPC.rectTransform, new Vector3(1.01f, 0.99f, 1f), 0.25f)//!parametrizar velocidad
            .setEase(LeanTweenType.easeInOutSine)    // Suaviza la animación
            .setLoopPingPong()
            .id;
            LT_NPC_Move = LeanTween.moveY(img_NPC.rectTransform, img_NPC.rectTransform.anchoredPosition.y + 30f, 0.25f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong()
            .id;
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
                m_anim_btn_ui.SetTrigger("exit");
                Invoke("cerrarConversacion",0.2f);
                return;
            }
            else
            {
                updateStep();
                switch (m_idiomaActual)
                {
                    case GLOBAL_TYPE.IDIOMA.espanol:
                        {
                            largoParrafo = PARRAFOS[currentSTEP].texto_ESPANOL.Count;
                            break;
                        }
                    case GLOBAL_TYPE.IDIOMA.ingles:
                        {
                            largoParrafo = PARRAFOS[currentSTEP].texto_INGLES.Count;
                            break;
                        }
                }
                currentParrafo = 0;
            }
        }
        rutinaMostrarTexto=StartCoroutine(mostrarTexto());
    }

    IEnumerator mostrarTexto()
    {
        btnNXT.enabled = false;
        textComplete = false;
        int largoTexto =0;
        string fullText = "";
        switch (m_idiomaActual)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    largoTexto = PARRAFOS[currentSTEP].texto_ESPANOL[currentParrafo].Length;
                    fullText = PARRAFOS[currentSTEP].texto_ESPANOL[currentParrafo];
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    largoTexto = PARRAFOS[currentSTEP].texto_INGLES[currentParrafo].Length;
                    fullText = PARRAFOS[currentSTEP].texto_INGLES[currentParrafo];
                    break;
                }
        }

        //operaciones
        m_l_operaciones = new List<Operaciones>();
        char signoIni = '/';
        char signoEnd = '%';
        string pattern = signoIni + "[^+" + signoEnd + "]+" + signoEnd;
        MatchCollection matches = Regex.Matches(fullText, pattern);
        int counterMatch = 0;
        foreach (Match match in matches)
        {
            Operaciones new_Operaciones = new Operaciones();
            string completeKey = match.Value;
            new_Operaciones.completeKey = completeKey;
            new_Operaciones.key = completeKey.Split(':')[0].Replace(signoIni.ToString(), "");
            string[] parameters = completeKey.Split(':')[1].Split(';');
            parameters[parameters.Length - 1] = parameters[parameters.Length - 1].Replace(signoEnd.ToString(), "");
            new_Operaciones.l_values = new List<string>();
            foreach (var item in parameters)
            {
                new_Operaciones.l_values.Add(item);
            }
            m_l_operaciones.Add(new_Operaciones);
            fullText = fullText.Replace(match.Value, "*");// $"<{counterMatch.ToString("D2")}>");
            //largoTexto -= 1;
            counterMatch++;
        }
        List<int> indices= new List<int>();
        int counterOp_Set = 0;
        for (int i = 0; i < fullText.Length; i++)
        {
            if (fullText[i] == '*')
            {
                m_l_operaciones[counterOp_Set].m_index = i - counterOp_Set;
                Debug.Log("m_l_operaciones[counterOp_Set].m_index: "+ m_l_operaciones[counterOp_Set].m_index);
                counterOp_Set++;
            }
        }
        fullText = fullText.Replace("*", "");
        fullText += " ";
        largoTexto = fullText.Length;
        Debug.Log($"largoTexto: {largoTexto}");
        Debug.Log($"fullText: {fullText}");
        //fin-operaciones


        //color
        List<ColorChange> l_color = new List<ColorChange>();
        ColorChange new_ColorChange = new ColorChange();
        foreach (var item in m_l_operaciones)
        {
            if (item.key == "color-s")
            {
                new_ColorChange.indexIni = item.m_index;
                new_ColorChange.indexEnd = item.m_index;//
                new_ColorChange.color = item.l_values[0];
            }
            if (item.key == "color-e")
            {
                ColorChange real_new_ColorChange = new ColorChange();
                real_new_ColorChange.indexIni = new_ColorChange.indexIni;
                real_new_ColorChange.indexEnd = item.m_index;
                real_new_ColorChange.color = new_ColorChange.color;// item.l_values[0];
                l_color.Add(real_new_ColorChange);
            }
        }
        //color-end

        //size
        List<SizeChange> l_size = new List<SizeChange>();
        SizeChange new_SizeChange = new SizeChange();
        foreach (var item in m_l_operaciones)
        {
            if (item.key == "size-s")
            {
                new_SizeChange.indexIni = item.m_index;
                new_SizeChange.size = int.Parse(item.l_values[0]);
            }
            if (item.key == "size-e")
            {
                SizeChange real_new_size = new SizeChange();
                real_new_size.indexIni = new_SizeChange.indexIni;
                real_new_size.indexEnd = item.m_index;
                real_new_size.size = new_SizeChange.size;
                l_size.Add(real_new_size);
            }
        }
        //size-end


        //formtter text
        //Color
        if (l_color.Count > 0)
        {
            string curr_textoColorForamtter = "";
            for (int j = 0; j < largoTexto; j++)
            {
                bool ok = false;
                foreach (var item in l_color)
                {
                    if (item.indexIni == j)
                    {
                        ok = true;
                        curr_textoColorForamtter += $"<color=#{item.color}>";
                        curr_textoColorForamtter += fullText[j];
                    }
                    if (item.indexEnd == j)
                    {
                        ok = true;
                        curr_textoColorForamtter += fullText[j];
                        curr_textoColorForamtter += "</color>";
                    }
                }
                if (!ok)
                {
                    curr_textoColorForamtter += fullText[j];
                }
            }
            completeFormaterCurrText = curr_textoColorForamtter;
        }
        else
        {
            completeFormaterCurrText = fullText;
        }


        //Size
        string curr_textoSizeForamtter = "";
        if (l_size.Count > 0)
        {
            for (int j = 0; j < largoTexto; j++)
            {
                bool ok = false;
                foreach (var item in l_size)
                {
                    if (item.indexIni == j)
                    {
                        ok = true;
                        curr_textoSizeForamtter += $"<size={item.size}%>";
                        curr_textoSizeForamtter += fullText[j];
                    }
                    if (item.indexEnd == j)
                    {
                        ok = true;
                        curr_textoSizeForamtter += fullText[j];
                        curr_textoSizeForamtter += "</size>";
                    }
                }
                if (!ok)
                {
                    curr_textoSizeForamtter += fullText[j];
                }
            }
            completeFormaterCurrText = curr_textoSizeForamtter;
        }
        //end-formatter

        string currentTexto = "";
        if (l_size.Count > 0)
        {

            m_dialogoTEXT.text = "<alpha=#00>" + curr_textoSizeForamtter;
        }
        else
        {
            m_dialogoTEXT.text = "<alpha=#00>" + largoTexto;
        }
        int counterOperaciones=0;
        string textoConColor = "";
        string textoConSize = "";
        float curr_vel = vel_ORIGINAL;
        permiteAdelantar = true;
        for (int i = 0; i < largoTexto; i++)
        {
            #region Color
            if (l_color.Count > 0)
            {
                textoConColor = "";
                for (int j = 0; j < i; j++)
                {
                    bool ok = false;
                    foreach (var item in l_color)
                    {
                        if (item.indexIni == j)
                        {
                            ok = true;
                            //colorEncontradoEnBUcle = true;
                            textoConColor += $"<color=#{item.color}>";
                            textoConColor += fullText[j];
                        }
                        if (item.indexEnd == j)
                        {
                            ok = true;
                            textoConColor += fullText[j];
                            textoConColor += "</color>";
                        }
                    }
                    if (!ok)
                    {
                        textoConColor += fullText[j];
                    }
                }
                currentTexto = "<alpha=#FF>" + textoConColor + "<alpha=#00>" + fullText.Substring(i);
            }
            else
            {
                currentTexto = "<alpha=#FF>" + fullText.Substring(0, i) + "<alpha=#00>" + fullText.Substring(i);
            }
            #endregion

            //Size
            int offsetB = 0;
            if (l_size.Count > 0)
            {
                //Debug.Break();
                textoConSize = "";
                for (int j = 0; j < i; j++)
                {
                    bool ok = false;
                    foreach (var item in l_size)
                    {
                        if (item.indexIni == j)
                        {
                            ok = true;
                            textoConSize += $"<size={item.size}%>";// {item.size}%>";
                            textoConSize += fullText[j];
                            offsetB += 11;
                        }
                        if (item.indexEnd == j)
                        {
                            ok = true;
                            textoConSize += fullText[j];
                            textoConSize += "</size>";
                            //Debug.Break();
                        }
                    }
                    if (!ok)
                    {
                        textoConSize += fullText[j];
                    }
                }
                //string restoTextoSize = "";
                //bool isBeetweenSize = false;
                //int indexCounter = 0;
                //for (int j = 0; j < i; j++)
                //{
                //    bool curr_isBeetweenSize = false;
                //    foreach (var item in l_size)
                //    {
                //        if (j>=item.indexIni && j <= item.indexEnd)
                //        {
                //            curr_isBeetweenSize = true;
                //            break;
                //        }
                //        if (i >= item.indexIni && i <= item.indexEnd)
                //        {
                //            isBeetweenSize = true;
                //        }
                //    }
                //    if (!curr_isBeetweenSize)
                //    {
                //        indexCounter++;
                //    }
                //}



                // completeFormaterCurrText
                //int currcountr = 0;
                //string aaa = "";
                //for (int j = 0; j < i; j++)
                //{
                //    bool curr_isBeetweenSize = false;
                //    foreach (var item in l_size)
                //    {
                //        if (j >= item.indexIni && j <= item.indexEnd)
                //        {
                //            curr_isBeetweenSize = true;
                //            break;
                //        }
                //        if (i >= item.indexIni && i <= item.indexEnd)
                //        {
                //            isBeetweenSize = true;
                //        }
                //    }
                //    if (!curr_isBeetweenSize)
                //    {
                //        currcountr++;
                //    }
                //    if (currcountr < indexCounter)
                //    {
                //        //agrego textyo[currcountr]
                //        aaa += completeFormaterCurrText[j];
                //    }
                //}
                //Debug.Log("aaa: "+ aaa);
                ///aaa+="<alpha=#00>";
                //aaa += "el resto";


                //    Debug.Log($"i: {i} | indexCounter: {indexCounter}");
                //restoTextoSize = curr_textoSizeForamtter.Substring(indexCounter);
                ////restoTextoSize = curr_textoSizeForamtter.Substring(indexCounter, curr_textoSizeForamtter.Length- indexCounter-1);



                //currentTexto = textoConSize + "<alpha=#00>" + restoTextoSize;//fullText.Substring(i);
                //currentTexto = "<alpha=#FF>" + textoConSize + "<alpha=#00>" + curr_textoSizeForamtter.Substring(i);
                currentTexto = "<alpha=#FF>" + textoConSize + "<alpha=#00>" + fullText.Substring(i);
                //Debug.Log(currentTexto);
            }

            //operaciones
            if (counterOperaciones< m_l_operaciones.Count &&  m_l_operaciones[counterOperaciones].m_index==i)//  fullText[i] == '*')//es una operacion
            {
                Operaciones op= m_l_operaciones[counterOperaciones];
                //Debug.Log($"op.key: {op.key}");
                //Debug.Log($"op.l_values[0]: {op.l_values[0]}");

                switch (op.key)
                {
                    case "w":
                        {
                            yield return new WaitForSeconds(float.Parse(op.l_values[0], CultureInfo.InvariantCulture));
                            break;
                        }
                    case "sound":
                    {
                        Audio_FX_Actions.Play_Conversacion_Sonido(op.l_values[0]);// sound:sonidoConversacion_test
                        break;
                    }
                    case "vel":
                    {
                        curr_vel = float.Parse(op.l_values[0], CultureInfo.InvariantCulture);
                        break;
                    }
                    case "no_adelantar":
                    {
                        permiteAdelantar = true;
                        break;
                    }
                    case "L_leanMove"://  /L_leanMove:%
                        {

                            break;
                        }
                    case "N":
                        {


                            break;
                        }
                }
            }
            m_dialogoTEXT.text = currentTexto;
            yield return new WaitForSeconds(curr_vel);
        }
        mostrarNTX();
        textComplete = true;
    } 

    private void mostrarNTX()
    {
        btnNXT.enabled = true;
        int largoParrafoActual = PARRAFOS[currentSTEP].texto_ESPANOL.Count;
        
        //if (currentSTEP+1 < largoSTEP)
        if(currentSTEP + 1 < largoSTEP || currentParrafo+1 < largoParrafoActual)
        {
            Audio_FX_Actions.Play_Conversacion_EndParrafo();
            btnNXT.sprite = Resources.Load<Sprite>("Conv_globo/sp_nxt");
        }
        else
        {
            Audio_FX_Actions.Play_Conversacion_EndConversacion();
            btnNXT.sprite = Resources.Load<Sprite>("Conv_globo/sp_cls");
        }
    }

    private void cerrarConversacion()
    {
        if(m_estados == estados.enter)
        {
            Audio_FX_Actions.Play_Conversacion_Exit();
            if (LT_NPC_SS != -1)
            {
                LeanTween.cancel(LT_NPC_SS);
            }
            if (LT_NPC_Move != -1)
            {
                LeanTween.cancel(LT_NPC_Move);
            }
            if (LT_PJ_SS != -1)
            {
                LeanTween.cancel(LT_PJ_SS);
            }
            if (LT_PJ_Move != -1)
            {
                LeanTween.cancel(LT_PJ_Move);
            }


            m_animTarget.SetBool("enabled", true);
            m_animTarget.SetTrigger("exit");

            //ui_conversacion.SetActive(false);
            m_uiAnim.AparecerUI();
            m_box_alejarPj.enabled = false;//!
            StopAllCoroutines();
            m_estados = estados.fuera;
            setControls(false);
            movePJ.salirConversacion();//add tiempo
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(m_estados== estados.fuera && collision.CompareTag("Player") && movePJ.CanStartConversacion())
        {
            if (!m_animTarget.GetBool("enabled"))
            {
                m_animTarget.SetTrigger("start");
            }
            m_animTarget.SetBool("enabled", true);
            //m_animTarget.SetTrigger("start");
            m_estados = estados.stay;
            setControls(true);
            //Debug.Log("aaaaaaaaaaaaaaaaaa");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && m_estados == estados.stay)
        {
            m_estados = estados.fuera;
            m_animTarget.SetBool("enabled", true);
            m_animTarget.SetTrigger("exit");
            //m_ControlConversacion.Disable();
            setControls(false);
        }
    }
    //private void OnDestroy()
    //{
    //    setControls(false);
    //}
}

public class Operaciones
{
    public int m_index;
    public string completeKey;
    public string key;//funcion
    public List<string> l_values;//parameters
}
public class ColorChange
{
    public int indexIni;
    public int indexEnd;
    public string color;
}
public class SizeChange
{
    public int indexIni;
    public int indexEnd;
    public int size;
}

///w:0.1 | wait(0.1 segundos)
///color-s | color(string color)
///color-e | </color>
///size-s:300
///size-e:300