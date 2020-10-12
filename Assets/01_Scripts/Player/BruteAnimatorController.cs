﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAnimatorController : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getInputBrute;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        SetInputMove(_getInputBrute.Movement);
        SetSpeed(_playerMove.MovementSpeed);
        SetVelocityY(_playerMove.VelocityRb.y);
    }

    // Move
    public void SetInputMove(Vector3 vector)
    {
        SetInputHorizontal(vector.x);
        SetInputVertical(vector.z);
    }
    public void SetInputHorizontal(float value)
    {
        _animator.SetFloat("InputLateral", value);
    }
    public void SetInputVertical(float value)
    {
        _animator.SetFloat("InputForward", value);
    }
    public void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
    public void SetVelocityY(float velocityY)
    {
        _animator.SetFloat("VelocityY", velocityY);
    }
    // Horizontal
    public void SetIdle(bool value)
    {
        _animator.SetBool("IsIdle", value);
    }
    public void SetWalking(bool value)
    {
        _animator.SetBool("IsJogging", value);
    }
    public void SetRunning(bool value)
    {
        _animator.SetBool("IsRunning", value);
    }
    public void SetSneacking(bool value)
    {
        _animator.SetBool("IsSneaking", value);
    }
    // Vertical
    public void SetFalling(bool value)
    {
        _animator.SetBool("IsFalling", value);
    }
    public void SetGrounded(bool value)
    {
        _animator.SetBool("IsGrounded", value);
    }
    public void SetJumping(bool value)
    {
        _animator.SetBool("IsJumping", value);
    }
    // Attack
    public void SetAttack01(bool value)
    {
        _animator.SetBool("IsAttackingAxe", value);
    }
    public void SetAttack02(bool value)
    {
        _animator.SetBool("IsAttackingKick", value);
    }
    public void SetProtection(bool value)
    {
        _animator.SetBool("IsProtected", value);
    }
}
