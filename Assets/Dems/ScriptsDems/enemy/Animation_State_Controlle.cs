using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animation_State_Controlle : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Enemy_Controller _enemyController;
    [SerializeField] private float _acceleration = 0.1f;
    [SerializeField] private float _deceleration = 0.5f;

    

    // Start is called before the first frame update
    void Start()
    {
        _velocityHash = Animator.StringToHash("Velocity");
        
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat(_velocityHash, _velocity);
    }


    void MoveEnemy()
    {
        
    }

    private float _velocity = 0.0f;
    private int _velocityHash;
    
}
