using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NewPlayerEntityData : EntityData
{
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _speedWalk, _speedRun, _speedTurn, _jumpHeight;

    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public float SpeedWalk { get => _speedWalk; set => _speedWalk = value; }
    public float SpeedRun { get => _speedRun; set => _speedRun = value; }
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
    public float SpeedTurn { get => _speedTurn; set => _speedTurn = value; }
}
