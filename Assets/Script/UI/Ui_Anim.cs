using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Anim : MonoBehaviour
{
    [SerializeField] private Animator m_animatorDolor;
    [SerializeField] private Animator m_animatorUIManager;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_animatorDolor.speed = Time.unscaledDeltaTime / Time.deltaTime;
    }
    public void StartDolor()
    {
        Debug.Log("startDolor");
        m_animatorDolor.Play("startDolor");
    }

    public void DesaparecerUI()
    {
        m_animatorUIManager.SetTrigger("exit");
    }

    internal void AparecerUI()
    {
        m_animatorUIManager.SetTrigger("enter");
    }

    internal void DesaparecerUI_Bloquear()
    {
        m_animatorUIManager.SetBool("bloquear", true);
        m_animatorUIManager.SetTrigger("exit");
    }
}
