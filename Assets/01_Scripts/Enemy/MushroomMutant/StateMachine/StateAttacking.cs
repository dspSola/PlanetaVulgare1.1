using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateAttacking : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private AverageAttack _averageAttack;
    [SerializeField] private ScriptableTransform _playerTransform;
    [SerializeField] private float _timeDelay;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: StateAttacking");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        _averageAttack = animator.GetComponent<AverageAttack>();
        m_Agent.speed = _enemyEntity.SpeedRun;
        _isAttacking = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: StateAttacking");
        //DoChassing();
        if(_isDelayed)
        {
            animator.SetTrigger(_modeCombat);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: StateAttacking");
    }

    IEnumerator DelayAttack()
    {
        _isDelayed = false;
        yield return new WaitForSeconds(_timeDelay);
        _isDelayed = true;
    }
        

    //private void DoChassing()
    //{
    //    if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
    //    {
    //        m_Agent.destination = _playerTransform.value.position;
    //    }
    //}

    private int _modeCombat = Animator.StringToHash("ModeCombat");
    private bool _isAttacking;
    private bool _isDelayed;

    public bool IsAttacking { get => _isAttacking;}
}
