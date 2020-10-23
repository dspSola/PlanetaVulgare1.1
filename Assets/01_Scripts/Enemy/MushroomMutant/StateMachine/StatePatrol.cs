using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : StateMachineBehaviour
{
    [Header("Waypoint Info")]
    [SerializeField] private TransformArrayData _waitPoints;
    [SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] MushroomEntity _mushroomEntity;
    [SerializeField] private DetectionPlayer _detectionPlayer;
    [SerializeField] private AlertCircle _alertCircle;
    [SerializeField] private BoolVariable _isDesactivedCone;

    [Header("Timer")]
    [SerializeField] private float _minTime = 2000f;
    [SerializeField] private float _maxTime = 10000f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering state: Patrol");

        if (_mushroomEntity = null)
        {
            _mushroomEntity = animator.GetComponent<MushroomEntity>();
        }

        _isDesactivedCone.value = false;
        _detectionPlayer = animator.GetComponentInChildren<DetectionPlayer>();
        _alertCircle = animator.GetComponentInChildren<AlertCircle>();
        m_Agent = animator.GetComponent<NavMeshAgent>();

        m_Agent.speed = _mushroomEntity.SpeedWalk;

        _isDelayed = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Staying in state: Patrol");

        DoPatrol();
        Timer();

        /*Transitons*/

        //si le temps delay est vrais il part en idle
        //if (_isDelayed)
        //{
        //    animator.SetTrigger(_idleId);
        //}
        //    _isDelayed = false;
        
        //si je detecte 
        if (_detectionPlayer.PlayerIsTrigger)
        {
            animator.SetTrigger(_detectId);
        }

        //ou si je suis alerté
        //if (_alertCircle.IsAlerted)
        //{
        //    animator.SetTrigger(_alertId);
        //}

        //si la vie est à 0 on meurt
        //if (_enemyEntity.CurrentLife <= 0)
        //{
        //    animator.SetTrigger(_dieId);
        //}
        //Debug.Log($"en Patrol le random est à : {_delayTime} le temps est de {_currentTime} le bool est : {_isDelayed}");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting state: Patrol");
        _isDelayed = false;
    }

    private void DoPatrol()
    {
        if (!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            if (_waitPoints.Value.Count == 0)
            {
                return;
            }

            m_Agent.destination = _waitPoints.Value[i].position;
            i = (i + 1) % _waitPoints.Value.Count;
        }
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

    private int i;

    private bool _isDelayed;
    private float _currentTime;
    float _delayTime;

    private int _detectId = Animator.StringToHash("Detect");
    private int _alertId = Animator.StringToHash("Alert");
    private int _dieId = Animator.StringToHash("Die");
    private int _idleId = Animator.StringToHash("Idle");
}
