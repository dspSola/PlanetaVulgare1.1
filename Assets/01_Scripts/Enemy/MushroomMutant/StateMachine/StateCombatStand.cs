using UnityEngine;
using UnityEngine.AI;

public class StateCombatStand : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] ScriptableTransform _playerTransform;
    [SerializeField] private EnemyEntityData _enemyEntity;

    [Header("Timer")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: CombatSand");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = 0;

        _isDelayed = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: CombatSand");

        Timer();

        /*Transitons*/

        //si le temps du delay est dépassé alors il attaque
        if (_isDelayed)
        {
            animator.SetTrigger(_attackingId);
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
        Debug.Log("Exiting state: CombatSand");
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

    private int _attackingId = Animator.StringToHash("Attacking");
    private int _dieId = Animator.StringToHash("Die");
}
