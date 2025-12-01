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
    public TypeMovement m_typeMovement;
    public float m_minDistance;
    public float speed = 1f;

    Vector3[] m_LNodes;
    int currentNode;
    int countNodes;
    bool forward = true;
    bool isNew = true;
    float m_offesetYPlayer;
    private void Awake()
    {
        int countChildren = transform.childCount;
        m_LNodes = new Vector3[countChildren];
        for (int i = 0; i < countChildren; i++)
        {
            m_LNodes[i] = transform.GetChild(i).transform.position;
        }
        for (int i = 0; i < countChildren; i++)
        {
            transform.GetChild(0).transform.SetParent(null);
        }
    }
    void Start()
    {
        currentNode = 0;
        countNodes = m_LNodes.Length;
        m_offesetYPlayer = transform.localScale.y;
    }

    void Update()
    {
        CalculateNode();
        MoveObject();
    }
    private void MoveObject()
    {
        transform.Translate((m_LNodes[currentNode]- transform.position).normalized * speed*Time.deltaTime, Space.World);
    }

    private void CalculateNode()
    {
        float currentDistance = Vector2.Distance(m_LNodes[currentNode], transform.position);
        if (currentDistance < m_minDistance)
        {
            ChangeNxPosition();
        }
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
    
    internal float GetHeightOffset()=> m_offesetYPlayer;
}
