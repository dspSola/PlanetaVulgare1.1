using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindBall : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab, _secondProjectile;

    public float _timeToSeparate, _timeToSeparateMax;

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

        _timeToSeparateMax = Random.Range(0.25f, 0.5f);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }

        if(_timeToSeparate < _timeToSeparateMax)
        {
            _timeToSeparate += Time.deltaTime;
        }
        else
        {
            Vector3 currentEulerAngles = transform.rotation.eulerAngles + new Vector3(0, 15, 0);
            Quaternion quaternion = new Quaternion();
            quaternion.eulerAngles = currentEulerAngles;
            GameObject secondProjectile01 = Instantiate(_secondProjectile, transform.position, quaternion);
            currentEulerAngles = transform.rotation.eulerAngles + new Vector3(0, -15, 0);
            quaternion = new Quaternion();
            quaternion.eulerAngles = currentEulerAngles;
            GameObject secondProjectile02 = Instantiate(_secondProjectile, transform.position, quaternion);
            Destroy(gameObject);
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
