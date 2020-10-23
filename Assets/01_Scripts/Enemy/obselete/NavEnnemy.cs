using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class NavEnnemy : MonoBehaviour
{
    [Header("Waypoint Info")]//à retirer
    [SerializeField] Transform[] _waitPoint;
    [SerializeField] float _waitpointDistance = 0.2f;

    [Header("Player Info")]
    [SerializeField] Transform _playerPosition; //à remplacer par la futur dat du transform player
    [SerializeField] DetectionPlayer _detectionPlayer;
    [SerializeField] Vector3 _offset;

    [SerializeField] Camera _cam;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        //_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!m_Agent.pathPending && m_Agent.remainingDistance < _waitpointDistance)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        //_speed = _rb. ;
        //Debug.Log("la vitesse" + _speed);
    }

    private void Patrol()//à retirer
    {
        if(_waitPoint.Length == 0)
        {
            return;
        }

        m_Agent.destination = _waitPoint[i].position;
        i = (i + 1) % _waitPoint.Length;
    }

    int i;//à retirer
    NavMeshAgent m_Agent;//à retirer
    RaycastHit m_HitInfo = new RaycastHit();
    //Rigidbody _rb;


    float _speed;
    public float Speed { get => _speed; set => _speed = value; }
}
