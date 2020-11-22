<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossAttackManager : MonoBehaviour
{
    [SerializeField] private UltimeBossAnimatorMono _earthBossAnimatorMono;
    [SerializeField] private float _maxDistanceToAttackGround, _maxDistanceToAttackHunderGround;
    [SerializeField] private bool _canAttack;

    [SerializeField] private UltimeBossHand[] _ultimeBossHands;

    private void Update()
    {
        if (_canAttack)
        {
            _earthBossAnimatorMono.SetAttack();
        }
    }

    public void InitHand()
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.InitializeHand();
        }
    }

    public void SetCanAttackToHand(bool value)
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.CanDamageCac = value;
        }
    }

    public void SetCanAttackToHand(int[] _index, bool value)
    {
        int i = 0;
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            if (_index[i] == 1)
            {
                ultimeBossHand.CanDamageCac = value;
            }
            i++;
        }
    }

    public void SetStartAttackToHand(int[] _index)
    {
        int i = 0;
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            if (_index[i] == 1)
            {
                ultimeBossHand.SetActiveCollider(true);
            }
            i++;
        }
    }

    public void SetEndCanAttackToHand()
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.SetActiveCollider(false);
            ultimeBossHand.TouchPlayer = false;
        }
    }

    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public float MaxDistanceToAttackGround { get => _maxDistanceToAttackGround; set => _maxDistanceToAttackGround = value; }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossAttackManager : MonoBehaviour
{
    [SerializeField] private UltimeBossAnimatorMono _earthBossAnimatorMono;
    [SerializeField] private float _maxDistanceToAttackGround, _maxDistanceToAttackHunderGround;
    [SerializeField] private bool _canAttack;

    [SerializeField] private UltimeBossHand[] _ultimeBossHands;

    private void Update()
    {
        if (_canAttack)
        {
            _earthBossAnimatorMono.SetAttack();
        }
    }

    public void InitHand()
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.InitializeHand();
        }
    }

    public void SetCanAttackToHand(bool value)
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.CanDamageCac = value;
        }
    }

    public void SetCanAttackToHand(int[] _index, bool value)
    {
        int i = 0;
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            if (_index[i] == 1)
            {
                ultimeBossHand.CanDamageCac = value;
            }
            i++;
        }
    }

    public void SetStartAttackToHand(int[] _index)
    {
        int i = 0;
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            if (_index[i] == 1)
            {
                ultimeBossHand.SetActiveCollider(true);
            }
            i++;
        }
    }

    public void SetEndCanAttackToHand()
    {
        foreach (UltimeBossHand ultimeBossHand in _ultimeBossHands)
        {
            ultimeBossHand.SetActiveCollider(false);
            ultimeBossHand.TouchPlayer = false;
        }
    }

    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public float MaxDistanceToAttackGround { get => _maxDistanceToAttackGround; set => _maxDistanceToAttackGround = value; }
}
>>>>>>> Master
