using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (karakter)
    public float smoothSpeed = 0.125f; // Yumuþatma hýzý
    public Vector3 offset; // Kameranýn hedefe olan ofseti

    void LateUpdate()
    {
        // Hedef pozisyonu ile kamera arasýndaki hedef pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;
        // Kamerayý yumuþak bir þekilde hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Kamerayý hedefe doðru döndür
        transform.LookAt(target);
    }
}
