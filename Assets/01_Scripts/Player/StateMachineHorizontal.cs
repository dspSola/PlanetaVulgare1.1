using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerHorizontalState
{
    IDLE,
    SNEAKING,
    JOGGING,
    RUNNING
}

public class StateMachineHorizontal : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private BruteAnimatorController _bruteAnimatorController;
    [SerializeField] private Collider _colliderIdle, _colliderSneak;
    [SerializeField] private CollisionOverlapBoxTester _sneackFloorCheck;

    [SerializeField] private bool _isSneaking;
    [SerializeField] private bool _afficheDebug;
    public PlayerHorizontalState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    private void Start()
    {
        TransitionToState(_currentState, PlayerHorizontalState.IDLE);
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

    private void OnStateEnter(PlayerHorizontalState state)
    {
        switch (state)
        {
            case PlayerHorizontalState.IDLE:
                DoIdleEnter();
                break;

            case PlayerHorizontalState.SNEAKING:
                DoSneakingEnter();
                break;

            case PlayerHorizontalState.JOGGING:
                DoJoggingEnter();
                break;

            case PlayerHorizontalState.RUNNING:
                DoRunningEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(PlayerHorizontalState state)
    {
        switch (state)
        {
            case PlayerHorizontalState.IDLE:
                DoIdleExit();
                break;

            case PlayerHorizontalState.SNEAKING:
                DoSneakingExit();
                break;

            case PlayerHorizontalState.JOGGING:
                DoJoggingExit();
                break;

            case PlayerHorizontalState.RUNNING:
                DoRunningExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(PlayerHorizontalState state)
    {
        switch (state)
        {
            case PlayerHorizontalState.IDLE:
                DoIdleUpdate();
                break;

            case PlayerHorizontalState.SNEAKING:
                DoSneakingUpdate();
                break;

            case PlayerHorizontalState.JOGGING:
                DoJoggingUpdate();
                break;

            case PlayerHorizontalState.RUNNING:
                DoRunningUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(PlayerHorizontalState fromState, PlayerHorizontalState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(PlayerHorizontalState toState)
    {
        TransitionToState(_currentState, toState);
    }
    #endregion

    #region State Idle

    private void DoIdleEnter()
    {
        _playerMove.DoIdle();
        _bruteAnimatorController.SetIdle(true);
    }

    private void DoIdleExit()
    {
        _bruteAnimatorController.SetIdle(false);
    }

    private void DoIdleUpdate()
    {
        if (_getBruteInput.SneakInput.IsUp)
        {
            _isSneaking = true;
        }
        if (_isSneaking)
        {
            TransitionToState(PlayerHorizontalState.SNEAKING);
            return;
        }

        if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        {
            if (_getBruteInput.RunInput.IsUp && _getBruteInput.Movement.z > 0.01f)
            {
                TransitionToState(PlayerHorizontalState.RUNNING);
                return;
            }

            TransitionToState(PlayerHorizontalState.JOGGING);
            return;
        }
    }

    #endregion


    #region State Sneaking

    private void DoSneakingEnter()
    {
        _playerMove.DoSneak();
        _bruteAnimatorController.SetSneacking(true);
        _colliderIdle.enabled = false;
        _colliderSneak.enabled = true;
    }

    private void DoSneakingExit()
    {
        _bruteAnimatorController.SetSneacking(false);
        _colliderSneak.enabled = false;
        _colliderIdle.enabled = true;
    }

    private void DoSneakingUpdate()
    {
        if (_stateMachineAttack.CurrentState == PlayerAttackState.IDLE)
        {
            if ((_getBruteInput.SneakInput.IsUp || _getBruteInput.RunInput.IsActive) && (!_sneackFloorCheck.TestCollision()))
            {
                _isSneaking = false;
            }
            if (!_isSneaking)
            {
                if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
                {
                    if (_getBruteInput.RunInput.IsActive)
                    {
                        TransitionToState(PlayerHorizontalState.RUNNING);
                        return;
                    }

                    TransitionToState(PlayerHorizontalState.JOGGING);
                    return;
                }
                else
                {
                    TransitionToState(PlayerHorizontalState.IDLE);
                    return;
                }
            }
        }
        //else if(_stateMachineAttack.CurrentState == PlayerAttackState.DODGE)
        //{
        //    if ((_getBruteInput.SneakInput.IsUp || _getBruteInput.RunInput.IsActive) && (!_sneackFloorCheck.TestCollision()))
        //    {
        //        _isSneaking = false;
        //    }
        //}
    }

    #endregion


    #region State Jogging

    private void DoJoggingEnter()
    {
        _playerMove.DoJog();
        _bruteAnimatorController.SetWalking(true);
    }

    private void DoJoggingExit()
    {
        _bruteAnimatorController.SetWalking(false);
    }

    private void DoJoggingUpdate()
    {
        if (_stateMachineAttack.CurrentState == PlayerAttackState.IDLE)
        {
            if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
            {
                if (_getBruteInput.SneakInput.IsUp)
                {
                    _isSneaking = true;
                }
                if (_isSneaking)
                {
                    TransitionToState(PlayerHorizontalState.SNEAKING);
                    return;
                }

                if (_getBruteInput.RunInput.IsActive && _getBruteInput.Movement.z > 0.01f)
                {
                    TransitionToState(PlayerHorizontalState.RUNNING);
                    return;
                }
            }
            else
            {
                TransitionToState(PlayerHorizontalState.IDLE);
                return;
            }
        }
    }

    #endregion


    #region State Running

    private void DoRunningEnter()
    {
        _playerMove.DoRun();
        _bruteAnimatorController.SetRunning(true);
    }

    private void DoRunningExit()
    {
        _bruteAnimatorController.SetRunning(false);
    }

    private void DoRunningUpdate()
    {
        if (_stateMachineAttack.CurrentState == PlayerAttackState.IDLE)
        {
            if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
            {
                if (_getBruteInput.SneakInput.IsUp)
                {
                    _isSneaking = true;
                }
                if (_isSneaking)
                {
                    TransitionToState(PlayerHorizontalState.SNEAKING);
                    return;
                }

                if (_getBruteInput.RunInput.IsUp)
                {
                    TransitionToState(PlayerHorizontalState.JOGGING);
                    return;
                }

                if (_getBruteInput.Movement.z < 0.01f)
                {
                    TransitionToState(PlayerHorizontalState.JOGGING);
                    return;
                }
            }
            else
            {
                TransitionToState(PlayerHorizontalState.IDLE);
                return;
            }
        }
    }

    #endregion


    #region Debug

    private void OnGUI()
    {
        if (_afficheDebug)
        {
            if (_style == null)
            {
                _style = new GUIStyle("button");
                _style.fontSize = 24;
                _style.alignment = TextAnchor.MiddleLeft;
                _style.padding = new RectOffset(15, 15, 0, 0);
            }
            using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.1f, Screen.width * 0.2f, Screen.height * 0.1f)))
            {
                using (new GUILayout.VerticalScope())
                {
                    GUILayout.Button($"HState: {_currentState}", _style, GUILayout.ExpandHeight(true));
                }
            }
        }
    }

    public void SetTransitionToIdle()
    {
        TransitionToState(PlayerHorizontalState.IDLE);
    }

    private GUIStyle _style;

    #endregion


    #region Private

    private PlayerHorizontalState _currentState;

    #endregion
}
