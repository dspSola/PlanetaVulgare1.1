using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EarthBossAgentController _earthBossAgentController;
    [SerializeField] private EarthBossAnimatorMono _earthBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _earthBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);
        if (base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _earthBossAnimatorMono.SetDeath();
            PlayerEventStoryThis.WinBossEarth();
            Destroy(gameObject, 3);
        }
    }
}
