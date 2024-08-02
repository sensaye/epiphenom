using UnityEngine;

public class CylinderReflection : MonoBehaviour
{
    public Light mainSpotlight; // Karakterden gelen ışık kaynağı
    public Light reflectedSpotlight; // Yansıtılan spotlight (cylinder spot)
    public Transform reflectiveSurface; // Yansıtıcı yüzey (cylinder)
    public LayerMask reflectiveLayer; // Yansıtıcı katman

    void Start()
    {
        // Başlangıçta reflectedSpotlight'ı kapat
        if (reflectedSpotlight != null)
        {
            reflectedSpotlight.enabled = false;
        }

        // İlk kontrol
        if (mainSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            Debug.LogError("Bir veya daha fazla bileşen eksik. Lütfen tüm bileşenleri atayın.");
        }
    }

    void Update()
    {
        // Eğer gerekli bileşenler eksikse geri dön
        if (mainSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            return;
        }

        // Eğer mainSpotlight aktif değilse, reflectedSpotlight'ı kapat
        if (!mainSpotlight.enabled)
        {
            reflectedSpotlight.enabled = false;
            return;
        }

        Vector3 spotlightDirection = mainSpotlight.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(mainSpotlight.transform.position, spotlightDirection, out hit, 100f, reflectiveLayer))
        {
            if (hit.transform == reflectiveSurface)
            {
                Vector3 normal = hit.normal;
                Vector3 reflectedDirection = Vector3.Reflect(spotlightDirection, normal);

                reflectedSpotlight.transform.position = hit.point;
                reflectedSpotlight.transform.rotation = Quaternion.LookRotation(reflectedDirection);
                reflectedSpotlight.enabled = true;

                Debug.Log("Yansıma yüzeyine çarpıldı: " + hit.transform.name);
            }
            else
            {
                reflectedSpotlight.enabled = false;
            }
        }
        else
        {
            reflectedSpotlight.enabled = false;
        }
    }
}
