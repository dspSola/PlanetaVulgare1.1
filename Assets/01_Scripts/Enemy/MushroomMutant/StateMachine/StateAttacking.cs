using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateAttacking : StateMachineBehaviour
{
    [SerializeField] IntVariable _mushroomCurrentLife;

    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private AverageAttack _averageAttack;
    [SerializeField] private ScriptableTransform _playerTransform;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Timer")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _minTime = 0.5f;
    [SerializeField] private float _maxTime = 2f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: StateAttacking");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        _averageAttack = animator.GetComponent<AverageAttack>();
        m_Agent.speed = _enemyEntity.SpeedRun;
        _averageAttack.IsAttacking = true;

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
        if (_mushroomCurrentLife.value <= 0)
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
        Vector3 safeDistance = new Vector3(0, 0, _averageAttack.AttackDistance);
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            m_Agent.destination = _playerTransform.value.position - safeDistance;//marche dans un sens :(
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
    }

    private bool _isDelayed;
    private float _currentTime;
    float _delayTime;

    private int _modeCombat = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
}
