using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateAttacking : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private ScriptableTransform _playerTransform;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Timer")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 5f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: StateAttacking");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _enemyEntity.SpeedRun;

        _isDelayed = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: StateAttacking");

        Timer();
        //Debug.Log(_currentTime + " second");

        DoChassing();

        /*Transitons*/

        if (_isDelayed)
        {
            animator.SetTrigger(_modeCombat);
        }

        //si la vie est à 0 on meurt
        if (_enemyEntity.CurrentLife <= 0)
        {
            animator.SetTrigger(_dieId);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: StateAttacking");
    }

    private void DoChassing()
    {
        
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            m_Agent.destination = _playerTransform.value.position ;
        }
    }

    private void Timer()
    {
        if (_currentTime >= 0)
        {
            _currentTime += Time.deltaTime;
        }

        if (_currentTime > _delayTime)
        {
            _isDelayed = true;
        }
        else
        {
            _isDelayed = false;
        }
    }

    private bool _isDelayed;
    private float _currentTime;
    float _delayTime;

    private int _modeCombat = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
}
