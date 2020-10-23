using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] EnemyEntityData _MushroomEntity;
    [SerializeField] EntityData _playerParameter;
    [SerializeField] Collider _collider;

    private void Start()
    {
        //_damage.value = false;
        _isDead = false;
    }

    private void Update()
    {
        _MushroomEntity.CurrentLife = Mathf.Clamp(_MushroomEntity.CurrentLife, _playerParameter.Damage, _MushroomEntity.LifeMax);

        if (_damage)
        {
            _MushroomEntity.CurrentLife -= _playerParameter.Damage;
        }
        else
        {
            _damage = false;
        }

        //if (_MushroomEntity.CurrentLife <= 0)
        //{
        //    _isDead = true;
        //}

        Debug.Log("life =" + _MushroomEntity.CurrentLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        //si la hache player entre en collision
        //il perd des points de vie
        if (other.gameObject.CompareTag("WeaponSliceableColl"))
        {
            //other.GetComponent<MushroomEntity>().LessLife(_MushroomEntity.CurrentLife);
            _damage = true;
        }
    }

    public bool _isDead;
    public bool _damage;

    public bool IsDead { get => _isDead; set => _isDead = value; }
}
