using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : StateMachineBehaviour
{
    [Header("Waypoint Info")]
    [SerializeField] Transform[] _waitPoint;
    [SerializeField] float _waitpointDistance = 0.2f;

    [Header("Parameter")]
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] EnemyEntity _enemyEntity; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_Agent = FindObjectOfType<NavMeshAgent>;
        Debug.Log("Entering state: Patrol");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _enemyEntity.SpeedWalk;
        DoPatrol();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Patrol");
        
        //si je detecte
        //si je suis alerté
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: Patrol");
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

    private void DoPatrol()
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            if (_waitPoint.Length == 0)
            {
                return;
            }

            m_Agent.destination = _waitPoint[i].position;
            i = (i + 1) % _waitPoint.Length;
        }
    }

    int i;
}
