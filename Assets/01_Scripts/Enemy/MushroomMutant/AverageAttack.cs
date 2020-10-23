using UnityEngine;
using UnityEngine.AI;

public class AverageAttack : MonoBehaviour
{
    [SerializeField] ScriptableTransform _playerTransform;
    [SerializeField] float _attackDistance;

    private void Awake()
    {
        _transform = transform;
        _isWithinRange = false;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float averageDistance;
        averageDistance = Vector3.Distance(_transform.position, _playerTransform.value.position);

        _navMeshAgent.stoppingDistance = _attackDistance;

        if (_navMeshAgent.stoppingDistance <= averageDistance)
        {
            _isWithinRange = true;
        }
        else
        {
            _isWithinRange = false;
        }

    }

    private Transform _transform;
    private NavMeshAgent _navMeshAgent;

    private bool _isWithinRange;
    private bool _isAttacking;

    public bool IsWithinRange { get => _isWithinRange; set => _isWithinRange = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public float AttackDistance { get => _attackDistance; set => _attackDistance = value; }
}
