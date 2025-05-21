using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class CinematicCameraDolly : MonoBehaviour
{
    public CinemachineCamera _camera;
    public SplineContainer splineContainer;
    [SerializeField] private DialogueSystem _dialogueSystem;
    public float duration = 5f;

    private CinemachineSplineDolly dolly;
    private float timer;

    private bool needCamera = false;

    void Start()
    {
        dolly = _camera.GetComponent<CinemachineSplineDolly>();
        SwitchCameraActive(true);
    }

    void Update()
    {
        if (!needCamera) return;
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            dolly.CameraPosition = Mathf.Lerp(0f, 1f, t);
        }
        else
        {
            SwitchCameraActive(false);
            _dialogueSystem.StartDialogue();
        }
        
    }

    void SwitchCameraActive(bool active)
    {
        int priority = active ? 30 : -10;
        _camera.Priority = priority;

        if (active)
        {
            timer = 0f;
            dolly.Spline = splineContainer;
            dolly.PositionUnits = PathIndexUnit.Normalized;
            dolly.CameraPosition = 0f;
        }

        needCamera = active;
    }
}
