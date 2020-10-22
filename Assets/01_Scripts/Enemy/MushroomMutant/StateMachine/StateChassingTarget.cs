using UnityEngine;
using UnityEngine.AI;

public class StateChassingTarget : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private ScriptableTransform _playerTransform;
    [SerializeField] private BoolVariable _switchStates;
    [SerializeField] private float _attackDistance = 1f;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Chassing");

        _transform = animator.GetComponent<Transform>();
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _enemyEntity.SpeedRun;
        _switchStates.value = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Chassing");

        _distanceRange = Vector3.Distance(_transform.position, _playerTransform.value.position);

        //Debug.Log("moyenne X: " + _distanceRange);

        //si la distance est inférieur a la distance attack il attaque sinon il chasse
        if(_distanceRange < _attackDistance)
        {
            AverageAttack(_attackDistance);
            _switchStates.value = false;
        }
        else
        {
            DoChassing();
            _switchStates.value = true;
        }
        
        /*Transitons*/

        //si la distance est averageAttack on passe en combat
        if (_switchStates.value)
        {
            animator.SetTrigger(_modeCombatId);
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
        Debug.Log("Exiting state: Chassing");
    }
    private void DoChassing()
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            m_Agent.destination = _playerTransform.value.position;
        }
    }

    private void AverageAttack(float distance)
    {
        m_Agent.stoppingDistance = distance;
    }

    
    private float _distanceRange;
    private int _modeCombatId = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
    private Transform _transform;
}
