using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamage : MonoBehaviour
{
    [SerializeField] private MushroomEntity _mushroomEntity;
    [SerializeField] private MushroomManager _mushroomManager;
    [SerializeField] private EntityData _playerEntityData;

    private void Awake()
    {
        //if(_mushroomEntity = null)
        //{
        //    _mushroomEntity = GetComponentInParent<MushroomEntity>();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //si la hache player entre en collision
        //il perd des points de vie
        if (other.gameObject.CompareTag("WeaponSliceableColl"))
        {
            _mushroomEntity.Life -= _playerEntityData.Damage;
        }
    }
}
