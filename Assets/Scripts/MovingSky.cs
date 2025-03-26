using UnityEngine;

public class MovingSky : MonoBehaviour
{
    [Header("Skybox Settings")]
    [SerializeField] private float rotationSpeed = 0.1f;

    private Material originalSkybox;
    private Material dynamicSkybox;
    private float currentRotation;

    void Start()
    {
        // Сохраняем оригинальный материал и создаем копию
        originalSkybox = RenderSettings.skybox;

        if (originalSkybox != null)
        {
            dynamicSkybox = new Material(originalSkybox);
            RenderSettings.skybox = dynamicSkybox;
            currentRotation = dynamicSkybox.GetFloat("_Rotation");
        }
        else
        {
            Debug.LogError("Skybox material not found in Render Settings!");
        }
    }

    void Update()
    {
        if (dynamicSkybox != null)
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            currentRotation %= 360; // Сохраняем значение в диапазоне 0-360
            dynamicSkybox.SetFloat("_Rotation", currentRotation);
        }
    }

   
}