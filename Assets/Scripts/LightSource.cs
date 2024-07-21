using UnityEngine;

public class LightSource : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool isAbsorbing = false;
    public float absorptionSpeed = 1.0f;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isAbsorbing)
        {
            // Partikül sistemini hýzlandýr
            var main = particleSystem.main;
            main.simulationSpeed = Mathf.Max(0, main.simulationSpeed + absorptionSpeed * Time.deltaTime);

            // Partikülleri küçült
            var emission = particleSystem.emission;
            var rate = emission.rateOverTime;
            rate = new ParticleSystem.MinMaxCurve(Mathf.Max(0, rate.constant - absorptionSpeed * Time.deltaTime));
            emission.rateOverTime = rate;

            if (rate.constant == 0)
            {
                // Iþýk kaynaðý yok oldu
                Destroy(gameObject);
            }
        }
    }

    public void StartAbsorption()
    {
        isAbsorbing = true;
    }

    public void StopAbsorption()
    {
        isAbsorbing = false;
    }
}
