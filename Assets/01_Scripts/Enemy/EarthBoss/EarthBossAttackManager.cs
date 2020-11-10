using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossAttackManager : MonoBehaviour
{
    [SerializeField] private EarthBossAnimatorMono _earthBossAnimatorMono;
    [SerializeField] private float _maxDistanceToAttackGround, _maxDistanceToAttackHunderGround;
    [SerializeField] private bool _canAttack;

    [SerializeField] private EarthBossHand[] _earthBossHands;

    private void Update()
    {
        if (_canAttack)
        {
            _earthBossAnimatorMono.SetAttack();
        }
    }

    public void InitHand()
    {
        foreach(EarthBossHand earthBossHand in _earthBossHands)
        {
            earthBossHand.InitializeHand();
        }
    }

    public void SetCanAttackToHand(bool value)
    {
        foreach (EarthBossHand earthBossHand in _earthBossHands)
        {
            earthBossHand.CanDamageCac = value;
        }
    }

    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public float MaxDistanceToAttackGround { get => _maxDistanceToAttackGround; set => _maxDistanceToAttackGround = value; }
}
