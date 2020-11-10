using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossBlenderControl : StateMachineBehaviour
{
    [SerializeField] private EarthBossAgentController _windBossAgentController;
    [SerializeField] private EarthBossAnimatorMono _windBossAnimatorMono;
    [SerializeField] private EarthBossAttackManager _earthBossAttackManager;
    [SerializeField] private EarthBossSpellManager _windBossSpellManager;
    [SerializeField] private bool _makePauseDestinationAttack, _makePoseRotationAttack, _canMakeRotationPauseAttack, _isAttack, _isSpell, _spellOnPlayer;
    [SerializeField] private string _spellHand;
    [SerializeField] private int _indexSpell, _cptNbSpell, _cptNbSpellSpawn;

    private Vector3 _posPlayerForSpell;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set Script
        if (_windBossAgentController == null)
        {
            _windBossAgentController = animator.GetComponentInChildren<EarthBossAgentController>();
        }
        if (_windBossAnimatorMono == null)
        {
            _windBossAnimatorMono = animator.GetComponentInChildren<EarthBossAnimatorMono>();
        }
        if (_earthBossAttackManager == null)
        {
            _earthBossAttackManager = animator.GetComponentInChildren<EarthBossAttackManager>();
        }
        if (_windBossSpellManager == null)
        {
            _windBossSpellManager = animator.GetComponentInChildren<EarthBossSpellManager>();
        }
        //if (_windBossHand == null)
        //{
        //    _windBossHand = animator.GetComponentInChildren<WindBossHand>();
        //}
        // Set Modification 
        if (_makePauseDestinationAttack)
        {
            _windBossAgentController.MakePauseAttack = true;
        }
        if (_makePoseRotationAttack)
        {
            _windBossAgentController.MakeRotationPauseAttack = true;
        }
        if (_isSpell)
        {
            if (_cptNbSpellSpawn != 0)
            {
                _cptNbSpellSpawn = 0;
            }
            if (_spellOnPlayer)
            {
                _posPlayerForSpell = _windBossAgentController.PlayerTransform.position;
            }
        }
        if (_canMakeRotationPauseAttack)
        {
            _windBossAgentController.CanMakeRotationPauseAttack = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isAttack)
        {
            if (animator.GetFloat("CanDamageCac") > 0)
            {
                _earthBossAttackManager.SetCanAttackToHand(true);
            }
            else
            {
                _earthBossAttackManager.SetCanAttackToHand(false);
            }
        }

        if (_isSpell)
        {
            if (animator.GetFloat("CanLunchSpell") > 0 && _cptNbSpellSpawn < _cptNbSpell)
            {
                if (_spellOnPlayer)
                {
                    _windBossSpellManager.SpawnSpell(_indexSpell, _posPlayerForSpell);
                }
                else
                {
                    _windBossSpellManager.SpawnSpell(_indexSpell, _spellHand);
                }
                _cptNbSpellSpawn++;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_makePauseDestinationAttack)
        {
            _windBossAgentController.MakePauseAttack = false;
        }
        if (_makePoseRotationAttack)
        {
            _windBossAgentController.MakeRotationPauseAttack = false;
        }
        if (_isAttack)
        {
            _windBossAnimatorMono.SetAttack(false, 0);
        }
        if (_isSpell)
        {
            _windBossSpellManager.SetSpell(false, 0);
        }
        if (_canMakeRotationPauseAttack)
        {
            _windBossAgentController.CanMakeRotationPauseAttack = false;
        }
    }
}
