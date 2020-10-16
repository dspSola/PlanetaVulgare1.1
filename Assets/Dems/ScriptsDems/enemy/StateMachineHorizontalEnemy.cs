using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyHorizontalState
{
    IDLE,
    SNEAKING,
    JOGGING,
    RUNNING
}


public class StateMachineHorizontalEnemy : MonoBehaviour
{

    [SerializeField] private EnemyHorizontalState _currentState;

    public EnemyHorizontalState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    private void Start()
    {
        TransitionToState(_currentState, EnemyHorizontalState.IDLE);
    }

    private void Update()
    {
        DoUpdate();
    }

    public void DoUpdate()
    {
        OnStateUpdate(_currentState);
    }

    #region State Machine

    private void OnStateEnter(EnemyHorizontalState state)
    {
        switch (state)
        {
            case EnemyHorizontalState.IDLE:
                DoIdleEnter();
                break;

            case EnemyHorizontalState.SNEAKING:
                DoSneakingEnter();
                break;

            case EnemyHorizontalState.JOGGING:
                DoJoggingEnter();
                break;

            case EnemyHorizontalState.RUNNING:
                DoRunningEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(EnemyHorizontalState state)
    {
        switch (state)
        {
            case EnemyHorizontalState.IDLE:
                DoIdleExit();
                break;

            case EnemyHorizontalState.SNEAKING:
                DoSneakingExit();
                break;

            case EnemyHorizontalState.JOGGING:
                DoJoggingExit();
                break;

            case EnemyHorizontalState.RUNNING:
                DoRunningExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(EnemyHorizontalState state)
    {
        switch (state)
        {
            case EnemyHorizontalState.IDLE:
                DoIdleUpdate();
                break;

            case EnemyHorizontalState.SNEAKING:
                DoSneakingUpdate();
                break;

            case EnemyHorizontalState.JOGGING:
                DoJoggingUpdate();
                break;

            case EnemyHorizontalState.RUNNING:
                DoRunningUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(EnemyHorizontalState fromState, EnemyHorizontalState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(EnemyHorizontalState toState)
    {
        TransitionToState(_currentState, toState);
    }
    #endregion

    #region State Idle

    private void DoIdleEnter()
    {
        //_playerMove.DoIdle();
        //_bruteAnimatorController.SetIdle(true);
    }

    private void DoIdleExit()
    {
        //_bruteAnimatorController.SetIdle(false);
    }

    private void DoIdleUpdate()
    {
        //if (_getBruteInput.SneakInput.IsActive)
        //{
        //    TransitionToState(EnemyHorizontalState.SNEAKING);
        //    return;
        //}

        //if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        //{
        //    if (_getBruteInput.RunInput.IsActive && _getBruteInput.Movement.z > 0.01f)
        //    {
        //        TransitionToState(EnemyHorizontalState.RUNNING);
        //        return;
        //    }

        //    TransitionToState(EnemyHorizontalState.JOGGING);
        //    return;
        //}
    }

    #endregion


    #region State Sneaking

    private void DoSneakingEnter()
    {
        //_playerMove.DoSneak();
        //_bruteAnimatorController.SetSneacking(true);
    }

    private void DoSneakingExit()
    {
        //_bruteAnimatorController.SetSneacking(false);
    }

    private void DoSneakingUpdate()
    {
        //if (_getBruteInput.SneakInput.IsUp)
        //{
        //    if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        //    {
        //        if (_getBruteInput.RunInput.IsActive)
        //        {
        //            TransitionToState(EnemyHorizontalState.RUNNING);
        //            return;
        //        }

        //        TransitionToState(EnemyHorizontalState.JOGGING);
        //        return;
        //    }
        //    else
        //    {
        //        TransitionToState(EnemyHorizontalState.IDLE);
        //        return;
        //    }
        //}
    }

    #endregion


    #region State Jogging

    private void DoJoggingEnter()
    {
        //_playerMove.DoJog();
        //_bruteAnimatorController.SetWalking(true);
    }

    private void DoJoggingExit()
    {
        //_bruteAnimatorController.SetWalking(false);
    }

    private void DoJoggingUpdate()
    {
        //if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        //{
        //    if (_getBruteInput.SneakInput.IsActive)
        //    {
        //        TransitionToState(EnemyHorizontalState.SNEAKING);
        //        return;
        //    }

        //    if (_getBruteInput.RunInput.IsActive && _getBruteInput.Movement.z > 0.01f)
        //    {
        //        TransitionToState(EnemyHorizontalState.RUNNING);
        //        return;
        //    }
        //}
        //else
        //{
        //    TransitionToState(EnemyHorizontalState.IDLE);
        //    return;
        //}
    }

    #endregion


    #region State Running

    private void DoRunningEnter()
    {
        //_playerMove.DoRun();
        //_bruteAnimatorController.SetRunning(true);
    }

    private void DoRunningExit()
    {
        //_bruteAnimatorController.SetRunning(false);
    }

    private void DoRunningUpdate()
    {
        //if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        //{
        //    if (_getBruteInput.SneakInput.IsActive)
        //    {
        //        TransitionToState(EnemyHorizontalState.SNEAKING);
        //        return;
        //    }

        //    if (_getBruteInput.RunInput.IsUp)
        //    {
        //        TransitionToState(EnemyHorizontalState.JOGGING);
        //        return;
        //    }

        //    if (_getBruteInput.Movement.z < 0.01f)
        //    {
        //        TransitionToState(EnemyHorizontalState.JOGGING);
        //        return;
        //    }
        //}
        //else
        //{
        //    TransitionToState(EnemyHorizontalState.IDLE);
        //    return;
        //}
    }

    #endregion


    #region Private

    

    #endregion
}
