using UnityEngine;

[RequireComponent(typeof(Light))]
public class SPT_PentagramLightFlicker : MonoBehaviour
{
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;

    float random;

    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time*6);
        GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
