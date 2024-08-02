using UnityEngine;

public class WaterReflection : MonoBehaviour
{
    public Light mainSpotlight; // Karakterden gelen ışık kaynağı
    public Light reflectedSpotlight; // Bu yüzeyden yansıyan ışık
    public Transform reflectiveSurface; // Bu yansıtıcı yüzey
    public LayerMask reflectiveLayer; // Yansıtıcı katman

    private bool isReflecting = false;

    void Start()
    {
        if (mainSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            return;
        }
    }

    void Update()
    {
        if (mainSpotlight == null || reflectedSpotlight == null || reflectiveSurface == null)
        {
            return;
        }

        // F tuşuna basıldığında isReflecting'i değiştir
        if (Input.GetKeyDown(KeyCode.F))
        {
            isReflecting = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            isReflecting = false;
            reflectedSpotlight.enabled = false; // Işığı kapat
            return; // Update metodundan çık
        }

        // isReflecting ve mainSpotlight kontrolü
        if (isReflecting && mainSpotlight.enabled)
        {
            Vector3 spotlightDirection = mainSpotlight.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(mainSpotlight.transform.position, spotlightDirection, out hit, Mathf.Infinity, reflectiveLayer))
            {
                if (hit.transform == reflectiveSurface)
                {
                    Vector3 normal = hit.normal;
                    Vector3 reflectedDirection = Vector3.Reflect(spotlightDirection, normal);

                    reflectedSpotlight.transform.position = hit.point;
                    reflectedSpotlight.transform.rotation = Quaternion.LookRotation(reflectedDirection);
                    reflectedSpotlight.enabled = true;
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
        else
        {
            reflectedSpotlight.enabled = false;
        }
    }
}
