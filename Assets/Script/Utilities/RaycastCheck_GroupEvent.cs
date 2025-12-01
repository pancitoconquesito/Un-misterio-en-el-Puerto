using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck_GroupEvent : MonoBehaviour
{
    [SerializeField] Check_areaCollider m_Check_areaCollider;
    [SerializeField] GameObject go_l_raycast;
    List<RaycastCheck> l_RaycastCheck;
    public event Action OnColision;
    private void Awake()
    {
        if (go_l_raycast != null)
        {
            RaycastCheck[] arr_RaycastCheck = go_l_raycast.transform.gameObject.GetComponents<RaycastCheck>();
            l_RaycastCheck = new List<RaycastCheck>(arr_RaycastCheck);
            foreach (var item in l_RaycastCheck)
            {
                item.OnColision += OnColisionMethod;
            }
        }

        if (m_Check_areaCollider != null)
        {
            m_Check_areaCollider.OnColision += OnColisionMethod;
        }
    }

    void OnColisionMethod()
    {
        OnColision?.Invoke();
    }

}
