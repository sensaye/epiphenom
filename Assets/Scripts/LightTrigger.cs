using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }

    public GameObject GetLightObject()
    {
        return this.gameObject;
    }
}
