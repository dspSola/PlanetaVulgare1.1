using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossBlenderControl : StateMachineBehaviour
{
    [SerializeField] private FireBossAgentController _fireBossAgentController;
    [SerializeField] private FireBossAnimator _fireBossAnimator;
    [SerializeField] private ClawCollider _clawCollider;

    [SerializeField] bool _makePauseDestinationAttack, _makePoseRotationAttack, _canMakeRotationPauseAttack, _isAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_fireBossAgentController == null)
        {
            _fireBossAgentController = animator.GetComponentInParent<FireBossAgentController>();
        }
        if (_fireBossAnimator == null)
        {
            _fireBossAnimator = animator.GetComponentInParent<FireBossAnimator>();
        }
        // Set Modification 
        if (_clawCollider == null)
        {
            _clawCollider = animator.GetComponentInChildren<ClawCollider>();
        }
        if (_makePauseDestinationAttack)
        {
            _fireBossAgentController.MakePauseAttack = true;
        }
        if (_makePoseRotationAttack)
        {
            _fireBossAgentController.MakeRotationPauseAttack = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat("CanDamageCac") > 0)
        {
            _clawCollider.CanDamageAnim = true;
        }
        else
        {
            _clawCollider.CanDamageAnim = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_makePauseDestinationAttack)
        {
            _fireBossAgentController.MakePauseAttack = false;
        }
        if (_makePoseRotationAttack)
        {
            _fireBossAgentController.MakeRotationPauseAttack = false;
        }
        if (_isAttack)
        {
            _fireBossAnimator.SetAttack(false, 0);
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
