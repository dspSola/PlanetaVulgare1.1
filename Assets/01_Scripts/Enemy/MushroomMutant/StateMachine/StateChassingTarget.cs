using UnityEngine;
using UnityEngine.AI;

public class StateChassingTarget : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private MushroomEntity _mushroomEntity;
    [SerializeField] private ScriptableTransform _playerTransform;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private BoolVariable _isDesactivedCone;

    [Header("Waypoint Info")]
    [SerializeField] private float _waitpointDistance = 0.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Chassing");

        if (_mushroomEntity = null)
        {
            _mushroomEntity = animator.GetComponent<MushroomEntity>();
        }

        _isDesactivedCone.value = true;
        _transform = animator.GetComponent<Transform>();
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _mushroomEntity.SpeedRun;
        _isReadyForAttack = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Chassing");

        _distanceRange = Vector3.Distance(_transform.position, _playerTransform.value.position);

        //Debug.Log("moyenne X: " + _distanceRange);

        //si la distance est inférieur a la distance attack il attaque sinon il chasse
        if (_distanceRange < _attackDistance)
        {
            m_Agent.stoppingDistance = _attackDistance;
            _isReadyForAttack = true;
        }
        else
        {
            DoChassing();
            _isReadyForAttack = false;
        }

        /*Transitons*/

        //si la distance est averageAttack on passe en combat
        if (_isReadyForAttack)
        {
            animator.SetTrigger(_modeCombatId);
        }

        //si la vie est à 0 on meurt
        if (_mushroomEntity.Life <= 0)
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
        m_Agent.destination = _playerTransform.value.position;
        //if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        //{
            
        //}
    }

    
    private bool _isReadyForAttack;
    private float _distanceRange;
    private int _modeCombatId = Animator.StringToHash("ModeCombat");
    private int _dieId = Animator.StringToHash("Die");
    private Transform _transform;
}
