using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridantCollider : MonoBehaviour
{
    [SerializeField] private WaterBossEntity _waterBossEntity;
    [SerializeField] private bool _touchWeaponBeforePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //_touchWeaponBeforePlayer = false;
            //if (other.gameObject.tag == "WeaponColl")
            //{
            //    Debug.Log("Touch Weapon : " + other.gameObject.name + " / " + other.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponentInChildren<StateMachineAttack>().CurrentState);
            //    if (other.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponentInChildren<StateMachineAttack>().CurrentState == PlayerAttackState.PROTECTION)
            //    {
            //        _touchWeaponBeforePlayer = true;
            //    }
            //}

            //if (other.gameObject.tag == "PlayerColl")
            //{
            //    Debug.Log("Touch Player : " + other.gameObject.name + " / _touchWeaponBeforePlayer : " + _touchWeaponBeforePlayer + " / CurrentState : " + other.GetComponentInChildren<StateMachineAttack>().CurrentState);
            //    if (!_touchWeaponBeforePlayer)
            //    {
            //        other.GetComponentInChildren<PlayerEntity>().LessLife(_waterBossEntity.Damage);
            //    }
            //}

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
