using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivoteCameraController : MonoBehaviour
{
    private Vector2 initialPosition;
    private void Awake()
    {
        initialPosition = transform.localPosition;
    }
    public void returnInitialLocalPosition()
    {
        transform.localPosition =initialPosition;
    }
    public void returnInitialLocalPosition(float time)
    {
        //Debug.Log($"initial pos : {transform.localPosition} | finalPos: {initialPosition}");
        //float ditance = Vector2.Distance(transform.localPosition, initialPosition);
        //Vector2 currPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        LTDescr tweener = LeanTween.moveLocal(gameObject, initialPosition, time);
        tweener.setEase(LeanTweenType.easeOutExpo);
        //por tiempo o tiempo por diatncia, ahi se ve TODO:
        //transform.localPosition = initialPosition;
    }
}
