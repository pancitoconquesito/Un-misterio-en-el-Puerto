using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //public string nombreObjeto;
    public GameObject objeto;
    public int antObjeto;

    internal void ForceReturnToPool(GameObject miObj)
    {
        ReturnObjPool(miObj);
    }

    public bool AlinearConPadre=true;
    private GameObject padre;

    private Queue<GameObject> colaOBjeto;
    private void startCola()
    {
        colaOBjeto = new Queue<GameObject>();
        for (int i = 0; i < antObjeto; i++)
        {
            GameObject newPolvo = Instantiate(objeto);
            if (AlinearConPadre)
                newPolvo.transform.SetParent(padre.transform);
            colaOBjeto.Enqueue(newPolvo);
            newPolvo.SetActive(false);
        }
    }
    private Vector2 position;
    void Start()
    {
        if(AlinearConPadre)
            padre = transform.gameObject;
        startCola();
        position = transform.position;
    }

    private GameObject getObjPool()
    {
        GameObject newObj;
        //Debug.Log("antObjeto: "+ antObjeto);

        if (antObjeto >= 1)
        {
            antObjeto--;
            newObj = colaOBjeto.Dequeue();
            newObj.SetActive(true);

            newObj.transform.SetPositionAndRotation(position, Quaternion.identity);

            return newObj;
        }
        return null;
    }
    private void ReturnObjPool(GameObject go)
    {
        //Debug.Log("acá");
        if (!go.activeSelf) {
            Debug.Log("!go.activeSelf");
            return;
        } 
        //Debug.Log("ReturnObjPool");
        antObjeto++;
        go.SetActive(false);
        colaOBjeto.Enqueue(go);
    }
    //obtener
    public void emitirObj(float tiempo, bool sacarDePadre=false)
    {
        GameObject objA = getObjPool();
        if (objA != null) {
            if (sacarDePadre)
            {
                objA.transform.parent = null;
            }
            PoolObjectForceObject poolReturn = objA.GetComponent<PoolObjectForceObject>();
            //if (poolReturn!=null)
            //{
                poolReturn?.AddObjectPooling(this);
            //}
            StartCoroutine(retornarObjPool(tiempo, objA));
        }
    }
    public GameObject emitirObj(float tiempo, Vector2 _position, bool sacarDePadre = false, bool returnObj=false)
    {
        position = _position;
        GameObject objA = getObjPool();
        if (objA != null)
        {
            //Debug.Log("acá-------------");
            PoolObjectForceObject poolReturn = objA.GetComponent<PoolObjectForceObject>();
            //if (poolReturn != null)
            //{
                poolReturn?.AddObjectPooling(this);
            //}
            if (sacarDePadre)
            {
                objA.transform.parent = null;
            }
            StartCoroutine(retornarObjPool(tiempo, objA));
            if (returnObj)
            {
                return objA;
            }
        }
        //Debug.Log("va null");
        return null;
    }
    IEnumerator retornarObjPool(float tiempo, GameObject miObj)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        ReturnObjPool(miObj);
    }
    /*
    public string getNombre()
    {
        return nombreObjeto;
    }
    */


    public void destruirLista()
    {
        while (colaOBjeto.Count > 0)
        {
            Destroy(colaOBjeto.Dequeue());
        }
    }
}
