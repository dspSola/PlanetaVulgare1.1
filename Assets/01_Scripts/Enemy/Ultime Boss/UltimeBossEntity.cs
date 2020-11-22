<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private UltimeBossAgentController _ultimeBossAgentController;
    [SerializeField] private UltimeBossAnimatorMono _ultimeBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _ultimeBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);

        if (base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _ultimeBossAnimatorMono.SetDeath();
            PlayerEventStoryThis.WinBossWater();
            Destroy(gameObject, 3);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossEntity : BossEntity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private UltimeBossAgentController _ultimeBossAgentController;
    [SerializeField] private UltimeBossAnimatorMono _ultimeBossAnimatorMono;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        _ultimeBossAgentController.Initialize(_playerData.Transform);
    }

    public override void LessLife(float value, PlayerEntity pe)
    {
        base.LessLife(value);

        if (base.Life <= 0)
        {
            pe.LifeToLifeMax();
            _ultimeBossAnimatorMono.SetDeath();
            PlayerEventStoryThis.WinBossWater();
            Destroy(gameObject, 3);
        }
    }
}
>>>>>>> Master
