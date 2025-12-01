using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_alert : MonoBehaviour
{
    RaycastCheck_GroupEvent RaycastCheck_GroupEvent;

    private void Start()
    {
        RaycastCheck_GroupEvent = this.transform.GetComponent<RaycastCheck_GroupEvent>();
        RaycastCheck_GroupEvent.OnColision += OnColision;
    }

    void OnColision()
    {
        Debug.Log("OnColision-Raycast FOV!");
    }
}
