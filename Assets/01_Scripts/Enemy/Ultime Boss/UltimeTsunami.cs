<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeTsunami : MonoBehaviour
{
    public float _speed, _timeToDestroy, _timeModifTsunamei, _timeUpTsunami, _timeDownTsunami;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;
    public bool _upTsunami, _downTsunami;
    public Transform _colliderTr, _particuleTr;


    private void Start()
    {
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
        Destroy(gameObject, _timeToDestroy);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);

            if (_upTsunami)
            {
                _timeModifTsunamei += Time.deltaTime;

                if (_timeModifTsunamei < _timeUpTsunami)
                {
                    Vector3 newVectorSize = new Vector3(_colliderTr.localScale.x * -Time.deltaTime * 0.25f, _colliderTr.localScale.y * Time.deltaTime * 0.5f, 0);
                    Vector3 newVectorPos = new Vector3(0, _colliderTr.localScale.y * Time.deltaTime, 0);
                    _colliderTr.localScale += newVectorSize;
                    _colliderTr.localPosition += newVectorPos;
                    _particuleTr.localScale += newVectorSize;
                    _particuleTr.localPosition += newVectorPos;
                }
                else
                {
                    _timeModifTsunamei = 0;
                    _upTsunami = false;
                    _downTsunami = true;
                }
            }
            if(_downTsunami)
            {
                _timeModifTsunamei += Time.deltaTime;

                if (_timeModifTsunamei < _timeDownTsunami)
                {
                    Vector3 newVectorSize = new Vector3(_colliderTr.localScale.x * -Time.deltaTime * 0.25f, _colliderTr.localScale.y * Time.deltaTime * 0.5f, 0);
                    Vector3 newVectorPos = new Vector3(0, _colliderTr.localScale.y * Time.deltaTime, 0);
                    _colliderTr.localScale -= newVectorSize;
                    _colliderTr.localPosition -= newVectorPos;
                    _particuleTr.localScale -= newVectorSize;
                    _particuleTr.localPosition -= newVectorPos;
                }
                else
                {
                    _timeModifTsunamei = 0;
                    _downTsunami = false;
                    Destroy(gameObject);
                }
            }

            _speed -= Time.deltaTime * 0.5f;
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

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

public class UltimeTsunami : MonoBehaviour
{
    public float _speed, _timeToDestroy, _timeModifTsunamei, _timeUpTsunami, _timeDownTsunami;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;
    public bool _upTsunami, _downTsunami;
    public Transform _colliderTr, _particuleTr;


    private void Start()
    {
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
        Destroy(gameObject, _timeToDestroy);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);

            if (_upTsunami)
            {
                _timeModifTsunamei += Time.deltaTime;

                if (_timeModifTsunamei < _timeUpTsunami)
                {
                    Vector3 newVectorSize = new Vector3(_colliderTr.localScale.x * -Time.deltaTime * 0.25f, _colliderTr.localScale.y * Time.deltaTime * 0.5f, 0);
                    Vector3 newVectorPos = new Vector3(0, _colliderTr.localScale.y * Time.deltaTime, 0);
                    _colliderTr.localScale += newVectorSize;
                    _colliderTr.localPosition += newVectorPos;
                    _particuleTr.localScale += newVectorSize;
                    _particuleTr.localPosition += newVectorPos;
                }
                else
                {
                    _timeModifTsunamei = 0;
                    _upTsunami = false;
                    _downTsunami = true;
                }
            }
            if(_downTsunami)
            {
                _timeModifTsunamei += Time.deltaTime;

                if (_timeModifTsunamei < _timeDownTsunami)
                {
                    Vector3 newVectorSize = new Vector3(_colliderTr.localScale.x * -Time.deltaTime * 0.25f, _colliderTr.localScale.y * Time.deltaTime * 0.5f, 0);
                    Vector3 newVectorPos = new Vector3(0, _colliderTr.localScale.y * Time.deltaTime, 0);
                    _colliderTr.localScale -= newVectorSize;
                    _colliderTr.localPosition -= newVectorPos;
                    _particuleTr.localScale -= newVectorSize;
                    _particuleTr.localPosition -= newVectorPos;
                }
                else
                {
                    _timeModifTsunamei = 0;
                    _downTsunami = false;
                    Destroy(gameObject);
                }
            }

            _speed -= Time.deltaTime * 0.5f;
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

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
