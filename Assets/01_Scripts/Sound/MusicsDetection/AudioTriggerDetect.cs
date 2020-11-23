using UnityEngine;

public class AudioTriggerDetect : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] BoolVariable _isCatchingTotem;
    [SerializeField] BoolVariable _isDeadBossVariable;

    [Header("Parameter")]
    [SerializeField] float _speed;

    [Header("StateSelect")]
    [SerializeField] bool _isTrigger;
    [SerializeField] bool _isTriggerBossFinal;
    [SerializeField] bool _isPickingTotem;
    [SerializeField] bool _isDeadBoss;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(_isTrigger)
        {
            if (_isRisingUp)
            {
                DoVolumeUp();
            }

            if (_isRisingDown)
            {
                DoVolumeDown();
            }
        }

        if(_isPickingTotem)
        {
            if(_isCatchingTotem.value)
            {
                DoVolumeUp();
            }
            if (_isDeadBossVariable.value)
            {
                DoVolumeDown();
            }
        }

        if(_isDeadBoss)
        {
            if (_isDeadBossVariable.value)
            {
                DoVolumeUp();
                Timer(_isStoppingTimer);
            }
            if(_isStoppingTimer)
            {
                DoVolumeDown();
            }
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

    private bool Timer(bool p_IsStopping)
    {
        float time = 0f;
        float timerInterval = 30f;

        time += Time.deltaTime;
        Debug.Log("Timer : " + time);
        if(time > timerInterval)
        {
            p_IsStopping = true;
            time = 0;
        }

        return p_IsStopping;
    }

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
    private bool _isRisingUp;
    private bool _isRisingDown;
    private bool _isStoppingTimer;
}
