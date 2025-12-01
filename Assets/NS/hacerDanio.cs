using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hacerDanio : MonoBehaviour
{
    [SerializeField] private dataDanio m_dataDanio;
    [SerializeField] private ObjectPooling m_ObjectPooling;
    [SerializeField] private float fuerzaMov;
    [SerializeField] private bool IsPlayerAttack;
    [SerializeField] private movementPJ m_movementPJ;
    [SerializeField] CaidaResSpawn_Manager m_CaidaResSpawn_Manager;
    [SerializeField] Generic_Ontrigger genericOnTrigger;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable currentDamage = collision.GetComponent<IDamageable>();
        if (currentDamage != null)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            //int numContacts = collision.GetContacts(contacts);
            //m_dataDanio.SetPositionCollision(/*contacts[0].point*/transform.position);

            float ladoX = transform.position.x - collision.gameObject.transform.position.x;
            if (ladoX < 0) m_dataDanio.setLado(GLOBAL_TYPE.LADO.iz);//yo ataque desde su izquierda
            else m_dataDanio.setLado(GLOBAL_TYPE.LADO.der);
            
            //print("lugar : "+contactPoint);
            m_dataDanio.updateTransform();
            //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = contactPoint;
            Vector3 dir = (transform.position - collision.transform.position).normalized *1.5f;
            Vector2 positionInstance = collision.transform.position + dir;

            m_dataDanio.SetPositionCollision(positionInstance);
            if (m_dataDanio.tipo_danio == GLOBAL_TYPE.TIPO_DANIO.vacio)
            {
                Vector2 position = m_CaidaResSpawn_Manager.GetNewPosition();
                m_dataDanio.SetPositionCollision(position);
            }
            if (currentDamage.RecibirDanio_I(m_dataDanio))
            {
                if (currentDamage.IsEnemy())
                {
                    if (IsPlayerAttack)
                    {
                        Audio_FX_PJ.PlaySound(Sound_FX_BANK.Sound_FX_Names.PJ_danio);
                        if (m_movementPJ.IsGroundedFunction())
                        {
                            if (ladoX < 0)
                            {
                                m_movementPJ.ApplyForce_X(Vector2.left, fuerzaMov);
                            }
                            else
                            {
                                m_movementPJ.ApplyForce_X(Vector2.right, fuerzaMov);
                            }
                        }
                        else
                        {
                            if(transform.position.y > collision.transform.position.y && m_movementPJ.IsAtaqueAbajo())
                            {
                                m_movementPJ.ApplyForce_X(Vector2.up, fuerzaMov * 1.8f);
                                m_movementPJ.AnimatorPJ.SetTrigger("startJump");
                            }
                        }
                    }
                }
                //else
                //{
                //    //
                //    Debug.Log("Empuje hacia arriba");
                //    m_movementPJ.ApplyForce_X(Vector2.up, fuerzaMov * 1.8f);
                //    m_movementPJ.AnimatorPJ.SetTrigger("startJump");
                //}
                if (m_ObjectPooling != null)
                    m_ObjectPooling.emitirObj(0.4f, positionInstance);

                if (genericOnTrigger != null)
                {
                    genericOnTrigger.EjecutarEvento();
                }
            }
        }
    }
}
