using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sh_set_movePasto : MonoBehaviour
{
    public float windStrength = 0.05f;
    public float windSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        float dynamicSpeed = windSpeed; // o usar Mathf.Sin(Time.time) * factor
        //float dynamicSpeed = Mathf.Sin(Time.time)*windSpeed*Time.deltaTime; // o usar Mathf.Sin(Time.time) * factor
        spriteRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_WindStrength", windStrength);
        propBlock.SetFloat("_WindSpeed", dynamicSpeed);
        spriteRenderer.SetPropertyBlock(propBlock);
    }
}
