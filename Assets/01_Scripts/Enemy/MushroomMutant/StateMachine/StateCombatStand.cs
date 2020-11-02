using UnityEngine;
using UnityEngine.AI;

public class StateCombatStand : StateMachineBehaviour
{
    [Header("Parameter")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private EnemyEntityData _mushroomEntityData;

    [SerializeField] private AnimationControler _animatorControler;
    [SerializeField] ScriptableTransform _playerTransform;
    [SerializeField] private float _attackDistance = 1f;

    //[Header("Waypoint Info")]
    //[SerializeField] private float _waitpointDistance = 0.2f;

    [Header("Timer")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Entering state: CombatSand");

        _transform = animator.GetComponent<Transform>();
        _animatorControler = animator.GetComponentInChildren<AnimationControler>();

        //initialise le nav mesh agent et assigne au speed la valeur de mushroom data
        m_Agent = animator.GetComponent<NavMeshAgent>();
        m_Agent.speed = _mushroomEntityData.SpeedRun;

        //assigation des valeurs aux variables : timer
        _isDelayed = false;
        _currentTime = 0;
        _delayTime = Random.Range(_minTime, _maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Staying in state: CombatSand");

        Timer();
        //Debug.Log(_currentTime + " second av combat");

        _distanceRange = Vector3.Distance(_transform.position, _playerTransform.value.position);

        /*Transitons*/

        //si la distance est inférieur a la distance attack il attaque sinon il chasse
        if (_distanceRange < _attackDistance)
        {
            DoChassing();
            _animatorControler.IsFigthing = false;
        }
        else
        {
            m_Agent.stoppingDistance = _attackDistance;
            animator.SetTrigger(_attackingId);
            _animatorControler.IsFigthing = false;
        }

        //si le temps du delay est dépassé alors il attaque
        //if (_isDelayed)
        //{
        //    animator.SetTrigger(_attackingId);
        //}

        
        if (animator.TryGetComponent(out MushroomManager mushroomManager))
        {
            //Debug.Log("meurt bordel!");
            //si la vie est à 0 on meurt
            if (mushroomManager.IsDead)
            {
                //Debug.Log("meurt bordel! averto 2");
                animator.SetTrigger(_dieId);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Exiting state: CombatSand");
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

    private void DoChassing()
    {
        m_Agent.destination = _playerTransform.value.position;
    }

    //private bool _isReadyForAttack;

    private bool _isDelayed;
    private float _currentTime;
    private float _delayTime;

    private float _distanceRange;
    private int _attackingId = Animator.StringToHash("Attacking");
    private int _dieId = Animator.StringToHash("Die");
    private Transform _transform;
}
