using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMutantEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        if (base.Life <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject, 3);
        }
    }
}
