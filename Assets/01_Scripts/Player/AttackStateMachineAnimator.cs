using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AttackStateMachineAnimator : StateMachineBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private GetInputBrute _getInputBrute;
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private StateMachineVertical _stateMachineVertical;
    [SerializeField] private StateMachineHorizontal _stateMachineHorizontal;
    [SerializeField] private WeaponColliderManager _weaponColliderManager;
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private bool _canSlice, _activeInAnimOnStart, _activeInAnimOnExit;

    [SerializeField] private bool _applyForceEnter, _applyForceExit, _applyForceValueEnter, _applyForceValueUpdate, _applyForceValueExit, _canRotatePlayer;

    [SerializeField] private bool _doJump, _doJumpExit, _canJumpExit, _doJumpExit2;

    [SerializeField] private bool _dodgeToAttack01, _canDodgeToAttack01;

    [SerializeField] private bool _endCombo, _modifieDegat, _playSondEnter, _playSondUpdate, _addModfieDamage, _lessModifieDamage;
    [SerializeField] private int _intSond;
    [SerializeField] private float _timeSoundMax, _damageModifie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Attache Script
        if (_playerEntity == null)
        {
            _playerEntity = animator.transform.parent.GetComponentInChildren<PlayerEntity>();
        }
        if (_getInputBrute == null)
        {
            _getInputBrute = animator.transform.parent.GetComponentInChildren<GetInputBrute>();
        }
        if (_stateMachineAttack == null)
        {
            _stateMachineAttack = animator.transform.parent.GetComponentInChildren<StateMachineAttack>();
        }
        if (_stateMachineVertical == null)
        {
            _stateMachineVertical = animator.transform.parent.GetComponentInChildren<StateMachineVertical>();
        }
        if (_stateMachineHorizontal == null)
        {
            _stateMachineHorizontal = animator.transform.parent.GetComponentInChildren<StateMachineHorizontal>();
        }
        if (_playerMove == null)
        {
            _playerMove = animator.transform.parent.GetComponentInChildren<PlayerMove>();
        }
        if (_weaponColliderManager == null)
        {
            _weaponColliderManager = animator.transform.parent.GetComponentInChildren<WeaponColliderManager>();
        }

        // Parametre
        if (_activeInAnimOnStart)
        {
            _stateMachineAttack.IsAnim = true;
        }
        if(_applyForceEnter)
        {
            _playerMove.CanApplyForceAnimation = true;
        }
        
        if(_canDodgeToAttack01)
        {
            _activeInAnimOnExit = true;
            _canDodgeToAttack01 = false;
        }
        if(_doJumpExit && _canJumpExit)
        {
            _canJumpExit = false;
        }

        if(_canRotatePlayer)
        {
            _playerMove.CanRotatePlayer();
        }

        if(_modifieDegat)
        {
            if (_addModfieDamage)
            {
                _addModfieDamage = false;
            }

            if (_lessModifieDamage)
            {
                _lessModifieDamage = false;
            }
            if(_damageModifie != 0)
            {
                _damageModifie = 0;
            }
        }

        if (_playSondEnter)
        {
            _weaponColliderManager.PlaySon(_intSond, _timeSoundMax);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("Slice") > 0)
        {
            if (_canSlice)
            {
                _stateMachineAttack.CanSlice = true;
            }
        }
        else
        {
            _stateMachineAttack.CanSlice = false;
        }

        if (_doJump && animator.GetFloat("JumpAnim") > 0)
        {
            _stateMachineVertical.SetTransitionToJumping();
        }
        if (_applyForceValueUpdate && animator.GetFloat("XYAnim") > 0)
        {
            _playerMove.ApplyForceAnimation = true;
        }
        else
        {
            _playerMove.ApplyForceAnimation = false;
        }
        if (_doJumpExit && _getInputBrute.JumpInput.IsActive)
        {
            _canJumpExit = true;
        }
        if (_dodgeToAttack01 && _getInputBrute.TriggerRight > 0)
        {
            _canDodgeToAttack01 = true;
            _stateMachineAttack.SetTransition(PlayerAttackState.ATTACK01);
        }

        if (_modifieDegat)
        {
            //Debug.Log(animator.GetFloat("ModifieDegat"));
            //Debug.Log(_playerEntity.Damage + animator.GetFloat("ModifieDegat"));
            //if (animator.GetFloat("ModifieDegat") > 0)
            //{
            //    Debug.Log("iciAdd");
            //    damage = animator.GetFloat("ModifieDegat");

            //    if (_playerEntity.Damage != _playerEntity.Damage + damage)
            //    {
            //        Debug.Log("iciAdd2");
            //        _playerEntity.Damage = _playerEntity.Damage + animator.GetFloat("ModifieDegat");
            //    }
            //}
            //Debug.Log(_playerEntity.Damage - damage);
            //if (animator.GetFloat("ModifieDegat") == 0)
            //{
            //    Debug.Log("iciLess");
            //    if (_playerEntity.Damage != _playerEntity.Damage - damage)
            //    {
            //        Debug.Log("iciLess2");
            //        _playerEntity.Damage = _playerEntity.Damage - damage;
            //    }
            //}

            if (animator.GetFloat("ModifieDegat") > 0)
            {
                if (!_addModfieDamage)
                {
                    _damageModifie = animator.GetFloat("ModifieDegat");
                    _playerEntity.Damage += _damageModifie;
                    _addModfieDamage = true;
                }
            }

            if(animator.GetFloat("ModifieDegat") == 0)
            {
                if(_addModfieDamage && !_lessModifieDamage)
                {
                    _playerEntity.Damage -= _damageModifie;
                    _lessModifieDamage = true;
                }
            }
        }

        if (_playSondUpdate)
        {
            if (animator.GetFloat("Slice") > 0)
            {
                _weaponColliderManager.PlaySon(_intSond, _timeSoundMax);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_dodgeToAttack01 && _canDodgeToAttack01)
        {
            _activeInAnimOnExit = false;
        }
        if (_activeInAnimOnExit)
        {
            animator.SetBool("IsAttackingAxe", false);
            animator.SetBool("IsAttackingKick", false);
            _stateMachineAttack.IsAnim = false;
        }
        if (_applyForceExit)
        {
            _playerMove.CanApplyForceAnimation = false;
        }
        if (_doJumpExit && _canJumpExit)
        {
            _stateMachineVertical.SetTransitionToJumping();
        }
        if (_doJumpExit2)
        {
            _stateMachineVertical.SetTransitionToJumping();
            _playerMove.DoJump();
        }

        if (!_endCombo)
        {
            animator.SetInteger("CptCombo", animator.GetInteger("CptCombo") + 1);
            _stateMachineAttack.CptCombo = animator.GetInteger("CptCombo");
        }
        else
        {
            animator.SetInteger("CptCombo", 0);
            _stateMachineAttack.CptCombo = animator.GetInteger("CptCombo");
        }

        if (animator.GetBool("IsAttackingAxe"))
        {
            animator.SetBool("IsAttackingAxe", false);
        }
        if (animator.GetBool("IsAttackingKick"))
        {
            animator.SetBool("IsAttackingKick", false);
        }
        if (animator.GetBool("IsProtected"))
        {
            animator.SetBool("IsProtected", false);
        }
        if (animator.GetBool("IsDodged"))
        {
            animator.SetBool("IsDodged", false);
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
