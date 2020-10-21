using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] EnemyEntityData _enemyParameter;
    [SerializeField] IntVariable _MushroomCurrentLife;
    [SerializeField] BoolVariable _isTakedDamage;
    [SerializeField] EntityData _playerParameter;

    private void Start()
    {
        _MushroomCurrentLife.value = _enemyParameter.LifeMax;
        _isTakedDamage.value = false;
        _isDead = false;
    }

    private void Update()
    {
        if(_isTakedDamage.value)
        {
            _MushroomCurrentLife.value -= _playerParameter.Damage;
        }
        else
        {
            _isTakedDamage.value = false;
        }

        if(_MushroomCurrentLife.value <= 0)
        {
            _isDead = true;
        }
    }

    private bool _isDead;

    public bool IsDead { get => _isDead; set => _isDead = value; }
}
