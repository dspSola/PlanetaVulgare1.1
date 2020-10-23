using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBossHand : MonoBehaviour
{
    [SerializeField] private WindBossEntity _windBossEntity;
    [SerializeField] private WindBossAnimatorMono _windBossAnimatorMono;
    [SerializeField] private WaterBossEntity _waterBossEntity;
    [SerializeField] private bool _canDamageCac;

    public bool CanDamageCac { get => _canDamageCac; set => _canDamageCac = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.tag == "PlayerColl")
            {
                if (other.GetComponentInChildren<StateMachineAttack>().CurrentState != PlayerAttackState.PROTECTION && _canDamageCac)
                {
                    other.GetComponentInChildren<PlayerEntity>().LessLife(_windBossEntity.Damage);
                }
            }
        }
    }
}
