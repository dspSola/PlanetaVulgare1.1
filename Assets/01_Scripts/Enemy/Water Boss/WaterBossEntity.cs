using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private WaterBossAgentController _waterBossAgentController;
    [SerializeField] private WaterBossAnimatorMono _waterBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _waterBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);

        if (base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _waterBossAnimatorMono.SetDeath();
            PlayerEventStoryThis.WinBossWater();
            Destroy(gameObject, 3);
        }
    }
}
