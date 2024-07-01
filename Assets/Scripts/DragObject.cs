using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float rayDistance = 2f; // Raycast mesafesi
    public Transform holdPosition; // Nesneyi tutma pozisyonu

    private GameObject objectBeingHeld;
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda
        {
            if (!isHolding)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        if (isHolding)
        {
            HoldObject();
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Draggable")) // "Draggable" etiketi olan nesneleri sürükleyebilmek için kontrol
            {
                objectBeingHeld = hit.collider.gameObject;
                objectBeingHeld.GetComponent<Rigidbody>().useGravity = false; // Nesnenin düþmesini engelle
                objectBeingHeld.GetComponent<Rigidbody>().freezeRotation = true; // Nesnenin dönmesini engelle
                isHolding = true;
            }
        }
    }

    void DropObject()
    {
        objectBeingHeld.GetComponent<Rigidbody>().useGravity = true;
        objectBeingHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectBeingHeld = null;
        isHolding = false;
    }

    void HoldObject()
    {
        objectBeingHeld.transform.position = holdPosition.position;
    }
}
