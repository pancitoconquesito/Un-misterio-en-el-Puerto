using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CaidaResSpawn_Colision : MonoBehaviour
{
    [SerializeField] [Tag] string m_tag;
    CaidaResSpawn_Manager m_CaidaResSpawn_Manager;
    GameObject m_position;
    //bool colision=false;

    //public bool Colision { get => colision; set => colision = value; }
    public GameObject Position { get => m_position; set => m_position = value; }
    //public Vector2 PositionVector2 { get => new Vector2(m_position.transform.position.x, m_position.transform.position.y);  }
    private void Awake()
    {
        m_CaidaResSpawn_Manager = transform.parent.gameObject.GetComponent<CaidaResSpawn_Manager>();
        m_position =transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tag))
        {
            //Debug.Log("OK - "+ PositionVector2);
            //Colision = true;
            m_CaidaResSpawn_Manager.GO_position = Position;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(m_tag))
    //    {
    //        Colision = false;
    //    }
    //}

}
