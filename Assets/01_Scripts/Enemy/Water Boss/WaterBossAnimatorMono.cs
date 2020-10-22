using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterBossAnimatorMono : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Update()
    {
        SetSpeed(_navMeshAgent.speed);
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat("Speed", value);
    }
    public void SetAttack(bool valueBool, int valueInt)
    {
        _animator.SetBool("Attack", valueBool);
        _animator.SetInteger("CptAttack", valueInt);
    }

    public void SetSpell(bool valueBool, int valueInt)
    {
        _animator.SetBool("Spell", valueBool);
        _animator.SetInteger("CptSpell", valueInt);
    }

    public void SetDeath(bool value)
    {
        _animator.SetBool("Death", value);
    }
}
