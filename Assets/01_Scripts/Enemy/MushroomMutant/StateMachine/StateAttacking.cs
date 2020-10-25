using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateAttacking : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _mushroomEntityData;
    [SerializeField] private ScriptableTransform _playerTransform;
    [SerializeField] private AnimationControler _animatorControler;
    [SerializeField] private float _attackDistance = 2f;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Timer")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 5f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Entering state: StateAttacking");

        _transform = animator.GetComponent<Transform>();
        m_Agent = animator.GetComponent<NavMeshAgent>();
        _animatorControler = animator.GetComponentInChildren<AnimationControler>();
        m_Agent.speed = _mushroomEntityData.SpeedRun;

        _isDelayed = false;
        _animatorControler.IsFigthing = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Staying in state: StateAttacking");

        _distanceRange = Vector3.Distance(_transform.position, _playerTransform.value.position);

        if (_distanceRange < _attackDistance)
        {
            m_Agent.stoppingDistance = _attackDistance;
            _animatorControler.IsFigthing = true;
        }

        Timer();
        //Debug.Log(_currentTime + " second attack");

        DoChassing();

        /*Transitons*/

        if (_isDelayed)
        {
            animator.SetTrigger(_modeCombat);
        }

        //si la vie est à 0 on meurt
        if (animator.TryGetComponent(out MushroomManager mushroomManager))
        {
            if (mushroomManager.IsDead)
            {
                animator.SetTrigger(_dieId);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Exiting state: StateAttacking");
    }

    private void DoChassing()
    {
        m_Agent.destination = _playerTransform.value.position;
        //if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        //{
            
        //}
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

    private Transform _transform;

    private bool _isDelayed;
    private float _currentTime;
    float _delayTime;

    private float _distanceRange;

    private int _modeCombat = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
}
