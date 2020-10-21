using UnityEngine;
using UnityEngine.AI;

public class AnimationControler : MonoBehaviour
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] Animator _animator;
    [SerializeField] StateAttacking _stateAttacking;
    private void Awake()
    {
        
    }

    private void Update()
    {
        _speed = m_Agent.speed;

        _animator.SetFloat(_speedId, _speed);
        _animator.SetBool(_isAttacking1, _stateAttacking.IsAttacking);
    }

    private float _speed;

    private int _speedId = Animator.StringToHash("Speed");
    private int _isAttacking1 = Animator.StringToHash("IsAttacking1");


}
