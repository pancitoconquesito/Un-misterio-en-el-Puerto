using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColisionTag : MonoBehaviour
{
    [SerializeField] private string tagName;
    int colisionWithTag=0;

    public bool ColisionWithTag { get => colisionWithTag<=0; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            colisionWithTag++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            colisionWithTag--;
        }
    }
}
