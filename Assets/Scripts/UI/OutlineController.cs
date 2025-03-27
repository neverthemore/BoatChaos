using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OutlineController : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineWidth = 0.05f;
    [SerializeField] private Color outlineColor = Color.yellow;

    private Renderer objectRenderer;
    private Material originalMaterial;
    private bool isOutlined = false;

    void Start()
    {
        outlineMaterial = GetComponent<Material>();
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        
        outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
    }

    // ��������� �������
    public void EnableOutline()
    {
        if (!isOutlined)
        {
            objectRenderer.material = outlineMaterial;
            isOutlined = true;
        }
    }

    // ���������� �������
    public void DisableOutline()
    {
        if (isOutlined)
        {
            objectRenderer.material = originalMaterial;
            isOutlined = false;
        }
    }

    // ���������� ���������� �������
    public void UpdateOutlineSettings(float width, Color color)
    {
        outlineWidth = width;
        outlineColor = color;

        if (isOutlined)
        {
            outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
        }
    }
}
