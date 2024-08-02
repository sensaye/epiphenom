using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Karakterin tag'ini kontrol edin (�rne�in, "Player" olarak ayarlanm��sa)
        if (other.CompareTag("Player"))
        {
            if (!hasEntered)
            {
                hasEntered = true;
                StartCoroutine(TransitionAfterDelay(3f)); // 3 saniye bekle
            }
        }
    }

    private IEnumerator TransitionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Sahneyi de�i�tir (�rne�in, "NewScene" adl� sahneye ge�i�)
        SceneManager.LoadScene(2);
    }
}
