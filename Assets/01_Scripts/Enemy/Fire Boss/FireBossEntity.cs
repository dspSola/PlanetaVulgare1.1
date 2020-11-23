﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private FireBossAgentController _fireBossAgentController;
    [SerializeField] private FireBossAnimator _fireBossAnimator;
    [SerializeField] private BoolVariable _isDeadBoss;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _fireBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);
        if(base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _fireBossAnimator.SetDeath();
            PlayerEventStoryThis.WinBossFire();
            Destroy(gameObject, 3);
            _isDeadBoss.value = true;
        }
    }
}

