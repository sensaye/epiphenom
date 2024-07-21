using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    public int lightCount = 0;
    private List<LightTrigger> lightTriggersInRange = new List<LightTrigger>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lightTriggersInRange.Count > 0)
        {
            PullLights();
        }
    }

    void PullLights()
    {
        for (int i = lightTriggersInRange.Count - 1; i >= 0; i--)
        {
            LightTrigger lightTrigger = lightTriggersInRange[i];
            if (lightTrigger.IsPlayerInRange())
            {
                GameObject lightObject = lightTrigger.GetLightObject();
                Destroy(lightObject);
                lightCount++;
                lightTriggersInRange.RemoveAt(i);
                Debug.Log("Iþýk çekildi. Toplam ýþýk sayýsý: " + lightCount);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            LightTrigger lightTrigger = other.GetComponent<LightTrigger>();
            if (lightTrigger != null)
            {
                lightTriggersInRange.Add(lightTrigger);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            LightTrigger lightTrigger = other.GetComponent<LightTrigger>();
            if (lightTrigger != null)
            {
                lightTriggersInRange.Remove(lightTrigger);
            }
        }
    }
}
