using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity : EnemyEntity
{
    [SerializeField] private PlayerEventStory _playerEventStory;

    public override void InitializeEntity()
    {
        base.InitializeEntity();

        if(_playerEventStory.CptBossWin > 0)
        {
            LifeMax += _playerEventStory.CptBossWin * 20;
            Life = LifeMax;
        }
    }

    public PlayerEventStory PlayerEventStoryThis { get => _playerEventStory; set => _playerEventStory = value; }
}
