using UnityEngine;

public class SimpleMovementWithCamera : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform; // Kameranýn Transform'u
    public float speed = 6f;

    void Update()
    {
        // Kullanýcýnýn girdiði yatay ve düþey eksen hareketlerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        // Karakteri hareket ettirme
        controller.Move(move * speed * Time.deltaTime);

        // Hareket animasyonunu tetikleme
        if (move.magnitude > 0)
        {
            animator.SetFloat("Speed", speed);
            // Karakteri döndürme
            transform.forward = move;
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        // Kamerayý karakterin döndüðü yöne çevirme
        if (move.magnitude > 0)
        {
            cameraTransform.forward = move; // Kamerayý karakterin döndüðü yöne çevir
        }
    }
}
