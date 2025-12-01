using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_A_ANIM : MonoBehaviour
{
    [SerializeField] List<Node_SpawnerDisparo> l_spaners;
    
    public void ANIM_Disparar(string tag)
    {
        foreach (var item in l_spaners)
        {
            if (tag == item.TAG)
            {
                item.spawner.DispararProyectil();
            }
        }
    }

    [System.Serializable]
    public class Node_SpawnerDisparo
    {
        public string TAG;
        public SpawnerDisparo spawner;
    }
}

