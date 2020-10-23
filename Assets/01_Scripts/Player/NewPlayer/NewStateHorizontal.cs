using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewPlayerHorizontalState
{
    IDLE,
    SNEAKING,
    JOGGING,
    RUNNING
}

public class NewStateHorizontal : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private NewStateAttack _newStateAttack;
    [SerializeField] private NewPlayerMoverController _newPlayerMoverController;

    [SerializeField] private bool _isSneaking;

    public NewPlayerHorizontalState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    private void Start()
    {
        TransitionToState(_currentState, NewPlayerHorizontalState.IDLE);
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

    private void OnStateEnter(NewPlayerHorizontalState state)
    {
        switch (state)
        {
            case NewPlayerHorizontalState.IDLE:
                DoIdleEnter();
                break;

            case NewPlayerHorizontalState.SNEAKING:
                DoSneakingEnter();
                break;

            case NewPlayerHorizontalState.JOGGING:
                DoJoggingEnter();
                break;

            case NewPlayerHorizontalState.RUNNING:
                DoRunningEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(NewPlayerHorizontalState state)
    {
        switch (state)
        {
            case NewPlayerHorizontalState.IDLE:
                DoIdleExit();
                break;

            case NewPlayerHorizontalState.SNEAKING:
                DoSneakingExit();
                break;

            case NewPlayerHorizontalState.JOGGING:
                DoJoggingExit();
                break;

            case NewPlayerHorizontalState.RUNNING:
                DoRunningExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(NewPlayerHorizontalState state)
    {
        switch (state)
        {
            case NewPlayerHorizontalState.IDLE:
                DoIdleUpdate();
                break;

            case NewPlayerHorizontalState.SNEAKING:
                DoSneakingUpdate();
                break;

            case NewPlayerHorizontalState.JOGGING:
                DoJoggingUpdate();
                break;

            case NewPlayerHorizontalState.RUNNING:
                DoRunningUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(NewPlayerHorizontalState fromState, NewPlayerHorizontalState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(NewPlayerHorizontalState toState)
    {
        TransitionToState(_currentState, toState);
    }
    #endregion

    #region State Idle

    private void DoIdleEnter()
    {

    }

    private void DoIdleExit()
    {

    }

    private void DoIdleUpdate()
    {
        if (_getBruteInput.SneakInput.IsUp)
        {
            _isSneaking = true;
        }
        if (_isSneaking)
        {
            TransitionToState(NewPlayerHorizontalState.SNEAKING);
            return;
        }

        if (_getBruteInput.Movement.sqrMagnitude > 0.01f)
        {
            if (_getBruteInput.RunInput.IsUp && _getBruteInput.Movement.z > 0.01f)
            {
                TransitionToState(NewPlayerHorizontalState.RUNNING);
                return;
            }

            TransitionToState(NewPlayerHorizontalState.JOGGING);
            return;
        }
    }

    #endregion


    #region State Sneaking

    private void DoSneakingEnter()
    {

    }

    private void DoSneakingExit()
    {

    }

    private void DoSneakingUpdate()
    {

    }

    #endregion


    #region State Jogging

    private void DoJoggingEnter()
    {

    }

    private void DoJoggingExit()
    {

    }

    private void DoJoggingUpdate()
    {

    }

    #endregion


    #region State Running

    private void DoRunningEnter()
    {

    }

    private void DoRunningExit()
    {

    }

    private void DoRunningUpdate()
    {

    }

    #endregion


    #region Debug

    private void OnGUI()
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

    private GUIStyle _style;

    #endregion


    #region Private

    private NewPlayerHorizontalState _currentState;

    #endregion
}
