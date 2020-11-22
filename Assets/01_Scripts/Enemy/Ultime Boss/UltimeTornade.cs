using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeTornade : MonoBehaviour
{
    public float _speedMove, _speedRotate;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public Transform _ultimeBossTransform;

    private void Start()
    {
        int randomDeletDestroy = Random.Range(15, 20);
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
        Destroy(gameObject, randomDeletDestroy);
    }

    private void Update()
    {
        if (_speedMove != 0)
        {
            transform.position += transform.right * (_speedMove * Time.deltaTime);
            if(_ultimeBossTransform != null)
            {
                transform.RotateAround(_ultimeBossTransform.position, Vector3.up, _speedRotate * Time.deltaTime);
            }
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
            DestroyIt();
        }

        if (other.gameObject.layer == 31)
        {
            DestroyIt();
        }
    }

    public void DestroyIt()
    {
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

    public void InitTornade(Transform ultimeBossTr)
    {
        _ultimeBossTransform = ultimeBossTr;
        //transform.localPosition = new Vector3(transform.localPosition.x, 1, transform.localPosition.z);
        //_speedMove = Random.Range(_speedMove - (_speedMove / 2), _speedMove + (_speedMove / 2));
        //_speedRotate = Random.Range(_speedRotate - (_speedRotate / 2), _speedRotate + (_speedRotate / 2));
        _speedMove = Random.Range(0.75f, 1.25f);
        _speedRotate = Random.Range(150, 300);
    }
}
