using System.Collections;
using UnityEngine;

public class ParrotController : MonoBehaviour
{
    public Animator parrotAnimator;
    public float flySpeed = 5f;
    public Vector3 flyAwayOffset = new Vector3(-10, 5, 0);
    public AudioClip flySound;

    private Vector3 startPosition;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public void StartTalking()
    {
        parrotAnimator.SetBool("IsTalking", true);
    }

    public void FlyAway()
    {
        parrotAnimator.SetTrigger("FlyAway");
        audioSource.PlayOneShot(flySound);
        StartCoroutine(FlyAwayRoutine());
    }

    IEnumerator FlyAwayRoutine()
    {
        Vector3 targetPosition = startPosition + flyAwayOffset;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                flySpeed * Time.deltaTime
            );
            yield return null;
        }

        gameObject.SetActive(false);
    }
}