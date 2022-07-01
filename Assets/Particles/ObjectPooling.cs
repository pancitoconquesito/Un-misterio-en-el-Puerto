using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //public string nombreObjeto;
    public GameObject objeto;
    public int antObjeto;
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
        if (antObjeto > 1)
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
        antObjeto++;
        go.SetActive(false);
        colaOBjeto.Enqueue(go);
    }
    //obtener
    public void emitirObj(float tiempo)
    {
        GameObject objA = getObjPool();
        if (objA != null)
            StartCoroutine(retornarObjPool(tiempo, objA));
    }
    public void emitirObj(float tiempo, Vector2 _position)
    {
        position = _position;
        GameObject objA = getObjPool();
        if (objA != null)
            StartCoroutine(retornarObjPool(tiempo, objA));
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
