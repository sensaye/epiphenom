using UnityEngine;
using System.Collections;

public class LightFocus : MonoBehaviour
{
    public Light pointLight;
    public Light spotLight;
    public float transitionSpeed = 2f;
    public float focusDuration = 0.5f; // Odaklanma süresi
    public float defocusDuration = 0.5f; // Kapanma süresi
    public KeyCode focusKey = KeyCode.F;

    private float initialSpotAngle;
    private float targetSpotAngle = 10f;
    private float initialPointRange;
    private float targetPointRange = 1f;
    private float initialPointIntensity;
    private float initialSpotIntensity;
    private bool isFocusing = false;

    void Start()
    {
        spotLight.enabled = false; // Başlangıçta SpotLight kapalı
        initialSpotAngle = spotLight.spotAngle;
        initialPointRange = pointLight.range;
        initialPointIntensity = pointLight.intensity;
        initialSpotIntensity = spotLight.intensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(focusKey))
        {
            isFocusing = true;
            spotLight.enabled = true;
            StopAllCoroutines(); // Bekleyen korutinleri durdur
            StartCoroutine(FocusLight());
        }

        if (Input.GetKeyUp(focusKey))
        {
            isFocusing = false;
            StopAllCoroutines(); // Bekleyen korutinleri durdur
            StartCoroutine(ResetLight());
        }
    }

    private IEnumerator FocusLight()
    {
        float elapsedTime = 0f;

        while (elapsedTime < focusDuration)
        {
            float t = elapsedTime / focusDuration;

            pointLight.range = Mathf.Lerp(initialPointRange, targetPointRange, t);
            pointLight.intensity = Mathf.Lerp(initialPointIntensity, 0, t);

            spotLight.spotAngle = Mathf.Lerp(initialSpotAngle, targetSpotAngle, t);
            spotLight.intensity = Mathf.Lerp(0, initialSpotIntensity, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ayarlamaları kesinleştir
        pointLight.range = targetPointRange;
        pointLight.intensity = 0;
        spotLight.spotAngle = targetSpotAngle;
        spotLight.intensity = initialSpotIntensity;
    }

    private IEnumerator ResetLight()
    {
        float elapsedTime = 0f;

        while (elapsedTime < defocusDuration)
        {
            float t = elapsedTime / defocusDuration;

            pointLight.range = Mathf.Lerp(targetPointRange, initialPointRange, t);
            pointLight.intensity = Mathf.Lerp(0, initialPointIntensity, t);

            spotLight.spotAngle = Mathf.Lerp(targetSpotAngle, initialSpotAngle, t);
            spotLight.intensity = Mathf.Lerp(initialSpotIntensity, 0, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ayarlamaları kesinleştir
        pointLight.range = initialPointRange;
        pointLight.intensity = initialPointIntensity;
        spotLight.spotAngle = initialSpotAngle;
        spotLight.intensity = 0;
        spotLight.enabled = false;
    }
}
