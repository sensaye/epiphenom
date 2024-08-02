using UnityEngine;

public class Water2Reflection : MonoBehaviour
{
    public Light waterSpotlight; // İlk yansıyan ışık kaynağı (water spot)
    public Light reflectedSpotlight; // Yansıtılan spotlight (water 2 spot)
    public Transform reflectiveSurface; // Yansıtıcı yüzey (water 2)
    public LayerMask reflectiveLayer; // Yansıtıcı katman

    void Start()
    {
        // Başlangıçta reflectedSpotlight'ı kapat
        if (reflectedSpotlight != null)
        {
            reflectedSpotlight.enabled = false;
        }

        // İlk kontrol
        if (waterSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            Debug.LogError("Bir veya daha fazla bileşen eksik. Lütfen tüm bileşenleri atayın.");
        }
    }

    void Update()
    {
        // Eğer gerekli bileşenler eksikse geri dön
        if (waterSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            return;
        }

        // Eğer waterSpotlight aktif değilse, reflectedSpotlight'ı kapat
        if (!waterSpotlight.enabled)
        {
            reflectedSpotlight.enabled = false;
            return;
        }

        Vector3 spotlightDirection = waterSpotlight.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(waterSpotlight.transform.position, spotlightDirection, out hit, 100f, reflectiveLayer))
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
