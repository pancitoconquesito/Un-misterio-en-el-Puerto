using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class ui_corazon : MonoBehaviour
{
    [SerializeField] GameObject go_ContentBack;
    [SerializeField] GameObject go_ContentCorazones;

    [SerializeField] Sprite sp_contentBack;
    [SerializeField] Sprite sp_contentFinal;
    [SerializeField] Sprite sp_corazon;


    [SerializeField] private RectTransform m_RectTransformCorazones;//!


    [ShowNonSerializedField] private int cantidadCorazones;
    [ShowNonSerializedField] private int MAXcantidadCorazones;
    private float tamanio;
    private void Awake()
    {
        tamanio = m_RectTransformCorazones.sizeDelta.x;

    }
    void Start()
    {
        UpdateFromDATA();
    }

    public void UpdateFromDATA()
    {
        cantidadCorazones = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().VidaPj;
        MAXcantidadCorazones = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().VidaMAXIMA_pj;
        if (cantidadCorazones == 0)
        {
            Debug.Log("vida == 0");//!
            //cantidadCorazones = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().vidaMAXIMA_pj;
            cantidadCorazones = DATA.instance.save_load_system.DataGame.DATA_PROGRESS.CantidadDeCorazonesTotales;
        }

        //set back
        //remove
        foreach (Transform child in go_ContentBack.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < MAXcantidadCorazones; i++)
        {
            GameObject newImageObject = new GameObject("contentBakc_" + i);
            newImageObject.transform.SetParent(go_ContentBack.transform);
            Image imageComponent = newImageObject.AddComponent<Image>();
            imageComponent.sprite = sp_contentBack;
            RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(62, 75);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
        }
        GameObject finalImageObject = new GameObject("finalContent");
        finalImageObject.transform.SetParent(go_ContentBack.transform);
        Image imageComponent_final = finalImageObject.AddComponent<Image>();
        imageComponent_final.sprite = sp_contentFinal;
        RectTransform rectTransform_final = finalImageObject.GetComponent<RectTransform>();
        rectTransform_final.sizeDelta = new Vector2(50, 76.6f);
        rectTransform_final.localPosition = Vector3.zero;
        rectTransform_final.localScale = Vector3.one;

        updateVida_UI(cantidadCorazones);
        //m_RectTransformCorazones.sizeDelta = new Vector2(tamanio * cantidadCorazones, tamanio);
    }

    public void updateVida_UI(int cantidadCorazonesActual)
    {
        cantidadCorazones = cantidadCorazonesActual;
        //m_RectTransformCorazones.sizeDelta = new Vector2(tamanio * cantidadCorazones, tamanio);
        foreach (Transform child in go_ContentCorazones.transform)
        {
            Destroy(child.gameObject);
        }
        //agregar content
        for (int i = 0; i < cantidadCorazones; i++)
        {
            GameObject newImageObject = new GameObject("Caronzon_"+i);
            newImageObject.transform.SetParent(go_ContentCorazones.transform);
            Image imageComponent = newImageObject.AddComponent<Image>();
            imageComponent.sprite = sp_corazon;
            RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(62, 62);  
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
        }
    }



}
