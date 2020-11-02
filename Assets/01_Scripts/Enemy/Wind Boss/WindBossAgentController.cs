using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WindBossAgentController : MonoBehaviour
{
    [SerializeField] private WindBossEntity _windBossEntity;
    [SerializeField] private WindBossAnimatorMono _windBossAnimatorMono;
    [SerializeField] private WindBossAttackManager _windBossAttackManager;
    [SerializeField] private WindBossSpellManager _windBossSpellManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _windBossTransform, _windBossTransformMesh, _playerTransform;

    [SerializeField] private float _speedCurrent, _speedMaxCurrent, _coefAcceleration, _coefDecceleration, _maxDistanceToAttackGround, _maxDistanceToAttackHigh, _distancePlayer;

    [SerializeField] private bool _makePauseDistance, _makePauseDestinationAttack, _makeRotationPauseAttack, _canMakeRotationPauseAttack, _canAttack, _isInPath, _path, _hasPath, _isStopped, _isDeath, _isFly;
    [SerializeField] private float _timeToFly, _timeToFlyMax, _highMax, _speedCoefHighUpDown, _randomTimeSpell, _randomTimeSpellMax;

    public void Initialize(Transform playerTr)
    {
        _speedMaxCurrent = _windBossEntity.SpeedWalk;
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

                if(_timeToFly < _timeToFlyMax)
                {
                    _timeToFly += Time.deltaTime;
                }
                else
                {
                    _isFly = !_isFly;
                    _windBossAnimatorMono.SetFly(_isFly);
                    _timeToFly = 0;
                }

                if(_isFly)
                {
                    if (_windBossTransformMesh.position.y < _windBossTransform.position.y + _highMax)
                    {
                        _windBossTransformMesh.position += Vector3.up * Time.deltaTime * _speedCoefHighUpDown;
                    }

                    if (_distancePlayer > _maxDistanceToAttackHigh)
                    {
                        if (_playerTransform.position != _navMeshAgent.destination)
                        {
                            _navMeshAgent.SetDestination(_playerTransform.position);
                        }
                    }
                    else
                    {
                        Vector3 dir = (_windBossTransform.position - _playerTransform.transform.position).normalized;
                        Vector3 dirDistance = dir * _distancePlayer;

                        if (dirDistance != _navMeshAgent.destination)
                        {
                            _navMeshAgent.SetDestination(dirDistance);
                        }
                    }

                    if(_randomTimeSpell < _randomTimeSpellMax)
                    {
                        _randomTimeSpell += Time.deltaTime;
                    }
                    else
                    {
                        int random = Random.Range(1, 4);
                        _windBossAnimatorMono.SetSpell(true, random);
                        _randomTimeSpell = 0;
                    }
                }
                else
                {
                    if (_windBossTransformMesh.position.y > _windBossTransform.position.y)
                    {
                        _windBossTransformMesh.position -= Vector3.up * Time.deltaTime * _speedCoefHighUpDown;
                    }

                    if (_distancePlayer > _maxDistanceToAttackGround)
                    {
                        if (_playerTransform.position != _navMeshAgent.destination)
                        {
                            _navMeshAgent.SetDestination(_playerTransform.position);
                        }
                    }
                    else
                    {
                        int random = Random.Range(1, 3);
                        _windBossAnimatorMono.SetAttack(true, random);
                    }
                }

                if (_navMeshAgent.hasPath)
                {

                    if (_navMeshAgent.speed != _speedMaxCurrent)
                    {
                        _navMeshAgent.speed = _speedMaxCurrent;
                    }
                    Walk();
                }

                if (_makePauseDestinationAttack)
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
    private void FixedUpdate()
    {

    }

    private void Rotate()
    {
        Vector3 relativePos = _playerTransform.position - _windBossTransform.position;
        // Annule la rotation y;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _windBossTransform.rotation = rotation;
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

    public void SetPlayerTransform(Transform value)
    {
        _playerTransform = value;
    }

    public bool MakePauseAttack { get => _makePauseDestinationAttack; set => _makePauseDestinationAttack = value; }
    public bool MakeRotationPauseAttack { get => _makeRotationPauseAttack; set => _makeRotationPauseAttack = value; }
    public float DistancePlayer { get => _distancePlayer; set => _distancePlayer = value; }
    public Transform WaterBossTransform { get => _windBossTransform; set => _windBossTransform = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public bool CanMakeRotationPauseAttack { get => _canMakeRotationPauseAttack; set => _canMakeRotationPauseAttack = value; }
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }
}
