using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    CrewCharacter aiCharacter;
    NavMeshAgent agent;
    [SerializeField] Transform[] points;

    [SerializeField] private bool _isOnPoint;
    void Start()
    {
        aiCharacter = GetComponent<CrewCharacter>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(AIMoving());
    }

    private bool IsOnPoint(Transform point)
    {
        if 
        return 
    }

    IEnumerator AIMoving()
    {
        while (true)
        {
            Transform point = points[Random.Range(0, points.Length)];
            if (IsOnPoint(point))
            {
                agent.SetDestination(point.position);                
            }
            else
            {
                yield return new WaitUntil(() => );
            }            
        }    
}
