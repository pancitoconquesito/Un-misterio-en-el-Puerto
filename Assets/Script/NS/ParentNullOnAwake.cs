using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentNullOnAwake : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }
}
