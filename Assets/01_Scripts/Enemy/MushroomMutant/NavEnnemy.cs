using UnityEngine;
using UnityEngine.AI;

public class NavEnnemy : MonoBehaviour
{
    [SerializeField] Transform[] _wayPoint;
    [SerializeField] Transform _player; //à remplacer par la futur dat du transform player

    [SerializeField] NavMeshAgent _agent;

    private void Awake()
    {
        _patrol = transform;
    }

    private void Update()
    {
        
    }

    int i;
    Transform _patrol;
}
