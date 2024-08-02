using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private int lastIdleState = -1;  // Son idle durumu kaydetmek için

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Karakterin yönünü deðiþtirme
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Karakteri hareket ettirme
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            // Hareket animasyonunu tetikleme
            animator.SetFloat("Speed", speed);
        }
        else
        {
            // Hareket yoksa rastgele idle animasyonunu tetikleme
            animator.SetFloat("Speed", 0);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                int randomIdle;
                do
                {
                    randomIdle = Random.Range(0, 2); // 0 ve 1 arasýnda rastgele sayý
                } while (randomIdle == lastIdleState); // Ayný deðeri üst üste verme

                animator.SetInteger("IdleState", randomIdle);
                lastIdleState = randomIdle; // Son idle durumunu kaydet
            }
        }
    }
}