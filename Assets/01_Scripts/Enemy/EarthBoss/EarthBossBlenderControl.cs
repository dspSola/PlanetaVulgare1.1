using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossBlenderControl : StateMachineBehaviour
{
    [SerializeField] private EarthBossAgentController _bossAgentController;
    [SerializeField] private EarthBossAnimatorMono _bossAnimatorMono;
    [SerializeField] private EarthBossAttackManager _bossAttackManager;
    [SerializeField] private EarthBossSpellManager _bossSpellManager;
    [SerializeField] private bool _makePauseDestinationAttack, _makePoseRotationAttack, _canMakeRotationPauseAttack, _isAttack, _isSpell, _spellOnPlayer;
    [SerializeField] private string _spellHand;
    [SerializeField] private int _indexSpell, _cptNbSpell, _cptNbSpellSpawn;
    [SerializeField] private int[] _indexCollider;

    private Vector3 _posPlayerForSpell;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set Script
        if (_bossAgentController == null)
        {
            _bossAgentController = animator.GetComponentInChildren<EarthBossAgentController>();
        }
        if (_bossAnimatorMono == null)
        {
            _bossAnimatorMono = animator.GetComponentInChildren<EarthBossAnimatorMono>();
        }
        if (_bossAttackManager == null)
        {
            _bossAttackManager = animator.GetComponentInChildren<EarthBossAttackManager>();
        }
        if (_bossSpellManager == null)
        {
            _bossSpellManager = animator.GetComponentInChildren<EarthBossSpellManager>();
        }
        // Set Modification 
        if (_makePauseDestinationAttack)
        {
            _bossAgentController.MakePauseAttack = true;
        }
        if (_makePoseRotationAttack)
        {
            _bossAgentController.MakeRotationPauseAttack = true;
        }

        if(_isAttack)
        {
            _bossAttackManager.SetStartAttackToHand(_indexCollider);
        }

        if (_isSpell)
        {
            _bossSpellManager.IsInSpell = true;
            if (_cptNbSpellSpawn != 0)
            {
                _cptNbSpellSpawn = 0;
            }
            if (_spellOnPlayer)
            {
                _posPlayerForSpell = _bossAgentController.PlayerTransform.position;
                _bossSpellManager.SpawnSpell(_indexSpell, _posPlayerForSpell);
            }
            else
            {
                _bossSpellManager.SpawnSpell(_indexSpell, _spellHand);
            }
        }
        if (_canMakeRotationPauseAttack)
        {
            _bossAgentController.CanMakeRotationPauseAttack = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isAttack)
        {
            if (animator.GetFloat("CanDamageCac") > 0)
            {
                _bossAttackManager.SetCanAttackToHand(_indexCollider, true);
            }
            else
            {
                _bossAttackManager.SetCanAttackToHand(_indexCollider, false);
            }
        }

        if (_isSpell)
        {
            //if (animator.GetFloat("CanLunchSpell") > 0 && _cptNbSpellSpawn < _cptNbSpell)
            //{
            //    if (_spellOnPlayer)
            //    {
            //        _bossSpellManager.SpawnSpell(_indexSpell, _posPlayerForSpell);
            //    }
            //    else
            //    {
            //        _bossSpellManager.SpawnSpell(_indexSpell, _spellHand);
            //    }
            //    _cptNbSpellSpawn++;
            //}
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_makePauseDestinationAttack)
        {
            _bossAgentController.MakePauseAttack = false;
        }
        if (_makePoseRotationAttack)
        {
            _bossAgentController.MakeRotationPauseAttack = false;
        }
        if (_isAttack)
        {
            _bossAnimatorMono.SetAttack(false, 0);
            _bossAttackManager.SetEndCanAttackToHand();
        }
        if (_isSpell)
        {
            _bossSpellManager.SetSpell(false, 0);
        }
        if (_canMakeRotationPauseAttack)
        {
            _bossAgentController.CanMakeRotationPauseAttack = false;
        }
    }
}
