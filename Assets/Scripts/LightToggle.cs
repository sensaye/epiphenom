using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light lightToToggle; // Toggle yapmak istediðiniz ýþýk

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            lightToToggle.enabled = !lightToToggle.enabled;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            lightToToggle.innerSpotAngle += 10;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            lightToToggle.innerSpotAngle -= 10;
        }
        
    }
    
}
