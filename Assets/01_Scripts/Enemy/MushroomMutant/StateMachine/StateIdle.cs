using UnityEngine;
using UnityEngine.AI;

public class StateIdle : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] EnemyEntityData _enemyEntity;
    
    [Header("Timer")]
    [SerializeField] private float _minTime = 2000f;
    [SerializeField] private float _maxTime = 10000f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Idle");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = 0;

        _isDelayed = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Idle");

        Timer();

        /*Transitons*/

        //si le temps delay est vrais il part en patrouille
        //if (_isDelayed)
        //{
        //    animator.SetTrigger(_patrolId);
        //}
        //    _isDelayed = false;

        //si la vie est à 0 on meurt
        if (_enemyEntity.CurrentLife <= 0)
        {
            animator.SetTrigger(_dieId);
        }

        //Debug.Log($"en Idle le random est à : {_delayTime} le temps est de {_currentTime} le bool est : {_isDelayed}");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: Idle");
        _isDelayed = false;
    }

    private void Timer()
    {
        if (_currentTime >= 0)
        {
            _currentTime += Time.deltaTime;
        }

        if (_currentTime < _delayTime)
        {
            _isDelayed = true;
        }
    }

    private bool _isDelayed;
    private float _currentTime;
    float _delayTime;

    private int _patrolId = Animator.StringToHash("ModePatrol");
    private int _dieId = Animator.StringToHash("Die");
}
