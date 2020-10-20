using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateModeEnemy
{
    Patrol,
    Chase,
    Attack
}



public class MoveEnemy : MonoBehaviour
{

    [SerializeField] private Transform[] _waitPoint;
    public NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] readonly float _waitpointDistance = 0.2f;
    [SerializeField] private Animator _animator;
    private int i;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        if(_agent == null)
        {
            Debug.LogError("NavMeshAgent manquant" + gameObject.name);
        }

        _speed = _agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
       

        //if (!_agent.pathPending && _agent.remainingDistance < _waitpointDistance)
        //{
        //    Patrol();
        //}
    }

    private void Patrol()
    {
        if (!_agent.pathPending && _agent.remainingDistance < _waitpointDistance)
        {

            _animator.SetBool("move", true);
            _animator.SetFloat(_speedId, _speed);

            _agent.destination = _waitPoint[i].position;
            i = (i + 1) % _waitPoint.Length;

            
            
            Debug.Log("la vitesse est " + _speed);

            



            Debug.Log("enemy et au" + _waitPoint[i] + "point");
        }
        else
        {
            _animator.SetBool("move", false);

        }


    }

    private void Chase()
    {
        if (Vector3.Distance(_player.position, this.transform.position) < 10)
        {
            Vector3 _direction = _player.position - this.transform.position;
            _direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(_direction), 0.1f);

            if (_direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.5f);
            }
        }
    }

    
    
        
    

    private int _speedId = Animator.StringToHash("Speed");
    private StateModeEnemy _curentModEnemy;
}
