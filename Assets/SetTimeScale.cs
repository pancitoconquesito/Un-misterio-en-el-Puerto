using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField] float timeScale;
    private void Awake()
    {
        Time.timeScale = timeScale;
    }

}
