using UnityEngine;
using UnityEngine.AI;

public class AnimationControler : MonoBehaviour
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] Animator _animator;
    [SerializeField] MushroomManager _mushroomManager;
    [SerializeField] private BoolVariable _switchStates;

    private void Awake()
    {
        //_stateChassingTarget = GetComponent<StateChassingTarget>();
    }

    private void Update()
    {
        _speed = m_Agent.speed;

        _animator.SetFloat(_speedId, _speed);
        _animator.SetBool(_isAttacking1Id, _isFigthing);
        _animator.SetBool(_isDeadId, _mushroomManager.IsDead);
    }
    private void EventDie()
    {
        _animator.GetComponent<Animator>().enabled = true;
        //_mushroomManager.IsDead = false;
    }

    private float _speed;
    private bool _isFigthing;

    private StateChassingTarget _stateChassingTarget;
    private int _speedId = Animator.StringToHash("Speed");
    private int _isAttacking1Id = Animator.StringToHash("IsAttacking1");
    private int _isDeadId = Animator.StringToHash("IsDead");

    public bool IsFigthing { get => _isFigthing; set => _isFigthing = value; }
}
