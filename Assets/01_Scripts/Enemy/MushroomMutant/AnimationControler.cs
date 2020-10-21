using UnityEngine;
using UnityEngine.AI;

public class AnimationControler : MonoBehaviour
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] Animator _animator;
    [SerializeField] AverageAttack _averageAttack;
    [SerializeField] MushroomManager _mushroomManager;

    private void Awake()
    {
    }

    private void Update()
    {
        _speed = m_Agent.speed;

        _animator.SetFloat(_speedId, _speed);
        _animator.SetBool(_isAttacking1Id, _averageAttack.IsAttacking);
        _animator.SetBool(_isDeadId, _mushroomManager.IsDead);
    }

    private float _speed;

    private int _speedId = Animator.StringToHash("Speed");
    private int _isAttacking1Id = Animator.StringToHash("IsAttacking1");
    private int _isDeadId = Animator.StringToHash("IsDead");
}
