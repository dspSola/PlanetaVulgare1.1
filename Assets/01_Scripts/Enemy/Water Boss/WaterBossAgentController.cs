using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterBossAgentController : MonoBehaviour
{
    [SerializeField] private WaterBossEntity _waterBossEntity;
    [SerializeField] private WaterBossAnimatorMono _waterBossAnimatorMono;
    [SerializeField] private WaterBossAttackManager _waterBossAttackManager;
    [SerializeField] private WaterBossSpellManager _waterBossSpellManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _waterBossTransform, _playerTransform;

    [SerializeField] private float _speedCurrent, _speedMaxCurrent, _coefAcceleration, _coefDecceleration, _maxDistanceToAttack, _distancePlayer;

    [SerializeField] private bool _makePauseDistance, _makePauseDestinationAttack, _makeRotationPauseAttack, _canMakeRotationPauseAttack, _canAttack, _isInPath, _path, _hasPath, _isDeath;

    public void Initialize(Transform playerTr)
    {
        _speedMaxCurrent = _waterBossEntity.SpeedWalk;
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
                if (_distancePlayer > _maxDistanceToAttack && !_makePauseDestinationAttack)
                {
                    if (_makePauseDistance)
                    {
                        _makePauseDistance = false;
                    }
                    if (_navMeshAgent.isStopped)
                    {
                        _navMeshAgent.isStopped = false;
                    }
                    if (_playerTransform.position != _navMeshAgent.destination)
                    {
                        _navMeshAgent.SetDestination(_playerTransform.position);
                    }
                    //_navMeshAgent.SetDestination(_playerTransform.position);
                    if (_navMeshAgent.speed != _speedMaxCurrent)
                    {
                        _navMeshAgent.speed = _speedMaxCurrent;
                    }
                    Walk();
                }
                else if (_distancePlayer < _maxDistanceToAttack)
                {
                    if (!_makeRotationPauseAttack)
                    {
                        Rotate();
                    }
                    _makePauseDistance = true;
                    _navMeshAgent.speed = 0;
                    _canAttack = true;
                    //if (_distancePlayer < _maxDistanceMove)
                    //{
                    //    _navMeshAgent.speed = 0;
                    //    Vector3 relativePos = _playerTransform.position - _waterBossTransform.position;
                    //    Debug.Log(relativePos);
                    //    if (relativePos.x == 0)
                    //    {
                    //        _canAttack = true;
                    //    }
                    //    _canAttack = true;
                    //}
                }

                if (_canMakeRotationPauseAttack)
                {
                    Rotate();
                }

                if (_makePauseDistance || _makePauseDestinationAttack || !_navMeshAgent.hasPath)
                {
                    _navMeshAgent.isStopped = true;
                    StopWalk();
                    //_navMeshAgent.speed = 0;
                    if (_canAttack)
                    {
                        _waterBossAttackManager.SetAttack(true, 1);
                        _canAttack = false;
                    }
                }

                _isInPath = _navMeshAgent.isPathStale;
                _path = _navMeshAgent.pathPending;
                _hasPath = _navMeshAgent.hasPath;
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
        Vector3 relativePos = _playerTransform.position - _waterBossTransform.position;
        // Annule la rotation y;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _waterBossTransform.rotation = rotation;
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
    public Transform WaterBossTransform { get => _waterBossTransform; set => _waterBossTransform = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public bool CanMakeRotationPauseAttack { get => _canMakeRotationPauseAttack; set => _canMakeRotationPauseAttack = value; }
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }
}
