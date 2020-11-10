using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpellRockBall : MonoBehaviour
{
    [SerializeField] private float _speed, _damage, _coefTimeAddSize;
    [SerializeField] private bool _stopAddSize;
    public Transform _firePointLeft, _firePointRight;

    public float _timeSize, _timeSizeMax;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        _coefTimeAddSize = Random.Range(0.5f, 5f);
    }

    private void Update()
    {
        if (_timeSize < _timeSizeMax)
        {
            _timeSize += Time.deltaTime;
        }
        else
        {
            _stopAddSize = true;
        }

        if (!_stopAddSize)
        {
            transform.position = _firePointRight.position;
            transform.localScale += (Vector3.one * _coefTimeAddSize) * Time.deltaTime;
        }
        else
        {
            if (_speed != 0)
            {
                transform.position += transform.forward * (_speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("No Speed");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 8 && collision.gameObject.tag == "PlayerColl")
        //{
        //    collision.gameObject.GetComponentInChildren<PlayerEntity>().LessLife(_damage);
        //}

        //if (collision.gameObject.layer == 31)
        //{
        //    _speed = 0;
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);
        }

        if (other.gameObject.layer == 31)
        {
            _speed = 0;

            //if (_hitPrefab != null)
            //{
            //    var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

            //    var psHit = hitVFX.GetComponent<ParticleSystem>();
            //    if (psHit != null)
            //    {
            //        Destroy(hitVFX, psHit.main.duration);
            //    }
            //    else
            //    {
            //        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            //        Destroy(hitVFX, psChild.main.duration);
            //    }
            //}

            Destroy(gameObject);
        }
    }

    public void Init(Transform firePointLeft, Transform firePointRight)
    {
        _firePointLeft = firePointLeft;
        _firePointRight = firePointRight;
    }
}
