using UnityEngine;

public class SpotlightEffectWithGradient : MonoBehaviour
{
    public Material spotlightMaterial;
    public float spotlightRadius = 0.2f;
    public Color gradientColor = Color.white;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        spotlightMaterial.SetVector("_SpotlightPos", new Vector4(mousePos.x / Screen.width * 2 - 1, mousePos.y / Screen.height * 2 - 1, 0, 0));
        spotlightMaterial.SetFloat("_SpotlightRadius", spotlightRadius);
        spotlightMaterial.SetColor("_GradientColor", gradientColor);
    }
}