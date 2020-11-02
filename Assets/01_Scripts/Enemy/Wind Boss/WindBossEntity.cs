using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private WindBossAgentController _windBossAgentController;
    [SerializeField] private WindBossAnimatorMono _windBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _windBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);
        if (base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _windBossAnimatorMono.SetDeath();
            Destroy(gameObject, 3);
        }
    }
}
