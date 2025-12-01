using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_setterRaycast : MonoBehaviour
{
    public enum Tipo
    {
        scaleX,
        rotationZ,
    }
    [SerializeField] RaycastCheck raycastCheck;
    [SerializeField] CheckerRayCast checkerRayCast;
    [SerializeField] GameObject goContent;
    [SerializeField] bool  invertir;
    [SerializeField] Tipo tipo;


    void Update()
    {
        if (raycastCheck != null)
        {
            raycastCheck.Direccion = new Vector2(goContent.gameObject.transform.localScale.x, raycastCheck.Direccion.y);
        }
        if (checkerRayCast != null)
        {
            switch (tipo)
            {
                case Tipo.scaleX:
                    {
                        checkerRayCast.Direccion = new Vector2(goContent.gameObject.transform.localScale.x, checkerRayCast.Direccion.y);
                        break;
                    }
                case Tipo.rotationZ:
                    {
                        float lado = 1;
                        float angulo = goContent.transform.eulerAngles.z;
                        if (angulo>160 || angulo < -160)
                        {
                            lado = -1;
                        }
                        if (invertir)
                        {
                            lado *= -1;
                        }
                        checkerRayCast.Direccion = new Vector2(lado, checkerRayCast.Direccion.y);
                        //checkerRayCast.OffsetOrigen = new Vector2(checkerRayCast.OffsetOrigen.x *lado, checkerRayCast.OffsetOrigen.y);
                        break;
                    }
            }
        }
    }
}
