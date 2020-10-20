using UnityEngine;
using UnityEngine.AI;

public class LocomotionAgent : MonoBehaviour
{
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        // Don’t update position automatically
        _agent.updatePosition = false;
    }

    void Update()
    {
        Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            _velocity = _smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;
        _speed = _agent.acceleration;
        //Debug.Log("la vitesse" + _speed);
        // Update animation parameters
        //_anim.SetBool("move", shouldMove);
        //_anim.SetFloat(_turnId, _velocity.x);
        _anim.SetFloat(_forwardId, _velocity.y);
        _anim.SetFloat(_speedId, _speed);

        //GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = _agent.nextPosition;
    }

    Animator _anim;
    NavMeshAgent _agent;
    Vector2 _smoothDeltaPosition = Vector2.zero;
    Vector2 _velocity = Vector2.zero;
    float _speed;

    private int _turnId = Animator.StringToHash("Turn");
    private int _forwardId = Animator.StringToHash("Forward");
    private int _speedId = Animator.StringToHash("Speed");
}
