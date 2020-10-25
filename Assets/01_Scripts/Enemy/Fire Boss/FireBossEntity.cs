﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private FireBossAgentController _fireBossAgentController;
    [SerializeField] private FireBossAnimator _fireBossAnimator;
    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _fireBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        if(base.Life <= 0)
        {
            _fireBossAnimator.SetDeath();
            Destroy(gameObject, 3);
        }
    }
}

