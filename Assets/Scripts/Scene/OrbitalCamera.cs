using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public Transform lightTarget;  // Işık karakteri
    public Transform shadowTarget;  // Gölge karakteri
    private Transform currentTarget;  // Şu anki hedef
    public Vector3 offset;    // Kameranın hedefe göre pozisyonu
    public float mouseSensitivity = 2.0f;
    public float distance = 5.0f;  // Kameranın hedeften uzaklığı
    public float minHeight = 1.0f; // Kameranın minimum yüksekliği

    float currentX = 0.0f;
    float currentY = 0.0f;
    public float yMinLimit = -40f;  // Dikey eksendeki minimum açı
    public float yMaxLimit = 80f;   // Dikey eksendeki maksimum açı

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentTarget = lightTarget;  // Başlangıçta ışık karakterini hedefle
    }

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);  // Dikey açıları sınırla

        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 newPosition = currentTarget.position + rotation * direction + offset;
        newPosition.y = Mathf.Max(newPosition.y, currentTarget.position.y + minHeight); // Kameranın minimum yüksekliğini koru
        transform.position = newPosition;

        transform.LookAt(currentTarget.position);
    }

    void Update()
    {
        // Tab tuşuna basıldığında hedefi değiştir
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTarget();
        }
    }

    void SwitchTarget()
    {
        if (currentTarget == lightTarget)
        {
            currentTarget = shadowTarget;
        }
        else
        {
            currentTarget = lightTarget;
        }
    }
}
