using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AI : MonoBehaviour
{
    CrewCharacter aiCharacter;
    NavMeshAgent agent;
    [SerializeField] GoalPoint[] points;

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
        Debug.Log("корутина началась");
        int randIndex = Random.Range(0, points.Length);
        Transform point = points[randIndex].transform;
        if (!_isOnPoint)
        {
            agent.SetDestination(point.position);
        }        
        else yield return new WaitForSeconds(1f);
    }
}
