using UnityEngine;

public class MovingSky : MonoBehaviour
{
    [Header("Skybox Settings")]
    [SerializeField] private float rotationSpeed = 0.1f;

    private Material skyboxMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skyboxMaterial != null)
        {
            float currentRotation = skyboxMaterial.GetFloat("_Rotation");
            skyboxMaterial.SetFloat("_Rotation", currentRotation + rotationSpeed * Time.deltaTime);
        }
    }
}
