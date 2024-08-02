using UnityEngine;

public class LightButtonInteraction : MonoBehaviour
{
    public Light spotLight; // Spot ışığı referansı
    public Transform button; // Button referansı
    public GateDoorController gateDoor; // GateDoorController referansı

    void Update()
    {
        // Işığın Button'a ulaşıp ulaşmadığını kontrol et
        if (IsLightHittingButton())
        {
            gateDoor.OpenGate();
        }
        else
        {
            gateDoor.CloseGate(); // Işık button'a ulaşmadığında kapıyı kapalı tut
        }
    }

    bool IsLightHittingButton()
    {
        RaycastHit hit;
        Vector3 direction = button.position - spotLight.transform.position;

        if (Physics.Raycast(spotLight.transform.position, direction, out hit))
        {
            if (hit.collider.gameObject == button.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
