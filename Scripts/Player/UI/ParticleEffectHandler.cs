using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour
{
    public void PlayParticles(ParticleSystem particles)
    {
        particles.Play();
    }
}
