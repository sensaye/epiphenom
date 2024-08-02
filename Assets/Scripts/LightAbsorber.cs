using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightAbsorber : MonoBehaviour
{
    public ParticleSystem[] characterParticleSystems; // Karakterdeki 3 ParticleSystem
    public float absorbDistance = 5.0f;
    public KeyCode absorbKey = KeyCode.E;
    public int maxParticles = 1000;
    public float absorbSpeed = 1.0f;

    private List<ParticleSystem> nearbyLightSources = new List<ParticleSystem>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            ParticleSystem lightSource = other.GetComponent<ParticleSystem>();
            if (lightSource != null && !nearbyLightSources.Contains(lightSource))
            {
                nearbyLightSources.Add(lightSource);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            ParticleSystem lightSource = other.GetComponent<ParticleSystem>();
            if (lightSource != null)
            {
                nearbyLightSources.Remove(lightSource);
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(absorbKey))
        {
            for (int i = nearbyLightSources.Count - 1; i >= 0; i--)
            {
                ParticleSystem lightSource = nearbyLightSources[i];
                if (lightSource != null && Vector3.Distance(transform.position, lightSource.transform.position) <= absorbDistance)
                {
                    StartCoroutine(AbsorbCoroutine(lightSource));
                }
            }
        }
    }

    IEnumerator AbsorbCoroutine(ParticleSystem lightSource)
    {
        if (lightSource == null) yield break;

        Animator animator = lightSource.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("Animator found, setting trigger.");
            animator.SetTrigger("absorb"); // Trigger ismini kontrol edin
            Debug.Log("Tetikleyici ayarlandý: absorb");
        

    }
        else
        {
            Debug.LogWarning("No Animator component found on light source.");
        }

        Transform lightSourceTransform = lightSource.transform;
        Vector3 initialPosition = lightSourceTransform.position;
        Vector3 characterPosition = transform.position;
        Vector3 initialScale = lightSourceTransform.localScale;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, characterPosition);

        while (Vector3.Distance(lightSourceTransform.position, characterPosition) > 0.1f)
        {
            if (lightSource == null || lightSourceTransform == null)
            {
                yield break;
            }

            float distanceCovered = (Time.time - startTime) * absorbSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            lightSourceTransform.position = Vector3.Lerp(initialPosition, characterPosition, fractionOfJourney);
            lightSourceTransform.localScale = Vector3.Lerp(initialScale, Vector3.zero, fractionOfJourney);
            yield return null;
        }

        // Iþýk kaynaðý karaktere temas ettiðinde yok olur ve karakterin enerjisi artar
        if (lightSource != null)
        {
            int particlesToAdd = (int)(lightSource.particleCount * 0.1f);
            foreach (var ps in characterParticleSystems)
            {
                if (ps != null)
                {
                    var main = ps.main;
                    main.maxParticles += 50;
                }
            }

            lightSource.gameObject.SetActive(false);
            nearbyLightSources.Remove(lightSource);
        }
    }
}
