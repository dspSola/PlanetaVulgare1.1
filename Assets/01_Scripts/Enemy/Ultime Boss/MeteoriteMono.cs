<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteMono : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToLunch, _timeToLunchMax;

    private void Start()
    {
        _timeToLunch = 0;
        _timeToLunchMax = Random.Range(0.1f, 0.5f);
        _speed = Random.Range(15f, 25f);

        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_timeToLunch < _timeToLunchMax)
        {
            _timeToLunch += Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);
        }

        if (other.gameObject.layer == 31)
        {
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteMono : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToLunch, _timeToLunchMax;

    private void Start()
    {
        _timeToLunch = 0;
        _timeToLunchMax = Random.Range(0.1f, 0.5f);
        _speed = Random.Range(15f, 25f);

        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_timeToLunch < _timeToLunchMax)
        {
            _timeToLunch += Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);
        }

        if (other.gameObject.layer == 31)
        {
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
>>>>>>> Master
