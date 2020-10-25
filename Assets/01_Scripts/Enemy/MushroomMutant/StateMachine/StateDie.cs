using UnityEngine;
using UnityEngine.AI;

public class StateDie : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _enemyEntity;
    [SerializeField] private MushroomManager _mushroomManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Entering state: StateDie");

        //initialise le nav mesh agent et assigne au speed la valeur de mushroom data
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = 0;

        _mushroomManager = animator.GetComponent<MushroomManager>();

        _mushroomManager.IsDead = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Staying in state: StateDie");
        //_mushroomManager.IsDead = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Exiting state: StateDie");
    }
}
