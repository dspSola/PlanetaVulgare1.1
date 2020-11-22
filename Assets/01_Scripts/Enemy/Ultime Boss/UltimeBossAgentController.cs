<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UltimeBossAgentController : MonoBehaviour
{
    [SerializeField] private UltimeBossEntity _ultimeBossEntity;
    [SerializeField] private UltimeBossAnimatorMono _ultimeBossAnimatorMono;
    [SerializeField] private UltimeBossAttackManager _ultimeBossAttackManager;
    [SerializeField] private UltimeBossSpellManager _ultimeBossSpellManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _ultimeBossTransformParent, _ultimeBossTransform, _playerTransform;

    [SerializeField] private float _speedCurrent, _speedMaxCurrent, _coefAcceleration, _coefDecceleration, _distancePlayer, _distanceToRunMax;
    [SerializeField] private bool _makePauseDistance, _makePauseDestinationAttack, _makeRotationPauseAttack, _canMakeRotationPauseAttack, _canAttack, _isInPath, _isStopped, _path, _hasPath, _isDeath;

    public float _timeToTp, _timeToTpMax;
    public GameObject _fvxTpMuzzle;

    public void Initialize(Transform playerTr)
    {
        _speedMaxCurrent = _ultimeBossEntity.SpeedWalk;
        _navMeshAgent.speed = 0;
        _playerTransform = playerTr;
    }

    private void Update()
    {
        if (!_isDeath)
        {
            if (_playerTransform != null)
            {
                _distancePlayer = Vector3.Distance(_playerTransform.position, _navMeshAgent.transform.position);

                if(_distancePlayer > _distanceToRunMax)
                {
                    _speedMaxCurrent = _ultimeBossEntity.SpeedRun;
                }
                else
                {
                    _speedMaxCurrent = _ultimeBossEntity.SpeedWalk;
                }

                if (_distancePlayer > _ultimeBossAttackManager.MaxDistanceToAttackGround)
                {
                    _ultimeBossAttackManager.CanAttack = false;
                    _makePauseDistance = false;
                    if (_playerTransform.position != _navMeshAgent.destination)
                    {
                        _navMeshAgent.SetDestination(_playerTransform.position);
                    }
                    Walk();
                }
                else
                {
                    StopWalk();
                    _makePauseDistance = true;
                    if (!_makeRotationPauseAttack)
                    {
                        Rotate();
                    }
                    _ultimeBossAttackManager.CanAttack = true;
                }

                if(_distancePlayer < 1.5f)
                {
                    if (_timeToTp < _timeToTpMax)
                    {
                        _timeToTp += Time.deltaTime;
                    }
                    else
                    {
                        int randomChanceToTp = Random.Range(0, 100);

                        if(randomChanceToTp < 90 - (50 * _ultimeBossEntity.CoefLife))
                        {
                            TpBossAroundRandomPlayer();
                        }
                        _timeToTp = 0;
                        _timeToTpMax = Random.Range(2f, 4f);
                    }
                }

                if (_navMeshAgent.hasPath)
                {
                    if (_navMeshAgent.speed != _speedMaxCurrent)
                    {
                        _navMeshAgent.speed = _speedMaxCurrent;
                    }
                }

                if (_makePauseDestinationAttack || _makePauseDistance)
                {
                    if (!_navMeshAgent.isStopped)
                    {
                        _navMeshAgent.isStopped = true;
                    }
                }
                else
                {
                    if (_navMeshAgent.isStopped)
                    {
                        _navMeshAgent.isStopped = false;
                    }
                }

                if (_canMakeRotationPauseAttack)
                {
                    Rotate();
                }

                _isInPath = _navMeshAgent.isPathStale;
                _path = _navMeshAgent.pathPending;
                _hasPath = _navMeshAgent.hasPath;
                _isStopped = _navMeshAgent.isStopped;
            }
            else
            {
                _distancePlayer = 0;
            }
        }
    }

    private void Rotate()
    {
        Vector3 relativePos = _playerTransform.position - _ultimeBossTransform.position;
        // Annule la rotation y;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _ultimeBossTransform.rotation = rotation;
    }

    public void Walk()
    {
        if (_speedCurrent < _speedMaxCurrent)
        {
            _speedCurrent += Time.deltaTime * _coefAcceleration;
        }
        else
        {
            _speedCurrent = _speedMaxCurrent;
        }
        if (_navMeshAgent.speed != _speedCurrent)
        {
            _navMeshAgent.speed = _speedCurrent;
        }
    }

    public void StopWalk()
    {
        if (_speedCurrent > 0)
        {
            _speedCurrent -= Time.deltaTime * _coefDecceleration;
        }
        else
        {
            _speedCurrent = 0;
        }
        if (_navMeshAgent.speed != _speedCurrent)
        {
            _navMeshAgent.speed = _speedCurrent;
        }
    }

    public void TpBossAroundRandomPlayer()
    {
        if (_fvxTpMuzzle != null)
        {
            var muzzleVFX = Instantiate(_fvxTpMuzzle, transform.position, transform.rotation);
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
        _ultimeBossTransformParent.transform.position = _playerTransform.position + new Vector3(Random.Range(-7.5f, 7.5f), 0, Random.Range(-7.5f, 7.5f));
    }

    public void SetPlayerTransform(Transform value)
    {
        _playerTransform = value;
    }

    public bool MakePauseAttack { get => _makePauseDestinationAttack; set => _makePauseDestinationAttack = value; }
    public bool MakeRotationPauseAttack { get => _makeRotationPauseAttack; set => _makeRotationPauseAttack = value; }
    public float DistancePlayer { get => _distancePlayer; set => _distancePlayer = value; }
    public Transform WaterBossTransform { get => _ultimeBossTransform; set => _ultimeBossTransform = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public bool CanMakeRotationPauseAttack { get => _canMakeRotationPauseAttack; set => _canMakeRotationPauseAttack = value; }
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UltimeBossAgentController : MonoBehaviour
{
    [SerializeField] private UltimeBossEntity _ultimeBossEntity;
    [SerializeField] private UltimeBossAnimatorMono _ultimeBossAnimatorMono;
    [SerializeField] private UltimeBossAttackManager _ultimeBossAttackManager;
    [SerializeField] private UltimeBossSpellManager _ultimeBossSpellManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _ultimeBossTransformParent, _ultimeBossTransform, _playerTransform;

    [SerializeField] private float _speedCurrent, _speedMaxCurrent, _coefAcceleration, _coefDecceleration, _distancePlayer, _distanceToRunMax;
    [SerializeField] private bool _makePauseDistance, _makePauseDestinationAttack, _makeRotationPauseAttack, _canMakeRotationPauseAttack, _canAttack, _isInPath, _isStopped, _path, _hasPath, _isDeath;

    public float _timeToTp, _timeToTpMax;
    public GameObject _fvxTpMuzzle;

    public void Initialize(Transform playerTr)
    {
        _speedMaxCurrent = _ultimeBossEntity.SpeedWalk;
        _navMeshAgent.speed = 0;
        _playerTransform = playerTr;
    }

    private void Update()
    {
        if (!_isDeath)
        {
            if (_playerTransform != null)
            {
                _distancePlayer = Vector3.Distance(_playerTransform.position, _navMeshAgent.transform.position);

                if(_distancePlayer > _distanceToRunMax)
                {
                    _speedMaxCurrent = _ultimeBossEntity.SpeedRun;
                }
                else
                {
                    _speedMaxCurrent = _ultimeBossEntity.SpeedWalk;
                }

                if (_distancePlayer > _ultimeBossAttackManager.MaxDistanceToAttackGround)
                {
                    _ultimeBossAttackManager.CanAttack = false;
                    _makePauseDistance = false;
                    if (_playerTransform.position != _navMeshAgent.destination)
                    {
                        _navMeshAgent.SetDestination(_playerTransform.position);
                    }
                    Walk();
                }
                else
                {
                    StopWalk();
                    _makePauseDistance = true;
                    if (!_makeRotationPauseAttack)
                    {
                        Rotate();
                    }
                    _ultimeBossAttackManager.CanAttack = true;
                }

                if(_distancePlayer < 1.5f)
                {
                    if (_timeToTp < _timeToTpMax)
                    {
                        _timeToTp += Time.deltaTime;
                    }
                    else
                    {
                        int randomChanceToTp = Random.Range(0, 100);

                        if(randomChanceToTp < 90 - (50 * _ultimeBossEntity.CoefLife))
                        {
                            TpBossAroundRandomPlayer();
                        }
                        _timeToTp = 0;
                        _timeToTpMax = Random.Range(2f, 4f);
                    }
                }

                if (_navMeshAgent.hasPath)
                {
                    if (_navMeshAgent.speed != _speedMaxCurrent)
                    {
                        _navMeshAgent.speed = _speedMaxCurrent;
                    }
                }

                if (_makePauseDestinationAttack || _makePauseDistance)
                {
                    if (!_navMeshAgent.isStopped)
                    {
                        _navMeshAgent.isStopped = true;
                    }
                }
                else
                {
                    if (_navMeshAgent.isStopped)
                    {
                        _navMeshAgent.isStopped = false;
                    }
                }

                if (_canMakeRotationPauseAttack)
                {
                    Rotate();
                }

                _isInPath = _navMeshAgent.isPathStale;
                _path = _navMeshAgent.pathPending;
                _hasPath = _navMeshAgent.hasPath;
                _isStopped = _navMeshAgent.isStopped;
            }
            else
            {
                _distancePlayer = 0;
            }
        }
    }

    private void Rotate()
    {
        Vector3 relativePos = _playerTransform.position - _ultimeBossTransform.position;
        // Annule la rotation y;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _ultimeBossTransform.rotation = rotation;
    }

    public void Walk()
    {
        if (_speedCurrent < _speedMaxCurrent)
        {
            _speedCurrent += Time.deltaTime * _coefAcceleration;
        }
        else
        {
            _speedCurrent = _speedMaxCurrent;
        }
        if (_navMeshAgent.speed != _speedCurrent)
        {
            _navMeshAgent.speed = _speedCurrent;
        }
    }

    public void StopWalk()
    {
        if (_speedCurrent > 0)
        {
            _speedCurrent -= Time.deltaTime * _coefDecceleration;
        }
        else
        {
            _speedCurrent = 0;
        }
        if (_navMeshAgent.speed != _speedCurrent)
        {
            _navMeshAgent.speed = _speedCurrent;
        }
    }

    public void TpBossAroundRandomPlayer()
    {
        if (_fvxTpMuzzle != null)
        {
            var muzzleVFX = Instantiate(_fvxTpMuzzle, transform.position, transform.rotation);
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
        _ultimeBossTransformParent.transform.position = _playerTransform.position + new Vector3(Random.Range(-7.5f, 7.5f), 0, Random.Range(-7.5f, 7.5f));
    }

    public void SetPlayerTransform(Transform value)
    {
        _playerTransform = value;
    }

    public bool MakePauseAttack { get => _makePauseDestinationAttack; set => _makePauseDestinationAttack = value; }
    public bool MakeRotationPauseAttack { get => _makeRotationPauseAttack; set => _makeRotationPauseAttack = value; }
    public float DistancePlayer { get => _distancePlayer; set => _distancePlayer = value; }
    public Transform WaterBossTransform { get => _ultimeBossTransform; set => _ultimeBossTransform = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public bool CanMakeRotationPauseAttack { get => _canMakeRotationPauseAttack; set => _canMakeRotationPauseAttack = value; }
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }
}
>>>>>>> Master
