using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hacerDanio : MonoBehaviour
{
    [SerializeField]private dataDanio m_dataDanio;
    [SerializeField] private ObjectPooling m_ObjectPooling;


    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable currentDamage = collision.GetComponent<IDamageable>();
        if (currentDamage != null)
        {
            float ladoX = transform.position.x - collision.gameObject.transform.position.x;
            if (ladoX < 0) m_dataDanio.setLado(GLOBAL_TYPE.LADO.iz);//yo ataque desde su izquierda
            else m_dataDanio.setLado(GLOBAL_TYPE.LADO.der);


            /*
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;

            */
            //print("lugar : "+contactPoint);
            m_dataDanio.updateTransform();

            //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = contactPoint;
            Vector3 dir = (transform.position - collision.transform.position).normalized *1.5f;
            Vector2 positionInstance = collision.transform.position + dir;

            if (currentDamage.RecibirDanio_I(m_dataDanio))
            {
                if (m_ObjectPooling != null)
                    m_ObjectPooling.emitirObj(0.4f, positionInstance);
            }
        }
    }
}
