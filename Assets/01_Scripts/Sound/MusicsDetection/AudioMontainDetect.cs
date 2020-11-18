using UnityEngine;

public class AudioMontainDetect : MonoBehaviour
{
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player entre dans la zone montagne");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player sort dans la zone montagne");
        }
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
}
