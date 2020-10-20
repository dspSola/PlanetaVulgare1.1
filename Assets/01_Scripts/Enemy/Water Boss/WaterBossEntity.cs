using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossEntity : EnemyEntity
{
    [SerializeField] private WaterBossAgentController _waterBossAgentController;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _waterBossAgentController.Initialize();
    }
}
