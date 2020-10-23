using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private Entity _playerEntity;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private List<AudioClip> _audioClipsImpact;
    [SerializeField] private AudioClip _audioChangeWeapon;

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
                if(other.gameObject.GetComponentInChildren<Entity>() != null)
                {
                    other.gameObject.GetComponentInChildren<Entity>().LessLife(_playerEntity.Damage);
                }
                if (other.gameObject.GetComponentInParent<Entity>() != null)
                {
                    other.gameObject.GetComponentInParent<Entity>().LessLife(_playerEntity.Damage);
                }
                int random = Random.Range(0, 3);
                _audioSource.PlayOneShot(_audioClipsImpact[random]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
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
