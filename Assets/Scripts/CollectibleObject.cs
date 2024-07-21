using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Çarpýþma karakterle mi gerçekleþti kontrol et
        if (other.CompareTag("Player"))
        {
            // Karakterin ParticleController script'ini al
            ParticleController particleController = other.GetComponent<ParticleController>();

            
            if (particleController != null)
            {
                // MaxParticles deðerini artýr
                particleController.IncreaseMaxParticles();
            }

            // Nesneyi yok et
            Destroy(gameObject);
        }
    }
}


