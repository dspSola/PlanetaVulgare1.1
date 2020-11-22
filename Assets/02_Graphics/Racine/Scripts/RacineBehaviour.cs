using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacineBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Transform _parentTr, _posGround, _posRayCast;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _distance, _distanceToActive, _distanceToAttack, _coefRotate;

    [SerializeField] private bool _canTouchPlayer, _touchPlayer, _rayCastHit, _playerTouchThisRacine;
    [SerializeField] private Vector3 _targetPostition;

    private Animator anim;
    private int _paramAwakeID;
    private int _paramAttackID;
    private int _paramDieID;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _paramAttackID = Animator.StringToHash("attack");
        _paramAwakeID = Animator.StringToHash("awake");
        _paramDieID = Animator.StringToHash("die");
    }

    private void Start()
    {
        _distanceToActive = Random.Range(2f, 3f) * transform.parent.transform.localScale.x;
        _distanceToAttack = 2f * transform.parent.transform.localScale.x;
        _coefRotate = Random.Range(3f, 6f);
        Destroy(transform.parent.gameObject, Random.Range(60f, 130f));
    }

    private void Update()
    {
        // DEBUG INPUTS !

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    // Line to awake it
        //    // Maybe in the Awake of the Racine Component ?
        //    anim.SetTrigger(_paramAwakeID);
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Line to make it attack : only visually tho
        //    anim.SetTrigger(_paramAttackID);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    // Line to make it die
        //    anim.SetTrigger(_paramDieID);
        //}

        // -------------------------------------------
        // ALWAYS FOLLOW PLAYER
        // -------------------------------------------

        _distance = Vector3.Distance(_posGround.position, _playerData.Transform.position);
        _targetPostition = new Vector3(_playerData.Transform.position.x,
                                this.transform.position.y,
                                _playerData.Transform.position.z);

        if (_distance < _distanceToActive)
        {
            anim.SetTrigger(_paramAwakeID);
        }
        if (_distance < _distanceToAttack && _rayCastHit)
        {
            anim.SetTrigger(_paramAttackID);
        }

        if (!_canTouchPlayer)
        {
            FollowPlayer();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_posRayCast.position, _posRayCast.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(_posRayCast.position, _posRayCast.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            _rayCastHit = true;
        }
        else
        {
            Debug.DrawRay(_posRayCast.position, _posRayCast.TransformDirection(Vector3.forward) * 1000, Color.white);
            _rayCastHit = false;
        }
    }

    private void FollowPlayer()
    {
        //transform.LookAt(targetPostition, Vector3.up);
        //Vector3 relativePos = _targetPostition - _posGround.position;

        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up * Time.deltaTime);
        //transform.rotation = rotation;

        //transform.RotateAround(_targetPostition, Vector3.up, 20 * Time.deltaTime);

        var newRotation = Quaternion.LookRotation(_targetPostition - _parentTr.position, Vector3.up);
        _parentTr.rotation = Quaternion.Slerp(_parentTr.rotation, newRotation, Time.deltaTime * _coefRotate);
    }

    // Event in animation : Destroy after die anim
    public void DieAfterAnim()
    {
        Destroy(transform.parent.gameObject);
    }

    public void Die()
    {
        _playerTouchThisRacine = true;
        anim.SetTrigger(_paramDieID);
    }

    public void StartBlender()
    {
        _canTouchPlayer = true;
    }

    public void EndBlender()
    {
        _canTouchPlayer = false;
        if(_touchPlayer)
        {
            _touchPlayer = false;
        }
    }

    public bool CanTouchPlayer { get => _canTouchPlayer; set => _canTouchPlayer = value; }
    public bool TouchPlayer { get => _touchPlayer; set => _touchPlayer = value; }
    public bool PlayerTouchThisRacine { get => _playerTouchThisRacine; set => _playerTouchThisRacine = value; }
}
