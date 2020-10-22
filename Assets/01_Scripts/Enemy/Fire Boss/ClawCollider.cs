using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCollider : MonoBehaviour
{
    [SerializeField] private FireBossEntity _waterBossEntity;
    [SerializeField] private bool _touchWeaponBeforePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.tag == "PlayerColl")
            {
                if (other.GetComponentInChildren<StateMachineAttack>().CurrentState != PlayerAttackState.PROTECTION)
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
}
