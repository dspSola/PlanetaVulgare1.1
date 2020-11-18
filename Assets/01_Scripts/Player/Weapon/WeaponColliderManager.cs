using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private PlayerEntity _playerEntity;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private List<AudioClip> _audioClipsImpact;
    [SerializeField] private AudioClip _audioChangeWeapon, _audioImpact;

    [SerializeField] private float _timeSond, _timeSoundMax;

    private void Update()
    {
        if(_timeSond > 0 && _timeSond < _timeSoundMax)
        {
            _timeSond += Time.deltaTime;
        }
        else
        {
            _timeSond = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_stateMachineAttack.CanSlice)
        {
            if (other.gameObject.layer == 9)
            {
                // Si Enemy
                if (other.gameObject.tag == "Enemy")
                {
                    // Boss
                    if (other.gameObject.GetComponentInChildren<BossEntity>() != null)
                    {
                        if (other.gameObject.GetComponentInChildren<BossEntity>().IfLifeNot0())
                        {
                            other.gameObject.GetComponentInChildren<BossEntity>().LessLife(_playerEntity.Damage, _playerEntity);
                            EndAttack();
                        }
                    }
                    // Simple Enemy
                    else if (other.gameObject.GetComponentInChildren<EnemyEntity>() != null)
                    {
                        if (other.gameObject.GetComponentInChildren<EnemyEntity>().IfLifeNot0())
                        {
                            other.gameObject.GetComponentInChildren<EnemyEntity>().LessLife(_playerEntity.Damage);
                            EndAttack();
                        }
                    }
                }

                // Si Racine
                if(other.gameObject.tag == "Racine")
                {
                    if(!other.gameObject.GetComponentInParent<RacineBehaviour>().PlayerTouchThisRacine)
                    {
                        other.gameObject.GetComponentInParent<RacineBehaviour>().Die();
                        EndAttack();
                    }
                }
            }

            if(other.gameObject.layer == 31)
            {
                _audioSource.PlayOneShot(_audioImpact); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void EndAttack()
    {
        // Add Rage
        _playerEntity.AddRage(_playerEntity.ValueRageAddAttack);
        // Son Impact Sur Enemy
        int random = Random.Range(0, 3);
        _audioSource.PlayOneShot(_audioClipsImpact[random]);
    }

    public void PlaySon(int i, float timeSoundMax)
    {
        if (_timeSond == 0)
        {
            _timeSoundMax = timeSoundMax;
            _audioSource.PlayOneShot(_audioClips[i]);
            _timeSond += Time.deltaTime;
        }
    }

    public void SetSonChangeWeapon()
    {
        if (_timeSond == 0)
        {
            _timeSoundMax = 0.35f;
            _audioSource.PlayOneShot(_audioChangeWeapon);
            _timeSond += Time.deltaTime;
        }
    }
}
