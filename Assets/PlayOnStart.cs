using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStart : MonoBehaviour
{
    public ParticleSystem particles;

    private void OnEnable()
    {
        particles.Play();
    }
}
