using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRage01 : MonoBehaviour
{
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;
    [SerializeField] private PlayerEntity _playerEntity;

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

        Destroy(gameObject, 2);
    }

    private void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag == "Enemy")
        {
            // Boss
            if (other.gameObject.GetComponentInChildren<BossEntity>() != null)
            {
                other.gameObject.GetComponentInChildren<BossEntity>().LessLife(_playerEntity.Damage, _playerEntity);
            }
            // Simple Enemy
            else if (other.gameObject.GetComponentInChildren<EnemyEntity>() != null)
            {
                other.gameObject.GetComponentInChildren<EnemyEntity>().LessLife(_playerEntity.Damage);
            }
            // Boss
            if (other.gameObject.GetComponentInParent<BossEntity>() != null)
            {
                other.gameObject.GetComponentInParent<BossEntity>().LessLife(_playerEntity.Damage, _playerEntity);
            }
            // Simple Enemy
            else if (other.gameObject.GetComponentInParent<EnemyEntity>() != null)
            {
                other.gameObject.GetComponentInParent<EnemyEntity>().LessLife(_playerEntity.Damage);
            }

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
        }
    }
}
