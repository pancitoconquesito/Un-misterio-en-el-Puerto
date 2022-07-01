using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noVoltear : MonoBehaviour
{
    [SerializeField] private changeMirada m_changeMirada;
    
    private GLOBAL_TYPE.LADO miradaActual;
    private Vector3 escalaObj;
    private void Start()
    {
        miradaActual = m_changeMirada.getMirada();
        escalaObj = gameObject.transform.localScale;
    }


    private void Update()
    {

        if(m_changeMirada.getMirada()!= miradaActual)
        {
            miradaActual = m_changeMirada.getMirada();
            switch (miradaActual)
            {
                case GLOBAL_TYPE.LADO.iz:
                    {
                        miradaActual = GLOBAL_TYPE.LADO.iz;
                        gameObject.transform.localScale = new Vector3(-escalaObj.x, escalaObj.y, escalaObj.z);
                        break;
                    }
                case GLOBAL_TYPE.LADO.der:
                    {
                        miradaActual = GLOBAL_TYPE.LADO.der;
                        gameObject.transform.localScale = escalaObj;
                        break;
                    }
            }

        }
    }
}
