using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class posicionarPJ_conversacion : MonoBehaviour
{

    private int id_LT_a;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int dir = 1;
            if (transform.position.x > collision.gameObject.transform.position.x) dir = -1;
            LeanTween.cancel(id_LT_a);
            id_LT_a = LeanTween.move(collision.gameObject, new Vector3(collision.gameObject.transform.position.x + (dir * 0.5f), collision.gameObject.transform.position.y, collision.gameObject.transform.position.z), 0.1f).id;
        }
    }
}
