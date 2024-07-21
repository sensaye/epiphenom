using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �arp��ma karakterle mi ger�ekle�ti kontrol et
        if (other.CompareTag("Player"))
        {
            // Karakterin ParticleController script'ini al
            ParticleController particleController = other.GetComponent<ParticleController>();

            
            if (particleController != null)
            {
                // MaxParticles de�erini art�r
                particleController.IncreaseMaxParticles();
            }

            // Nesneyi yok et
            Destroy(gameObject);
        }
    }
}


