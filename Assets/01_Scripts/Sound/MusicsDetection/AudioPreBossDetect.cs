using UnityEngine;

public class AudioPreBossDetect : MonoBehaviour
{
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player entre dans la zone preBoss");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player sort dans la zone preBoss");
        }
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
}
