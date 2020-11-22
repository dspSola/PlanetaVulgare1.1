using UnityEngine;

public class AudioTriggerDetect : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(_isRisingUp)
        {
            DoVolumeUp();
        }

        if(_isRisingDown)
        {
            DoVolumeDown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hou!!!");
        if(other.gameObject.CompareTag("PlayerColl"))
        {
            //Debug.Log("player entre dans une zone");
            _isRisingUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerColl"))
        {
            //Debug.Log("player sort d'une zone");
            _isRisingDown = true;
        }
    }

    private void DoVolumeUp()
    {
        if(_audioSource.volume < 1)
        {
            _audioSource.volume += Time.deltaTime * _speed;
        }
        else 
        {
            _audioSource.volume = 1;
            _isRisingUp = false;
        }
    }

    private void DoVolumeDown()
    {
        if (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime * _speed ;
        }
        else
        {
            _audioSource.volume = 0;
            _isRisingDown = false;
        }
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
    private bool _isRisingUp;
    private bool _isRisingDown;
}
