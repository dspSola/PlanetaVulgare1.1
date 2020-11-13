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

    public void SetCanAttackToHand(int[] _index, bool value)
    {
        int i = 0;
        foreach (EarthBossHand earthBossHand in _earthBossHands)
        {
            if (_index[i] == 1)
            {
                earthBossHand.CanDamageCac = value;
            }
            i++;
        }
    }

    public void SetStartAttackToHand(int[] _index)
    {
        int i = 0;
        foreach (EarthBossHand earthBossHand in _earthBossHands)
        {
            if (_index[i] == 1)
            {
                earthBossHand.SetActiveCollider(true);
            }
            i++;
        }
    }

    public void SetEndCanAttackToHand()
    {
        foreach (EarthBossHand earthBossHand in _earthBossHands)
        {
            earthBossHand.SetActiveCollider(false);
        }
    }

    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public float MaxDistanceToAttackGround { get => _maxDistanceToAttackGround; set => _maxDistanceToAttackGround = value; }
}
