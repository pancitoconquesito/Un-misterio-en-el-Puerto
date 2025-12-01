using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Check_areaCollider : MonoBehaviour
{
    [SerializeField, Tag] string tag;
    Collider2D collider;
    public event Action OnColision;
    public event Action OnExitColision;
    bool onStay = false;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            OnColision?.Invoke();
            onStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            OnExitColision?.Invoke();
            onStay = false;
        }
    }
}
