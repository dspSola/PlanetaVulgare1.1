using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AttackStateMachineAnimator : StateMachineBehaviour
{
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private StateMachineVertical _stateMachineVertical;
    [SerializeField] private StateMachineHorizontal _stateMachineHorizontal;
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private bool _canSlice, _activeInAnimOnStart, _activeInAnimOnExit;

    [SerializeField] private bool _applyForceEnter, _applyForceExit, _applyForceValueEnter, _applyForceValueUpdate, _applyForceValueExit;

    [SerializeField] private bool _doJump;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _stateMachineAttack = animator.transform.parent.GetComponentInChildren<StateMachineAttack>();
        _stateMachineVertical = animator.transform.parent.GetComponentInChildren<StateMachineVertical>();
        _stateMachineHorizontal = animator.transform.parent.GetComponentInChildren<StateMachineHorizontal>();
        _playerMove = animator.transform.parent.GetComponentInChildren<PlayerMove>();

        if (_activeInAnimOnStart)
        {
            _stateMachineAttack.IsAnim = true;
        }
        if(_applyForceEnter)
        {
            _playerMove.CanApplyForceAnimation = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat("Slice") > 0)
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
        if(_applyForceValueUpdate && animator.GetFloat("XYAnim") > 0)
        {
            _playerMove.ApplyForceAnimation = true;
        }
        else
        {
            _playerMove.ApplyForceAnimation = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_activeInAnimOnExit)
        {
            _stateMachineAttack.IsAnim = false;
        }
        if (_applyForceExit)
        {
            _playerMove.CanApplyForceAnimation = false;
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
