using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackState
{
    IDLE,
    ATTACK01,
    ATTACK02,
    PROTECTION
}

public class StateMachineAttack : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private BruteAnimatorController _bruteAnimatorController;
    [SerializeField] private bool _isAnim, _canSlice;
    public PlayerAttackState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    #region Unity Lifecycle

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

    private void OnStateEnter(PlayerAttackState state)
    {
        switch (state)
        {
            case PlayerAttackState.IDLE:
                DoIdleEnter();
                break;

            case PlayerAttackState.ATTACK01:
                DoATTACK01Enter();
                break;

            case PlayerAttackState.ATTACK02:
                DoATTACK02Enter();
                break;

            case PlayerAttackState.PROTECTION:
                DoPROTECTIONEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(PlayerAttackState state)
    {
        switch (state)
        {
            case PlayerAttackState.IDLE:
                DoIdleExit();
                break;

            case PlayerAttackState.ATTACK01:
                DoATTACK01Exit();
                break;

            case PlayerAttackState.ATTACK02:
                DoATTACK02Exit();
                break;

            case PlayerAttackState.PROTECTION:
                DoPROTECTIONExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(PlayerAttackState state)
    {
        switch (state)
        {
            case PlayerAttackState.IDLE:
                DoIdleUpdate();
                break;

            case PlayerAttackState.ATTACK01:
                DoATTACK01Update();
                break;

            case PlayerAttackState.ATTACK02:
                DoATTACK02Update();
                break;

            case PlayerAttackState.PROTECTION:
                DoPROTECTIONUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(PlayerAttackState fromState, PlayerAttackState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(PlayerAttackState toState)
    {
        TransitionToState(_currentState, toState);
    }

    #endregion

    #region Update State Machine
    // IDLE
    private void DoIdleEnter()
    {

    }
    private void DoIdleExit()
    {

    }
    private void DoIdleUpdate()
    {
        if (_getBruteInput.Attack01Input.IsActive)
        {
            TransitionToState(PlayerAttackState.ATTACK01);
            return;
        }
        if (_getBruteInput.Attack02Input.IsActive)
        {
            TransitionToState(PlayerAttackState.ATTACK02);
            return;
        }
        if (_getBruteInput.ProtectionInput.IsActive)
        {
            TransitionToState(PlayerAttackState.PROTECTION);
            return;
        }
    }

    // ATTACK01
    private void DoATTACK01Enter()
    {
        _bruteAnimatorController.SetAttack01(true);
    }
    private void DoATTACK01Exit()
    {
        _bruteAnimatorController.SetAttack01(false);
    }
    private void DoATTACK01Update()
    {
        if (!_isAnim)
        {
            TransitionToState(PlayerAttackState.IDLE);
            return;
        }
    }

    // ATTACK02
    private void DoATTACK02Enter()
    {
        _bruteAnimatorController.SetAttack02(true);
    }
    private void DoATTACK02Exit()
    {
        _bruteAnimatorController.SetAttack02(false);
    }
    private void DoATTACK02Update()
    {
        if (!_isAnim)
        {
            TransitionToState(PlayerAttackState.IDLE);
            return;
        }
    }

    // PROTECTION
    private void DoPROTECTIONEnter()
    {
        _bruteAnimatorController.SetProtection(true);
    }

    private void DoPROTECTIONExit()
    {
        _bruteAnimatorController.SetProtection(false);
    }

    private void DoPROTECTIONUpdate()
    {
        if(_getBruteInput.ProtectionInput.IsUp)
        {
            TransitionToState(PlayerAttackState.IDLE);
            return;
        }
    }

    #endregion

    private void OnGUI()
    {
        if (_style == null)
        {
            _style = new GUIStyle("button");
            _style.fontSize = 24;
            _style.alignment = TextAnchor.MiddleLeft;
            _style.padding = new RectOffset(15, 15, 0, 0);
        }
        using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.1f)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"HState: {_currentState}", _style, GUILayout.ExpandHeight(true));
            }
        }
        using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"IsAnim: {_isAnim}", _style, GUILayout.ExpandHeight(true));
            }
        }
    }

    public bool IsAnim { get => _isAnim; set => _isAnim = value; }
    public bool CanSlice { get => _canSlice; set => _canSlice = value; }

    private GUIStyle _style;

    private PlayerAttackState _currentState;
}
