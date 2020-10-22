using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossAttackManager : MonoBehaviour
{
    [SerializeField] private FireBossAnimator _fireBossAnimatorMono;

    public void SetAttack(bool valueBool, int valueInt)
    {
        _fireBossAnimatorMono.SetAttack(valueBool, valueInt);
    }
}
