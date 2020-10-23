﻿using UnityEngine;
using UnityEngine.AI;

public class StateAlert : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private MushroomEntity _mushroomEntity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Detect");

        if (_mushroomEntity = null)
        {
            _mushroomEntity = animator.GetComponent<MushroomEntity>();
        }

        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Detect");

        /*Transitons*/

        animator.SetTrigger(_alertId);

        //si la vie est à 0 on meurt
        if (_mushroomEntity.Life <= 0)
        {
            animator.SetTrigger(_dieId);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: Detect");
    }

    private int _alertId = Animator.StringToHash("Alert");
    private int _dieId = Animator.StringToHash("Die");
}
