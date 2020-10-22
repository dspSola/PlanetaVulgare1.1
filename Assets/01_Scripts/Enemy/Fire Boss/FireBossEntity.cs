using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private FireBossAgentController _fireBossAgentController;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _fireBossAgentController.Initialize(_playerData.Transform);
    }
}

