using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterMovementAndPickup : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private bool isGrounded = true;
    public Transform holdPosition; // Nesnenin tutulacağı pozisyon
    private GameObject heldObject; // Tutulan nesne

    void Update()
    {
        // Hareket
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Karakterin yönünü kameranın yönüne göre ayarla
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        }

        // Zıplama
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Nesne alma veya bırakma
        if (Input.GetKeyDown(KeyCode.E)) // "E" tuşuna basıldığında nesneyi al veya bırak
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void FixedUpdate()
    {
        // Tutulan nesneyi sürekli olarak holdPosition'da tut
        if (heldObject != null)
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        // Karakterin önündeki nesneyi raycast ile tespit et
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = holdPosition.rotation; // Nesnenin yönünü ayarla
        heldObject.transform.parent = holdPosition;
    }

    void DropObject()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}
