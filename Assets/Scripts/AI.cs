using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Drawing;

public class AI : MonoBehaviour
{    
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Transform[] points;
    [SerializeField] Transform[] pukePoints;

    [SerializeField] private float _distance;

    public bool _isOnPoint;
    void Start()
    {        
        animator = GetComponent<Animator>();
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
        Transform point = points[randIndex];
        while (Vector3.Distance(transform.position, point.position) > 3f)
        {
            _distance = Vector3.Distance(transform.position, point.position);
            if (agent.enabled && agent.navMeshOwner != null) 
                agent.SetDestination(point.position);

            yield return new WaitForFixedUpdate();
            point = points[randIndex];
            animator.SetBool("walking", true);
        }
        
        animator.SetBool("walking", false);
        yield return new WaitForSeconds(4);        
        ChangePointState(true);                
    }
    private Transform findPukePoint()
    {
        int index = 0;
        float minDistanceToPukePoint = 1000f;
        for (int i = 0; i < pukePoints.Length; i++)
        {
            float distanceToPukePoint = Vector3.Distance(transform.position, pukePoints[i].position);
            if (distanceToPukePoint < minDistanceToPukePoint)
            {
                index = i;
                minDistanceToPukePoint = distanceToPukePoint;
            }
        }
        return pukePoints[index];
    }
    public IEnumerator AIPuke()
    {
        Transform pukePoint = findPukePoint();
        while (Vector3.Distance(transform.position, pukePoint.position) > 2f)
        {
            if (agent.enabled && agent.navMeshOwner != null)
                agent.SetDestination(pukePoint.position);
            yield return new WaitForFixedUpdate();
            animator.SetBool("walking", true);
        }

        animator.SetBool("walking", false);        
        SetNavMesh(false);
        transform.rotation = pukePoint.rotation;            
    }

    public void SetNavMesh(bool conclusion)
    {
        agent.enabled = conclusion;
    }    
}
