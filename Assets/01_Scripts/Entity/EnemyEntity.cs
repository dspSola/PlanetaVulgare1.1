using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;

    public float SpeedWalk { get => _speedWalk; set => _speedWalk = value; }
    public float SpeedRun { get => _speedRun; set => _speedRun = value; }

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        if(base.EntityData is EnemyEntityData enemyEntityData)
        {
            _speedWalk = enemyEntityData.SpeedWalk;
            _speedRun = enemyEntityData.SpeedRun;
        }
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);
    }
}
