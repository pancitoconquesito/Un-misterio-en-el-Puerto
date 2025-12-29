using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public enum TypeMovement
    {
        PingPong, Loop
    }
    [SerializeField] GameObject m_go_Points;
    [SerializeField] TypeMovement m_typeMovement;
    [SerializeField] float m_minDistance;
    [SerializeField] float speed = 1f;
    [SerializeField] float tiempoEspera = 0f;
    [SerializeField] bool detenido = false;
    [SerializeField] bool eliminarNodos = true;

    Vector3[] m_LNodes;
    int currentNode;
    int countNodes;
    bool forward = true;
    bool isNew = true;
    float m_offesetYPlayer;
    public bool Detenido { get => detenido; set => detenido = value; }

    private void Awake()
    {
        GameObject objTargetPoints = transform.gameObject;
        if (m_go_Points != null)
        {
            objTargetPoints = m_go_Points;
        }
        int countChildren = objTargetPoints.transform.childCount;
        m_LNodes = new Vector3[countChildren];
        for (int i = 0; i < countChildren; i++)
        {
            m_LNodes[i] = objTargetPoints.transform.GetChild(i).transform.position;
        }

        if (eliminarNodos)
        {
            foreach (Transform child in objTargetPoints.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            foreach (Transform child in objTargetPoints.transform)
            {
                child.SetParent(null);
            }
        }
    }
    void Start()
    {
        currentNode = 0;
        countNodes = m_LNodes.Length;
        m_offesetYPlayer = transform.localScale.y/2f;
    }
    private void Update()
    {
        if (detenido)
        {
            return;
        }
        CalculateNode();
    }
    void FixedUpdate()
    {
        if (detenido)
        {
            return;
        }
        MoveObject();
    }
    private void MoveObject()
    {
        transform.Translate((m_LNodes[currentNode]- transform.position).normalized * speed*Time.fixedDeltaTime, Space.World);
    }

    private void CalculateNode()
    {
        float currentDistance = Vector2.Distance(m_LNodes[currentNode], transform.position);
        if (currentDistance < m_minDistance)
        {
            ChangeNxPosition();
            detenido = true;
            Invoke("ReActivar", tiempoEspera);
        }
    }
    void ReActivar()
    {
        detenido = false;
    }
    private void ChangeNxPosition()
    {
        switch (m_typeMovement)
        {
            case TypeMovement.Loop:
                {
                    currentNode = (currentNode + 1) % countNodes;
                    break;
                }
            case TypeMovement.PingPong:
                {
                    if(forward)
                    {
                        if(currentNode == countNodes-1)
                        {
                            forward = false;
                            currentNode = countNodes - 2;
                        }
                        else
                        {
                            currentNode++;
                        }
                    }
                    else if(!forward)
                    {
                        if (currentNode == 0) {
                            forward = true;
                            currentNode = 1;
                        }
                        else
                        {
                            currentNode--;
                        }
                    }
                    break;
                }
        }
    }

    public void FlipEstado() => detenido = !detenido;

    internal float GetHeightOffset()=> m_offesetYPlayer;
}
