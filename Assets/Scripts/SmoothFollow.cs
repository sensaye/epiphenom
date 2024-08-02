using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (karakter)
    public float smoothSpeed = 0.125f; // Yumu�atma h�z�
    public Vector3 offset; // Kameran�n hedefe olan ofseti

    void LateUpdate()
    {
        // Hedef pozisyonu ile kamera aras�ndaki hedef pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;
        // Kameray� yumu�ak bir �ekilde hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Kameray� hedefe do�ru d�nd�r
        transform.LookAt(target);
    }
}
