using UnityEngine;

public class AudioPlainDetect : MonoBehaviour
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
            Debug.Log("player entre dans la zone plaine");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player sort dans la zone plaine");
        }
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
}
