using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
public class Interpolacion_alpha_image : MonoBehaviour
{
    [SerializeField] float m_timeInterpolacion;
    [SerializeField] GameObject m_go_AllContent;
    List<Node_img_alpha> m_l_img;
    string currScene;
    private void Awake()
    {
        m_l_img = new List<Node_img_alpha>();
        currScene = SceneManager.GetActiveScene().name;
        List < Image > l_sp = GetAllImages(m_go_AllContent);
        foreach (var item in l_sp)
        {
            Node_img_alpha new_Node_img_alpha = new Node_img_alpha();
            //new_Node_img_alpha.m_idLean_asc = -1;
            new_Node_img_alpha.m_idLean_desc = -1;
            new_Node_img_alpha.m_img = item;
            //Color color = new_Node_img_alpha.m_img.color;
            //color.a = 0;
            //new_Node_img_alpha.m_img.color = color;
            string name = item.gameObject.name;
            new_Node_img_alpha.current = false;
            if (name== currScene)
            {
                new_Node_img_alpha.current = true;
            }
            m_l_img.Add(new_Node_img_alpha);
        }

        foreach (Node_img_alpha item in m_l_img)
        {
            //item.m_originalAlpha = item.m_img.color.a;
            item.m_originalAlpha = 0.4f;
            item.m_idLean_desc = -1;
            Color color = item.m_img.color;
            color.a = 0;
            item.m_img.color = color;
        }
    }

    public List<Image> GetAllImages(GameObject obj)
    {
        List<Image> spriteRenderers = new List<Image>();
        CollectImage(obj, spriteRenderers);
        return spriteRenderers;
    }
    private void CollectImage(GameObject obj, List<Image> list)
    {
        Image sr = obj.GetComponent<Image>();
        if (sr != null)
        {
            list.Add(sr);
        }
        foreach (Transform child in obj.transform)
        {
            CollectImage(child.gameObject, list);
        }
    }

    [Button("asc")]
    public void InterpolateAlpha_asc()
    {
        foreach(Node_img_alpha item in m_l_img)
        {
            if (item.m_idLean_desc != -1)
            {
                LeanTween.cancel(item.m_idLean_desc);
            }
            FadeIn(item, m_timeInterpolacion);
        }
    }

    [Button("desc")]
    public void InterpolateAlpha_desc()
    {
        foreach (Node_img_alpha item in m_l_img)
        {
            if (item.m_idLean_asc != -1)
            {
                LeanTween.cancel(item.m_idLean_asc);
            }
            FadeOut(item, m_timeInterpolacion);
        }
    }

    public void FadeOut(Node_img_alpha img, float fadeDuration)
    {
        Color startColor = img.m_img.color;
        Color endColor = startColor;
        endColor.a = 0.0f; 
        img.m_idLean_desc = LeanTween.value(gameObject, startColor.a, endColor.a, fadeDuration)
            .setOnUpdate((float alpha) =>
            {
                Color color = img.m_img.color;
                color.a = alpha;
                img.m_img.color = color;
            }).id;
    }

    public void FadeIn(Node_img_alpha img, float fadeDuration)
    {
        Color startColor = img.m_img.color;
        Color endColor = startColor;
        //endColor.a = 1.0f; 
        if (img.current)
        {
            endColor.a = 1;
        }
        else
        {
            endColor.a = img.m_originalAlpha;
        }
        img.m_idLean_asc = LeanTween.value(gameObject, startColor.a, endColor.a, fadeDuration)
            .setOnUpdate((float alpha) =>
            {
                Color color = img.m_img.color;
                color.a = alpha;
                img.m_img.color = color;
            }).id;
    }

    //private void OnDestroy()=>LeanTween.cancelAll();
}

[System.Serializable]
public class Node_img_alpha
{
    public bool current;
    public Image m_img;
    public float m_originalAlpha;
    public int m_idLean_desc;
    public int m_idLean_asc;
}
