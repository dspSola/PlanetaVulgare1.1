using UnityEngine;
using UnityEngine.AI;

public class NavEnnemy : MonoBehaviour
{
    [Header("Waypoint Info")]
    [SerializeField] Transform[] _waitPoint;
    [SerializeField] float _waitpointDistance = 0.2f;

    [Header("Player Info")]
    [SerializeField] Transform _playerPosition; //à remplacer par la futur dat du transform player
    [SerializeField] DetectionPlayer _detectionPlayer;
    [SerializeField] Vector3 _offset;

    [SerializeField] Camera _cam;

    private void Awake()
    {
        _patrol = transform;
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //MoveWhithMouse();
        if(!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            Patrol();
        }
        
    }

    private void Patrol()
    {
        if(_waitPoint.Length == 0)
        {
            return;
        }

        m_Agent.destination = _waitPoint[i].position;
        i = (i + 1) % _waitPoint.Length;

        Debug.Log("mush et au"+ _waitPoint[i] +"point");
    }

    private void MoveWhithMouse()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                m_Agent.destination = m_HitInfo.point;
            }
        }
    }

    int i;
    Transform _patrol;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
}
