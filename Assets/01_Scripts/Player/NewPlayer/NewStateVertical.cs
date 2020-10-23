using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewPlayerVerticalState
{
    GROUNDED,
    JUMPING,
    FALLING,
}

public class NewStateVertical : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private NewStateAttack _newStateAttack;
    [SerializeField] private NewPlayerMoverController _newPlayerMoverController;

    [SerializeField] private CollisionOverlapBoxTester _groundCheck;

    #region Public properties

    public NewPlayerVerticalState CurrentState
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
        TransitionToState(_currentState, NewPlayerVerticalState.FALLING);
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

    private void OnStateEnter(NewPlayerVerticalState state)
    {
        switch (state)
        {
            case NewPlayerVerticalState.GROUNDED:
                DoGroundedEnter();
                break;

            case NewPlayerVerticalState.JUMPING:
                DoJumpingEnter();
                break;

            case NewPlayerVerticalState.FALLING:
                DoFallingEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(NewPlayerVerticalState state)
    {
        switch (state)
        {
            case NewPlayerVerticalState.GROUNDED:
                DoGroundedExit();
                break;

            case NewPlayerVerticalState.JUMPING:
                DoJumpingExit();
                break;

            case NewPlayerVerticalState.FALLING:
                DoFallingExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(NewPlayerVerticalState state)
    {
        switch (state)
        {
            case NewPlayerVerticalState.GROUNDED:
                DoGroundedUpdate();
                break;

            case NewPlayerVerticalState.JUMPING:
                DoJumpingUpdate();
                break;

            case NewPlayerVerticalState.FALLING:
                DoFallingUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(NewPlayerVerticalState fromState, NewPlayerVerticalState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(NewPlayerVerticalState toState)
    {
        TransitionToState(_currentState, toState);
    }

    #endregion


    #region State Grounded

    private void DoGroundedEnter()
    {

    }

    private void DoGroundedExit()
    {

    }

    private void DoGroundedUpdate()
    {
        if (!_groundCheck.TestCollision())
        {
            TransitionToState(NewPlayerVerticalState.FALLING);
            return;
        }
    }

    #endregion


    #region State Jumping

    private void DoJumpingEnter()
    {

    }

    private void DoJumpingExit()
    {

    }

    private void DoJumpingUpdate()
    {

    }

    #endregion


    #region State Falling

    private void DoFallingEnter()
    {

    }

    private void DoFallingExit()
    {

    }

    private void DoFallingUpdate()
    {
        if (_groundCheck.TestCollision())
        {
            TransitionToState(NewPlayerVerticalState.GROUNDED);
            return;
        }
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
        using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.1f)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"VState: {_currentState}", _style, GUILayout.ExpandHeight(true));
            }
        }
    }

    public void SetTransitionToJumping()
    {
        if (_currentState == NewPlayerVerticalState.GROUNDED)
        {
            TransitionToState(NewPlayerVerticalState.JUMPING);
            return;
        }
    }

    private GUIStyle _style;

    #endregion


    #region Private

    private NewPlayerVerticalState _currentState;

    #endregion
}
