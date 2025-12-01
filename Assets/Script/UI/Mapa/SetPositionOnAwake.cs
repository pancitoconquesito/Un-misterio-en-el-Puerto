using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionOnAwake : MonoBehaviour
{
    [SerializeField] RectTransform m_rectransform;
    [SerializeField] Vector2 m_position;

    private void Awake()
    {
        m_rectransform.anchoredPosition = m_position;
    }


}
