using UnityEngine;
using UnityEngine.AI;

public class AnimationControler : MonoBehaviour
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] Animator _animator;
    private void Awake()
    {
        
    }

    private void Update()
    {
        _speed = m_Agent.speed;

        _animator.SetFloat(_speedId, _speed);
    }

    private float _speed;

    private int _speedId = Animator.StringToHash("Speed");
    
}
