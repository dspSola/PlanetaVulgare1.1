using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossBlenderControl : StateMachineBehaviour
{
    [SerializeField] private WaterBossAgentController _waterBossAgentController;
    [SerializeField] private WaterBossAnimatorMono _waterBossAnimatorMono;
    [SerializeField] private WaterBossSpellManager _waterBossSpellManager;
    [SerializeField] private TridantCollider _tridantCollider;
    [SerializeField] private bool _makePauseDestinationAttack, _makePoseRotationAttack, _canMakeRotationPauseAttack, _isAttack, _isSpell, _spellOnPlayer;
    [SerializeField] private string _spellHand;
    [SerializeField] private int _indexSpell, _cptNbSpell, _cptNbSpellSpawn;

    private Vector3 _posPlayerForSpell;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set Script
        if (_waterBossAgentController == null)
        {
            _waterBossAgentController = animator.GetComponentInChildren<WaterBossAgentController>();
        }
        if (_waterBossAnimatorMono == null)
        {
            _waterBossAnimatorMono = animator.GetComponentInChildren<WaterBossAnimatorMono>();
        }
        if(_tridantCollider == null)
        {
            _tridantCollider = animator.GetComponentInChildren<TridantCollider>();
        }
        // Set Modification 
        if (_waterBossSpellManager == null)
        {
            _waterBossSpellManager = animator.GetComponentInChildren<WaterBossSpellManager>();
        }
        if (_makePauseDestinationAttack)
        {
            _waterBossAgentController.MakePauseAttack = true;
        }
        if (_makePoseRotationAttack)
        {
            _waterBossAgentController.MakeRotationPauseAttack = true;
        }
        if (_isSpell)
        {
            if (_cptNbSpellSpawn != 0)
            {
                _cptNbSpellSpawn = 0;
            }
            if(_spellOnPlayer)
            {
                _posPlayerForSpell = _waterBossAgentController.PlayerTransform.position;
            }
        }
        if (_canMakeRotationPauseAttack)
        {
            _waterBossAgentController.CanMakeRotationPauseAttack = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_isAttack)
        {
            if(animator.GetFloat("Slice") > 0)
            {
                _tridantCollider.CanDamageCac = true;
            }
            else
            {
                _tridantCollider.CanDamageCac = false;
            }
        }

        if(_isSpell)
        {
            if(animator.GetFloat("CanLunchSpell") > 0 && _cptNbSpellSpawn < _cptNbSpell)
            {
                if (_spellOnPlayer)
                {
                    _waterBossSpellManager.SpawnSpell(_indexSpell, _posPlayerForSpell);
                }
                else
                {
                    _waterBossSpellManager.SpawnSpell(_indexSpell, _spellHand);
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
            _waterBossAgentController.MakePauseAttack = false;
        }
        if (_makePoseRotationAttack)
        {
            _waterBossAgentController.MakeRotationPauseAttack = false;
        }
        if (_isAttack)
        {
            _waterBossAnimatorMono.SetAttack(false, 0);
        }
        if (_isSpell)
        {
            _waterBossSpellManager.SetSpell(false, 0);
        }
        if(_canMakeRotationPauseAttack)
        {
            _waterBossAgentController.CanMakeRotationPauseAttack = false;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
