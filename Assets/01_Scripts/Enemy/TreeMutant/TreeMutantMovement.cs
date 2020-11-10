using UnityEngine;
using UnityEngine.AI;

public class TreeMutantMovement : MonoBehaviour
{
    [SerializeField] float _radius;

    private void Awake()
    {
        _transform = transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_navMeshAgent.destination != null)
        {
            RandomNavmeshLocation(_radius);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += _transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.forward/*= Vector3.zero*/;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    Transform _transform;
    NavMeshAgent _navMeshAgent;
}
