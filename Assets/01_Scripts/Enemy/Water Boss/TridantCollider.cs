using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridantCollider : MonoBehaviour
{
    [SerializeField] private WaterBossEntity _waterBossEntity;
    [SerializeField] private bool _touchWeaponBeforePlayer, _canDamageCac;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.tag == "PlayerColl")
            {
                if (other.GetComponentInChildren<StateMachineAttack>().CurrentState != PlayerAttackState.PROTECTION && _canDamageCac)
                {
                    other.GetComponentInChildren<PlayerEntity>().LessLife(_waterBossEntity.Damage);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_touchWeaponBeforePlayer)
        {
            _touchWeaponBeforePlayer = false;
        }
    }

    public bool CanDamageCac { get => _canDamageCac; set => _canDamageCac = value; }
}
