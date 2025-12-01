using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bool_checkRayscastWithEvent : MonoBehaviour
{
    CheckerRayCast m_CheckerRayCast;
    private void Awake()
    {
        m_CheckerRayCast = GetComponent<CheckerRayCast>();
        m_CheckerRayCast.OnHitEnter += OnHitEnter;
        m_CheckerRayCast.OnHitExit += OnHitExit;
    }
    bool curr_bool;
    GameObject curr_obj;

    public bool Curr_bool { get => curr_bool; set => curr_bool = value; }
    public GameObject Curr_obj { get => curr_obj; set => curr_obj = value; }

    void OnHitEnter(GameObject obj)
    {
        curr_bool = true;
        curr_obj = obj;
    }
    void OnHitExit(GameObject obj)
    {
        curr_bool = false;
    }
    
}
