using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponAnimator : StateMachineBehaviour
{
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private bool _activeGo, _activeInAnimOnStart, _activeInAnimOnExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _stateMachineAttack = animator.transform.parent.GetComponentInChildren<StateMachineAttack>();
        if (_activeInAnimOnStart)
        {
            _stateMachineAttack.IsAnim = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat("CanChangeWeapon") == 1)
        {
            _stateMachineAttack.SetActiveWeapon(_activeGo);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsArmed", _activeGo);
        _stateMachineAttack.IsArmed = _activeGo;
        animator.SetBool("ChangeWeapon", false);
        if (_activeInAnimOnExit)
        {
            _stateMachineAttack.IsAnim = false;
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
