using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class StairsTop : MonoBehaviour
{
    [SerializeField] [Tag] string tag_PJ;
    [SerializeField] [Tag] Stair_DOWN m_Stair_DOWN;
    [SerializeField] [Tag] Stairs_UP m_Stairs_UP;
    //Stairs_UP m_stair;
    movementPJ m_movementPJ;
    private void Awake()
    {
        //m_stair = GetComponent<Stairs_UP>();
    }
    private void Start()
    {
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag_PJ) && m_movementPJ.CanTopStair())
        {
            if (transform.position.y > m_movementPJ.transform.position.y)
            {
                m_Stair_DOWN.FullExit();
            }
            m_movementPJ.ExitStairToTop();
            m_Stairs_UP.DesactivarEscalera();
        }
    }
}
