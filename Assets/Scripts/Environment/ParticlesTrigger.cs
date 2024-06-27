using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ParticleSystem.MainModule mainModule = _particleSystem.main;
            mainModule.loop = false; // Stop new particles from spawning

            ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
            emissionModule.rateOverTime = 0; // Stop emission of particles

            ParticleSystem[] subEmitters = _particleSystem.GetComponentsInChildren<ParticleSystem>();
            foreach (var emitter in subEmitters)
            {
                ParticleSystem.MainModule subMainModule = emitter.main;
                subMainModule.loop = false; // Stop new particles from spawning
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ParticleSystem.MainModule mainModule = _particleSystem.main;
            mainModule.loop = true;

            ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
            emissionModule.rateOverTime = 1.5f;

            ParticleSystem[] subEmitters = _particleSystem.GetComponentsInChildren<ParticleSystem>();
            foreach (var emitter in subEmitters)
            {
                ParticleSystem.MainModule subMainModule = emitter.main;
                subMainModule.loop = true;
            }

            _particleSystem.Play();
        }
    }
}
