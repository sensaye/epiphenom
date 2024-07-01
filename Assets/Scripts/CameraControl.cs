using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    
    float zoomSpeed = 5f;
    float rotationSpeed = 2f;
    float mouseX;
    private float targetFieldOfView = 50f;
    private bool isRotating;
    private void Update()
    {
        HandleZoom();
        HandleRotation();
    }
    private void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0) 
        {
            targetFieldOfView -= 5f;
        }
        else if(Input.mouseScrollDelta.y < 0) 
        {
            targetFieldOfView += 5f;
        }
        targetFieldOfView = Mathf.Clamp(targetFieldOfView , 10f , 50f);
        _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.FieldOfView , targetFieldOfView , Time.deltaTime * zoomSpeed);
    }
    private void HandleRotation()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            Vector3 rotation = new Vector3(0f , mouseX , 0f);
            transform.Rotate(rotation);
        }
    }
}
