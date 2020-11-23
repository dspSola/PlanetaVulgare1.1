using System.Collections;
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

            if (_isRisingDown || _isCatchingTotem.value)
            {
                DoVolumeDown();
            }
        }

        if(_isPickingTotem)
        {
            //Debug.Log("avant condition");
            if(_isCatchingTotem.value)
            {
                //Debug.Log("dans condition");
                DoVolumeUp();
            }
            if (_isDeadBossVariable.value)
            {
                _isCatchingTotem.value = false;
                DoVolumeDown();
            }
        }

        if(_isDeadBoss)
        {
            if (_isDeadBossVariable.value)
            {
                DoVolumeUp();
                _isStoppingTimer = Timer(_isStoppingTimer);
            }
            if(_isStoppingTimer)
            {
                Debug.Log("Hou!!!");
                _isDeadBossVariable.value = false;
                DoVolumeDown();
                if(_audioSource.volume <= 0)
                {
                    _isStoppingTimer = false;
                }
                //StartCoroutine(DoDelayInit());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
        float timerInterval = 30f;

        _time += Time.deltaTime;
        Debug.Log("Timer : " + _time);
        if(_time > timerInterval)
        {
            p_IsStopping = true;
            _time = 0;
        }

        return p_IsStopping;
    }

    //IEnumerator DoDelayInit()
    //{
    //    yield return new WaitForSeconds(Time.deltaTime * _speed);
    //    _isStoppingTimer = false;
    //}

    private AudioSource _audioSource;
    private BoxCollider _boxCollider;
    private bool _isRisingUp;
    private bool _isRisingDown;
    public bool _isStoppingTimer;
    private float _time = 0f;
}
