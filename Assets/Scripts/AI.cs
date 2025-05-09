using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Drawing;

public class AI : MonoBehaviour
{    
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Transform[] points;
    [SerializeField] Transform pukePoint;
    BaseCharacter character;

    [SerializeField] private float _distance;

    public bool _isOnPoint;
    private bool _isPuking = false;
    public bool IsPuking() => _isPuking;
    void Start()
    {        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<BaseCharacter>();
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
    public IEnumerator AIPuke()
    {
        _isPuking = true;      

        // Идем к точке рвоты
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
        animator.SetBool("puking", true);

        // Ждем завершения анимации рвоты
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        _isPuking = false;
        animator.SetBool("puking", false);       

        // Если все еще болен, повторяем процесс
        if (character.IsIll)
        {
            SetNavMesh(true);
            StartCoroutine(AIPuke());
        }
        else
        {
            // Если выздоровел, возвращаемся к обычному поведению
            SetNavMesh(true);
            ChangePointState(true);
        }
    }

    public void SetNavMesh(bool conclusion)
    {
        agent.enabled = conclusion;
    }    
}
