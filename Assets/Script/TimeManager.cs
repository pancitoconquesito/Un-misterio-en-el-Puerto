using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TimeManager : MonoBehaviour
{

    [ShowNonSerializedField] private float m_curr_Time;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_curr_Time = Time.timeScale;
    }
    float old_timeReturn;
    public void SetTime(float newTime, float timeReturn)
    {
        Time.timeScale = newTime;
        LeanTween.delayedCall(gameObject, 0.3f, () =>
        {
            LeanTween.value(gameObject, newTime, 1, timeReturn)
                .setOnUpdate((float val) =>
                {
                    Time.timeScale = val;
                })
                .setIgnoreTimeScale(true); 
        })
        .setIgnoreTimeScale(true); 

    }



}
