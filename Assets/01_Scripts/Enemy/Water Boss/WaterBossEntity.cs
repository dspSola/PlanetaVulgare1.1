using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private WaterBossAgentController _waterBossAgentController;
    [SerializeField] private WaterBossAnimatorMono _waterBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _waterBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        if(base.Life <= 0)
        {
            _waterBossAnimatorMono.SetDeath(true);
            _waterBossAgentController.IsDeath = true;
        }
        Destroy(gameObject, 3);
    }
}
