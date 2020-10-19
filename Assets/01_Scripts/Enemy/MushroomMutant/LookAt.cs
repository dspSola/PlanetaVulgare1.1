using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform _head = null;
    [SerializeField] Vector3 _lookAtTargetPosition;
    [SerializeField] float _lookAtCoolTime = 0.2f;
    [SerializeField] float _lookAtHeatTime = 0.2f;
    [SerializeField] bool _looking = true;

    void Start()
    {
        if (!_head)
        {
            Debug.LogError("No head transform - LookAt disabled");
            enabled = false;
            return;
        }
        _animator = GetComponent<Animator>();
        _lookAtTargetPosition = _head.position + transform.forward;
        _lookAtPosition = _lookAtTargetPosition;
    }

    void OnAnimatorIK()
    {
        _lookAtTargetPosition.y = _head.position.y;
        float lookAtTargetWeight = _looking ? 1.0f : 0.0f;

        Vector3 curDir = _lookAtPosition - _head.position;
        Vector3 futDir = _lookAtTargetPosition - _head.position;

        curDir = Vector3.RotateTowards(curDir, futDir, 6.28f * Time.deltaTime, float.PositiveInfinity);
        _lookAtPosition = _head.position + curDir;

        float blendTime = lookAtTargetWeight > _lookAtWeight ? _lookAtHeatTime : _lookAtCoolTime;
        _lookAtWeight = Mathf.MoveTowards(_lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
        _animator.SetLookAtWeight(_lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
        _animator.SetLookAtPosition(_lookAtPosition);
    }

    private Vector3 _lookAtPosition;
    private Animator _animator;
    private float _lookAtWeight = 0.0f;
}
