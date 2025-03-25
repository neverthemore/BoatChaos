using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AI : MonoBehaviour
{
    CrewCharacter aiCharacter;
    NavMeshAgent agent;
    [SerializeField] GoalPoint[] points;

    [SerializeField] private float _distance;

    public bool _isOnPoint;
    void Start()
    {
        aiCharacter = GetComponent<CrewCharacter>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(AIMoving());
    }
    public void ChangePointState(bool state)
    {
        _isOnPoint = state;
    }
    public IEnumerator AIMoving()
    {        
        int randIndex = Random.Range(0, points.Length);
        Transform point = points[randIndex].transform;
        while (Vector3.Distance(transform.position, point.position) > 3f)
        {
            _distance = Vector3.Distance(transform.position, point.position);
            agent.SetDestination(point.position);
            yield return new WaitForFixedUpdate();
            point = points[randIndex].transform;
        }
              
        yield return new WaitForSeconds(4);
        ChangePointState(true);
                
    }

    public void SetNavMesh(bool conclusion)
    {
        agent.enabled = conclusion;
    }
}
