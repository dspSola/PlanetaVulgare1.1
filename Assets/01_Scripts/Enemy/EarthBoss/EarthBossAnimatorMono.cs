using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthBossAnimatorMono : MonoBehaviour
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
    public void SetInHearth(bool value)
    {
        _animator.SetBool("Fly", value);
    }
    public void SetAttack(bool boolValue, int valueInt)
    {
        _animator.SetBool("Attack", boolValue);
        _animator.SetInteger("CptAttack", valueInt);
    }
    public void SetAttack(int valueInt)
    {
        if (!_animator.GetBool("Attack"))
        {
            _animator.SetBool("Attack", true);
            _animator.SetInteger("CptAttack", valueInt);
        }
    }
    public void SetAttack()
    {
        if (!_animator.GetBool("Attack"))
        {
            _animator.SetBool("Attack", true);
            int random = Random.Range(1, 3);
            _animator.SetInteger("CptAttack", random);
        }
    }

    public void SetSpell(bool valueBool, int valueInt)
    {
        _animator.SetBool("Spell", valueBool);
        _animator.SetInteger("CptSpell", valueInt);
    }

    public void SetDeath()
    {
        _animator.SetBool("Death", true);
    }

    public bool GetAttack()
    {
        return _animator.GetBool("Attack");
    }
}
