using UnityEngine;
using UnityEngine.AI;

public class StateChassingTarget : StateMachineBehaviour
{
    [SerializeField] IntVariable _mushroomCurrentLife;

    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private AverageAttack _averageAttack;
    [SerializeField] private ScriptableTransform _playerTransform;
    [SerializeField] private float _distance = 1f;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Chassing");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        _averageAttack = animator.GetComponent<AverageAttack>();
        m_Agent.speed = _enemyEntity.SpeedRun;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Chassing");
        DoChassing();

        /*Transitons*/

        //si la distance est averageAttack on passe en combat
        if (_averageAttack.IsWithinRange)
        {
            animator.SetTrigger(_modeCombatId);
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
        Debug.Log("Exiting state: Chassing");
    }
    private void DoChassing()
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            m_Agent.destination = _playerTransform.value.position;
            m_Agent.stoppingDistance = _distance;
        }
    }
    
    private int _modeCombatId = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
}
