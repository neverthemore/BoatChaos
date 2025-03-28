using UnityEngine;

public class StepSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private float pitchVariation = 0.1f; // �������� ��������� �����

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        _audioSource.Play();
    }
}
