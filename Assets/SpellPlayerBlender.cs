using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPlayerBlender : StateMachineBehaviour
{
    [SerializeField] private PlayerSpellController _playerSpellController;
    [SerializeField] private int _cptSpell;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _cptSpell = 0;
        if(_playerSpellController == null)
        {
            _playerSpellController = animator.transform.parent.GetComponentInChildren<PlayerSpellController>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("CanLunchSpell") > 0)
        {
            if (_cptSpell < 1)
            {
                _playerSpellController.SpawnSpellRage();
                _cptSpell++;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AttackSpecial", false);
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
