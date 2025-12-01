using System.Collections;
using UnityEngine;
using NaughtyAttributes;
public class FlashSprite : MonoBehaviour
{
    public enum NAME_MATERIAL
    {
        Flash_m
    }
    [SerializeField] private NAME_MATERIAL m_NAME_MATERIAL;
    private string m_materialName;

    [SerializeField] private int m_countFlash;
    [SerializeField] private float m_timeChangeTo_flash;
    [SerializeField] private float m_timeChangeTo_normal;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    //[Header("-- Audio --")]
    //[SerializeField] private AudioClip m_AudioClip;
    //private AudioSource m_AudioSource;

    private Material m_MaterialFlash;
    private Material m_MaterialNormal;
    void Start()
    {
        //m_AudioSource = GetComponent<AudioSource>();
        //if (m_AudioSource == null)
        //    m_AudioSource = gameObject.AddComponent<AudioSource>();// AddComponent("AudioSource") as AudioSource;
        //m_AudioClip= Resources.Load<AudioClip>("Audio/Flash");
        //m_AudioSource.volume = DATA.instance.Nivel_Audio_FX* DATA.instance.Nivel_Audio_MASTER;
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_MaterialNormal = m_SpriteRenderer.material;
        m_materialName = getString_NameMaterial();
        m_MaterialFlash = Resources.Load<Material>("Material/"+ m_materialName);
    }
    private Coroutine miCorutinaFlash;
    [Button("Flashear()")]
    public void Flashear()
    {
        if (flasheando) StopCoroutine(miCorutinaFlash);
        miCorutinaFlash=StartCoroutine(changeFlash());
    }
    private bool flasheando = false;
    IEnumerator changeFlash()
    {
        //if(m_AudioSource!=null && m_AudioClip!=null)
        //    m_AudioSource.PlayOneShot(m_AudioClip);
        flasheando = true;
        int currentCount = m_countFlash;
        while (currentCount>=0)
        {
            m_SpriteRenderer.material = m_MaterialFlash;

            yield return new WaitForSeconds(m_timeChangeTo_normal);
            m_SpriteRenderer.material = m_MaterialNormal;
            yield return new WaitForSeconds(m_timeChangeTo_flash);

            currentCount--;
        }
        flasheando = false;
    }

    private string getString_NameMaterial()
    {
        string retorno = "";
        switch (m_NAME_MATERIAL)
        {
            case NAME_MATERIAL.Flash_m:
                {
                    retorno = "Flash_m";
                    break;
                }
            default:
                {
                    retorno = "Flash_m";
                    break;
                }
        }
        return retorno;
    }
}
