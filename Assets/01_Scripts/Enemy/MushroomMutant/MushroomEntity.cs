using UnityEngine;

public class MushroomEntity : EnemyEntity
{
    [SerializeField] private PlayerData _playerData;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        if(base.Life <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject, 3);
        }
    }
}
