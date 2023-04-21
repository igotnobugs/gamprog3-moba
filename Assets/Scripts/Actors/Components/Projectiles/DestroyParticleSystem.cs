using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    private float lifetime = 0f;

    private void Awake()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        lifetime = ps.main.startLifetimeMultiplier + ps.main.duration;
        Destroy(gameObject, lifetime);
    }
}
