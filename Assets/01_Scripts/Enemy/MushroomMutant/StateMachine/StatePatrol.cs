using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : StateMachineBehaviour
{
    [Header("Waypoint Info")]
    [SerializeField] private TransformArrayData _waitPoints;
    [SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private DetectionPlayer _detectionPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Patrol");
        _detectionPlayer = animator.GetComponentInChildren<DetectionPlayer>();
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _enemyEntity.SpeedWalk;

        //DoPatrol();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Patrol");
        DoPatrol();
        //si je detecte 
        if (_detectionPlayer.PlayerIsTrigger)
        {
            animator.SetTrigger(_detectId);
        }
        //ou si je suis alerté
        //if ()
        //{

        //}

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
            if (_waitPoints.Value.Count == 0)
            {
                return;
            }

            m_Agent.destination = _waitPoints.Value[i].position;
            i = (i + 1) % _waitPoints.Value.Count;
        }
    }

    private int i;

    private int _detectId = Animator.StringToHash("Detect");
    private int _alertId = Animator.StringToHash("Alert");
}
