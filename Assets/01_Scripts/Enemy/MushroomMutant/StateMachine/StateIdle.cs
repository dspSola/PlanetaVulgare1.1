using UnityEngine;
using UnityEngine.AI;

public class StateIdle : StateMachineBehaviour
{
    [SerializeField] IntVariable _mushroomCurrentLife;

    [Header("Parameter")]
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] EnemyEntityData _enemyEntity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Idle");
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Idle");
        animator.SetTrigger("Patrol");

        //si la vie est à 0 on meurt
        if (_mushroomCurrentLife.value <= 0)
        {
            animator.SetTrigger(_dieId);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: Idle");
    }

    private int _dieId = Animator.StringToHash("Die");
}
