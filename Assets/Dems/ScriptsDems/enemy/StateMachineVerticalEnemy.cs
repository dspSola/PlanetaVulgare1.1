using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyVerticalState
{
    GROUNDED,
    JUMPING,
    FALLING,
}


public class StateMachineVerticalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyVerticalState _currentState;
    [SerializeField] private CollisionOverlapBoxTester _groundCheck;

    #region Public properties

    public EnemyVerticalState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        TransitionToState(_currentState, EnemyVerticalState.FALLING);
    }

    private void Update()
    {
        DoUpdate();
    }

    public void DoUpdate()
    {
        OnStateUpdate(_currentState);
    }
    #endregion

    #region State Machine

    private void OnStateEnter(EnemyVerticalState state)
    {
        switch (state)
        {
            case EnemyVerticalState.GROUNDED:
                DoGroundedEnter();
                break;

            case EnemyVerticalState.JUMPING:
                DoJumpingEnter();
                break;

            case EnemyVerticalState.FALLING:
                DoFallingEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(EnemyVerticalState state)
    {
        switch (state)
        {
            case EnemyVerticalState.GROUNDED:
                DoGroundedExit();
                break;

            case EnemyVerticalState.JUMPING:
                DoJumpingExit();
                break;

            case EnemyVerticalState.FALLING:
                DoFallingExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(EnemyVerticalState state)
    {
        switch (state)
        {
            case EnemyVerticalState.GROUNDED:
                DoGroundedUpdate();
                break;

            case EnemyVerticalState.JUMPING:
                DoJumpingUpdate();
                break;

            case EnemyVerticalState.FALLING:
                DoFallingUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(EnemyVerticalState fromState, EnemyVerticalState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(EnemyVerticalState toState)
    {
        TransitionToState(_currentState, toState);
    }

    #endregion

    #region State Grounded

    private void DoGroundedEnter()
    {
        //_bruteAnimatorController.SetGrounded(true);
    }

    private void DoGroundedExit()
    {
        //_bruteAnimatorController.SetGrounded(false);
    }

    private void DoGroundedUpdate()
    {
        if (!_groundCheck.TestCollision())
        {
            TransitionToState(EnemyVerticalState.FALLING);
            return;
        }

        //if (_getBruteInput.JumpInput.IsActive)
        //{
        //    TransitionToState(EnemyVerticalState.JUMPING);
        //    return;
        //}
    }

    #endregion

    #region State Jumping

    private void DoJumpingEnter()
    {
        //_playerMove.DoJump();
        //_bruteAnimatorController.SetJumping(true);
    }

    private void DoJumpingExit()
    {
        //_bruteAnimatorController.SetJumping(false);
    }

    private void DoJumpingUpdate()
    {
        //if (_playerMove.VelocityRb.y < -0.01f)
        //{
        //    TransitionToState(EnemyVerticalState.FALLING);
        //    return;
        //}
    }

    #endregion

    #region State Falling

    private void DoFallingEnter()
    {
        //_bruteAnimatorController.SetFalling(true);
        Debug.Log(" DoFallingEnter ");
    }

    private void DoFallingExit()
    {
        //_bruteAnimatorController.SetFalling(false);
        Debug.Log(" DoFallingEXIT ");

    }

    private void DoFallingUpdate()
    {
        if (_groundCheck.TestCollision())
        {
            TransitionToState(EnemyVerticalState.GROUNDED);
            return;
        }
    }

    #endregion



    #region Private

   

    #endregion
}
