using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLeanTween : MonoBehaviour
{
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();

    }
    // Update is called once per frame
    void Update()
    {


        //LeanTween.easeInOutSine(rect.rect.y, rect.rect.y + 10, 0.5f)
        //LeanTween.easeInOutSine (LeanTweenType.easeOutBounce);

        //LeanTween.mov(gameObject, 0.0f, 1.0f).setEase(LeanTweenType.easeOutElastic).setOvershoot(0.3f);
    }

}
