using UnityEngine;

public class MushroomEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
    }
}
