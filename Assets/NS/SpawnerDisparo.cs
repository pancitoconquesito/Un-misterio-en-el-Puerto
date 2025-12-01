using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class SpawnerDisparo : MonoBehaviour
{
    public enum ModeMove
    {
        Lineal, Sine, Circular
    }
    public enum EnumRandomDirection
    {
        Horizontal, Vertical
    }
    [SerializeField] GameObject newProyectiL;
    [SerializeField] public List<SpawnerDisparo_node> nodes;
    [SerializeField] GameObject obj_padre;
    [SerializeField] float tiempo;
    [SerializeField] float potenciaProyectil;
    [SerializeField] bool RotateObjectToDirectionMove;
    [SerializeField] ModeMove typeMove;
    [SerializeField] float delayEntreSpawn;

    [Header("Sine move parameters")]
    [SerializeField] float sine_amplitud=5f;
    [SerializeField] float sine_frecuencia=8f;

    [Header("Sine move parameters")]
    [SerializeField] float circular_frecuencia = 2f;
    [SerializeField] float circular_radio = 1f;
    [SerializeField] float circular_expansion = 0.5f;


    [Header("Random")]
    [SerializeField] bool isRandom;
    [SerializeField] EnumRandomDirection random_direction;
    [SerializeField] GameObject random_go_A;
    [SerializeField] GameObject random_go_B;


    [Button("Change Proyectil")]
    public void ChangeProyectil()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.Contains("limite"))
            {
                continue;
            }
            ObjectPooling curr_op = child.GetComponent<ObjectPooling>();
            curr_op.objeto = newProyectiL;
        }
    }



    [Button("Add node")]
    public void AddNode()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/NodeProyectil"); // sin .prefab
        GameObject instancia = Instantiate(prefab, transform); // 'transform' es el padre
        instancia.transform.localPosition = Vector3.zero;  // posición local al padre
        instancia.transform.localRotation = Quaternion.identity;
    }

    [Button("Ordenar")]
    public void Ordenar()
    {
        nodes = new List<SpawnerDisparo_node>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.Contains("limite"))
            {
                continue;
            }
            SpawnerDisparo_node newNode = new SpawnerDisparo_node();
            newNode.obj = child;
            newNode.obj_direction = child.transform.GetChild(0).gameObject;
            newNode.pivote = child.GetComponent<ObjectPooling>();
            nodes.Add(newNode);
        }
    }

    IEnumerator SpawneadorFunction()
    {
        bool necesitaDelay = delayEntreSpawn > 0;
        yield return null;
        foreach (var item in nodes)
        {
            GameObject obj = item.pivote.emitirObj(tiempo, item.obj.transform.position, true, true);
            Debug.Log("=> " + obj.name);
            Proyectil proyectil = obj.GetComponent<Proyectil>();
            proyectil.SetPotencia(potenciaProyectil);
            proyectil.SetValues(
            (obj.transform.position - item.obj_direction.transform.position)
            , RotateObjectToDirectionMove);

            LlamarTipoProyectil(proyectil);

            if (necesitaDelay)
            {
                yield return new WaitForSeconds(delayEntreSpawn);
            }
        }
        if (!necesitaDelay)
        {
            yield return null;
        }
    }

    private void LlamarTipoProyectil(Proyectil proyectil)
    {
        if (isRandom)
        {
            switch (random_direction)
            {
                case EnumRandomDirection.Horizontal:
                {
                    foreach (SpawnerDisparo_node node in nodes)
                    {
                        float valor = UnityEngine.Random.Range(random_go_A.transform.position.x, random_go_B.transform.position.x);

                        node.obj.transform.position = new Vector3(valor, node.obj.transform.position.y, node.obj.transform.position.z);
                        node.pivote.transform.position = new Vector3(valor, node.pivote.transform.position.y, node.pivote.transform.position.z);
                    }
                    break;
                }
                case EnumRandomDirection.Vertical:
                {
                    foreach (SpawnerDisparo_node node in nodes)
                    {
                        float valor = UnityEngine.Random.Range(random_go_A.transform.position.y, random_go_B.transform.position.y);

                        node.obj.transform.position = new Vector3(node.obj.transform.position.x, valor, node.obj.transform.position.z);
                        node.pivote.transform.position = new Vector3(node.pivote.transform.position.x, valor, node.pivote.transform.position.z);
                    }
                    break;
                }
            }
        }
        switch (typeMove)
        {
            case ModeMove.Lineal:
                {
                    proyectil.SetLinealMove();
                    break;
                }
            case ModeMove.Sine:
                {
                    proyectil.SetSineMove(sine_amplitud, sine_frecuencia);
                    break;
                }
            case ModeMove.Circular:
                {
                    proyectil.SetCircularMove(circular_frecuencia, circular_radio, circular_expansion);
                    break;
                }
        }
    }

    [Button("Disparar Proyectil")]
    public void DispararProyectil()
    {
        StartCoroutine(SpawneadorFunction());
    }

    

    [System.Serializable]
    public class SpawnerDisparo_node
    {
        [SerializeField] public GameObject obj;
        [SerializeField] public GameObject obj_direction;
        [SerializeField] public ObjectPooling pivote;
        [SerializeField] public Proyectil proyectil;
    }

}

