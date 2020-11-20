using UnityEngine;

public class AudioMontainDetect : MonoBehaviour
{
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
        Debug.Log("c'est l'éclate");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hou!!!");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player entre dans la zone montagne");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player sort dans la zone montagne");
        }
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
}
