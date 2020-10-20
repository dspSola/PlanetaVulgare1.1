using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAttackManager : MonoBehaviour
{
    [SerializeField] private WaterBossAnimatorMono _waterBossAnimatorMono;

    public void SetAttack(bool valueBool, int valueInt)
    {
        _waterBossAnimatorMono.SetAttack(valueBool, valueInt);
    }
}
