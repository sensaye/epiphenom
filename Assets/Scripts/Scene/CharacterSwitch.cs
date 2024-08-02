using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject lightCharacter;
    public GameObject shadowCharacter;
    private GameObject activeCharacter;

    void Start()
    {
        activeCharacter = lightCharacter; // Başlangıçta aktif olan karakter
        SetActiveCharacter();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }

    void SwitchCharacter()
    {
        if (activeCharacter == lightCharacter)
        {
            activeCharacter = shadowCharacter;
        }
        else
        {
            activeCharacter = lightCharacter;
        }
        SetActiveCharacter();
    }

    void SetActiveCharacter()
    {
        // Aktif karakterin hareket scriptini etkinleştir, pasif olanı devre dışı bırak
        lightCharacter.GetComponent<ThirdPersonMovement>().enabled = (activeCharacter == lightCharacter);
        shadowCharacter.GetComponent<ShadowCharacterMovementAndPickup>().enabled = (activeCharacter == shadowCharacter);
    }
}
