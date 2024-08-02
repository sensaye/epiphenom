using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Karakterin tag'ini kontrol edin (örneðin, "Player" olarak ayarlanmýþsa)
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
        // Sahneyi deðiþtir (örneðin, "NewScene" adlý sahneye geçiþ)
        SceneManager.LoadScene(2);
    }
}
