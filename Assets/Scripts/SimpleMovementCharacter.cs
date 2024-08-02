using UnityEngine;

public class SimpleMovementWithCamera : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform; // Kameran�n Transform'u
    public float speed = 6f;

    void Update()
    {
        // Kullan�c�n�n girdi�i yatay ve d��ey eksen hareketlerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        // Karakteri hareket ettirme
        controller.Move(move * speed * Time.deltaTime);

        // Hareket animasyonunu tetikleme
        if (move.magnitude > 0)
        {
            animator.SetFloat("Speed", speed);
            // Karakteri d�nd�rme
            transform.forward = move;
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        // Kameray� karakterin d�nd��� y�ne �evirme
        if (move.magnitude > 0)
        {
            cameraTransform.forward = move; // Kameray� karakterin d�nd��� y�ne �evir
        }
    }
}
