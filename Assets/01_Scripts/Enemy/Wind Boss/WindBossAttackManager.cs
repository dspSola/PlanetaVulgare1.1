using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBossAttackManager : MonoBehaviour
{
    [SerializeField] private WindBossAnimatorMono _windBossAnimatorMono;


    public void SetAttack(bool valueBool, int valueInt)
    {
        _windBossAnimatorMono.SetAttack(valueBool, valueInt);
    }
}
