using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBossEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private WindBossAgentController _windBossAgentController;
    [SerializeField] private WindBossAnimatorMono _windBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _windBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        if (base.Life <= 0)
        {
            _windBossAnimatorMono.SetDeath(true);
            _windBossAgentController.IsDeath = true;
            Destroy(gameObject, 3);
        }
    }
}
