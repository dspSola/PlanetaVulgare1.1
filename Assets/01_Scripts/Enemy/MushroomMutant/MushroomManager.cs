using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] EnemyEntityData _MushroomEntity;
    [SerializeField] IntVariable _MushroomCurrentLife;
    [SerializeField] int _damage;
    [SerializeField] EntityData _playerParameter;

    private void Start()
    {
        _MushroomEntity.CurrentLife = _MushroomEntity.LifeMax;
        //_damage.value = false;
        _isDead = false;
    }

    private void Update()
    {
        _MushroomCurrentLife.value = Mathf.Clamp(_MushroomCurrentLife.value, _playerParameter.Damage, _MushroomEntity.LifeMax);

        //if(_damage.value)
        //{
        //    _MushroomCurrentLife.value -= _playerParameter.Damage;
        //}
        //else
        //{
        //    _damage.value = false;
        //}

        //if(_MushroomCurrentLife.value <= 0)
        //{
        //    _isDead = true;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //si la hache player entre en collision
        //il perd des points de vie
        if (other.gameObject.layer == 30 && other.gameObject.tag == "WeaponSliceableColl")
        {
            other.GetComponent<MushroomEntity>().LessLife(_damage);
        }
    }

    private bool _isDead;

    public bool IsDead { get => _isDead; set => _isDead = value; }
}
