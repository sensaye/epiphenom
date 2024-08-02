using System.Collections.Generic;
using UnityEngine;

public class CombinedSpotlightReflection : MonoBehaviour
{
    public Light mainSpotlight; // Işık kaynağı
    public List<Transform> reflectiveSurfaces; // Yansıtıcı yüzeylerin listesi
    public List<Light> reflectedSpotlights; // Yansıtılan spotlight'ların listesi
    private bool isReflecting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isReflecting = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            isReflecting = false;
            foreach (Light reflectedLight in reflectedSpotlights)
            {
                reflectedLight.enabled = false;
            }
            return; // Update metodundan çık
        }

        if (!isReflecting || mainSpotlight == null)
        {
            foreach (Light reflectedLight in reflectedSpotlights)
            {
                reflectedLight.enabled = false;
            }
            return;
        }

        Vector3 currentPosition = mainSpotlight.transform.position;
        Vector3 currentDirection = mainSpotlight.transform.forward;

        for (int i = 0; i < reflectiveSurfaces.Count; i++)
        {
            Transform surface = reflectiveSurfaces[i];
            Light reflectedLight = reflectedSpotlights[i];

            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit))
            {
                if (reflectiveSurfaces.Contains(hit.transform))
                {
                    Vector3 normal = hit.normal;
                    Vector3 reflectedDirection = Vector3.Reflect(currentDirection, normal);

                    reflectedLight.transform.position = hit.point;
                    reflectedLight.transform.rotation = Quaternion.LookRotation(reflectedDirection);
                    reflectedLight.enabled = true;

                    // Güncellenmiş pozisyon ve yön ile devam et
                    currentPosition = hit.point;
                    currentDirection = reflectedDirection;

                    // Aynı yüzeyde kalmamak için break komutu ekleyelim
                    break;
                }
                else
                {
                    reflectedLight.enabled = false;
                }
            }
            else
            {
                reflectedLight.enabled = false;
            }
        }
    }
}
