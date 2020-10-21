using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAlert : StateMachineBehaviour
{
    [SerializeField] IntVariable _mushroomCurrentLife;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //si la vie est à 0 on meurt
        if (_mushroomCurrentLife.value <= 0)
        {
            animator.SetTrigger(_dieId);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private int _dieId = Animator.StringToHash("Die");
}
