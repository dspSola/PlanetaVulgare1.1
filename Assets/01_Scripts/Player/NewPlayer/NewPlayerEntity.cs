using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerEntity : Entity
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speedWalk, _speedRun, _speedTurn;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
        if(base.EntityData is NewPlayerEntityData nped)
        {
            nped.PlayerTransform = _playerTransform;
            _speedWalk = nped.SpeedWalk;
            _speedRun = nped.SpeedRun;
            _speedTurn = nped.SpeedTurn;
        }
    }

    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    public float SpeedWalk { get => _speedWalk; set => _speedWalk = value; }
    public float SpeedRun { get => _speedRun; set => _speedRun = value; }
    public float SpeedTurn { get => _speedTurn; set => _speedTurn = value; }
}
