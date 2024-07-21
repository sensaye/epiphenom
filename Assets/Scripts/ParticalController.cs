using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public List<ParticleSystem> particleSystems; // Particle System'leri buraya ekleyin
    public int maxParticlesStart = 10;
    public int maxParticlesEnd = 100;
    public float duration = 5f;
    public int maxParticlesIncrease = 10;

    void Start()
    {
        foreach (var ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.maxParticles = 0; // Ýsteðinize göre baþlangýç deðerini ayarlayýn
        }
        // Baþlangýçta maxParticles deðerlerini ayarla
        foreach (var ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.maxParticles = maxParticlesStart;
        }

        // Coroutine'i baþlat
        StartCoroutine(ChangeMaxParticles());
    }

    IEnumerator ChangeMaxParticles()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            int currentMaxParticles = (int)Mathf.Lerp (maxParticlesStart, maxParticlesEnd, t);

            foreach (var ps in particleSystems)
            {
                var mainModule = ps.main;
                mainModule.maxParticles = currentMaxParticles;
            }

            yield return null;
        }

        // Süre tamamlandýktan sonra son deðeri ayarla
        foreach (var ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.maxParticles = maxParticlesEnd;
        }
    }
    public void IncreaseMaxParticles()
    {
        foreach (var ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.maxParticles += maxParticlesIncrease;
        }
    }
}
