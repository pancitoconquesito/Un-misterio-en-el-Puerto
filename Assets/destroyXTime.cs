using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyXTime : MonoBehaviour
{
    [SerializeField] private float timeDestroy;
    [SerializeField] private GameObject m_objDestroy;
    float curr_timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        curr_timeDestroy = timeDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (curr_timeDestroy > 0) curr_timeDestroy -= Time.deltaTime;
        else
        {
            Destroy(m_objDestroy);
        }
    }
}
