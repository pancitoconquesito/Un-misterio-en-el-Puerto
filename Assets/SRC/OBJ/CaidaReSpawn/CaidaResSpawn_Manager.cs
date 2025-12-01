using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaResSpawn_Manager : MonoBehaviour
{
    GameObject m_position;

    public GameObject GO_position { get => m_position; set => m_position = value; }

    public Vector2 GetNewPosition()
    {
        return new Vector2(GO_position.transform.position.x, GO_position.transform.position.y);
    }
}
