using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TreeMutantMovement : MonoBehaviour
{
    [SerializeField] float _range = 10.0f;
    [SerializeField] float _nbPoint = 30;
    [SerializeField] float _stopDistance = 1f;
    [SerializeField] float _delay = 2f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //_isStopping = false;
    }

    private void Update()
    {
        Vector3 point;

        if (/*!_isStopping &&*/ _navMeshAgent.remainingDistance < 0.2f)
        {
            if (RandomPoint(transform.position, _range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                if (NextPosition(transform.position, point))
                {
                    if (_navMeshAgent != null)
                    {
                        _navMeshAgent.destination = point;
                        _speedAgent = 2f;
                        _navMeshAgent.speed = _speedAgent;
                    }
                }
            }
        }
        //else
        //{
        //    StartCoroutine(DelayStop());
        //}
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < _nbPoint; i++)
        {
            // position de départ + pont random dans un cercle * range(radius)
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            // info sur la position d'arrivée
            NavMeshHit hit;

            //si il y a une position random sur un navAera
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                // le résultat égale la postion
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    private bool NextPosition(Vector3 currentPos, Vector3 nextPos)
    {
        // si la position courante n'est pas égale à la prochaine position
         if(currentPos != nextPos)
        {
            //_isStopping = true;
            return true;
        }
         else
        {
            //_isStopping = false;
            return false;
        }
    }

    IEnumerator DelayStop()
    {
        // l'agent ce stop à _stop
        _navMeshAgent.stoppingDistance = _stopDistance;
        _speedAgent = 0;
        _navMeshAgent.speed = _speedAgent;

        yield return new WaitForSeconds(_delay);

        //_isStopping = false;
    }

    private NavMeshAgent _navMeshAgent;
    private float _speedAgent;
    public bool _isStopping;

    public float SpeedAgent { get => _speedAgent; set => _speedAgent = value; }
}
