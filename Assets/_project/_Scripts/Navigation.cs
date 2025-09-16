using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class Navigation : MonoBehaviour
{
    private NavMeshSurface surface;
    [SerializeField] private float _updateInterval;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        InvokeRepeating(nameof(UpdateNavMesh), 0, _updateInterval);
    }
    void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }
}
