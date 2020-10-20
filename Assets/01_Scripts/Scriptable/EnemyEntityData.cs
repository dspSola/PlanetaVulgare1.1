using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyEntityData : EntityData
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;

    public float SpeedWalk { get => _speedWalk; set => _speedWalk = value; }
    public float SpeedRun { get => _speedRun; set => _speedRun = value; }
}
