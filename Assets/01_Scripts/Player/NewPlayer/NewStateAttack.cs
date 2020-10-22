using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NewPlayerAttackState
{
    IDLE,
    CHANGEWEAPON,
    ATTACK01,
    ATTACK02,
    PROTECTION,
    DODGE
}

public class NewStateAttack : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private NewPlayerMoverController _newPlayerMoverController;

    [SerializeField] private bool _isArmed, _isInCombo;
    [SerializeField] private int _cptCombo;
    [SerializeField] private float _timeCombo, _timeComboMax;
    public NewPlayerAttackState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    #region Unity Lifecycle

    private void Awake()
    {

    }

    private void Start()
    {
        TransitionToState(_currentState, NewPlayerAttackState.IDLE);
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

    private void OnStateEnter(NewPlayerAttackState state)
    {
        switch (state)
        {
            case NewPlayerAttackState.IDLE:
                DoIdleEnter();
                break;

            case NewPlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponEnter();
                break;

            case NewPlayerAttackState.ATTACK01:
                DoATTACK01Enter();
                break;

            case NewPlayerAttackState.ATTACK02:
                DoATTACK02Enter();
                break;

            case NewPlayerAttackState.PROTECTION:
                DoPROTECTIONEnter();
                break;
            case NewPlayerAttackState.DODGE:
                DoDODGEEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(NewPlayerAttackState state)
    {
        switch (state)
        {
            case NewPlayerAttackState.IDLE:
                DoIdleExit();
                break;

            case NewPlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponExit();
                break;

            case NewPlayerAttackState.ATTACK01:
                DoATTACK01Exit();
                break;

            case NewPlayerAttackState.ATTACK02:
                DoATTACK02Exit();
                break;

            case NewPlayerAttackState.PROTECTION:
                DoPROTECTIONExit();
                break;
            case NewPlayerAttackState.DODGE:
                DoDODGEExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(NewPlayerAttackState state)
    {
        switch (state)
        {
            case NewPlayerAttackState.IDLE:
                DoIdleUpdate();
                break;

            case NewPlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponUpdate();
                break;

            case NewPlayerAttackState.ATTACK01:
                DoATTACK01Update();
                break;

            case NewPlayerAttackState.ATTACK02:
                DoATTACK02Update();
                break;

            case NewPlayerAttackState.PROTECTION:
                DoPROTECTIONUpdate();
                break;

            case NewPlayerAttackState.DODGE:
                DoDODGEUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(NewPlayerAttackState fromState, NewPlayerAttackState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void TransitionToState(NewPlayerAttackState toState)
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

    }

    // CHANGEWEAPON
    private void DoChangeWeaponEnter()
    {

    }
    private void DoChangeWeaponExit()
    {

    }
    private void DoChangeWeaponUpdate()
    {

    }

    // ATTACK01
    private void DoATTACK01Enter()
    {

    }
    private void DoATTACK01Exit()
    {

    }
    private void DoATTACK01Update()
    {

    }

    // ATTACK02
    private void DoATTACK02Enter()
    {

    }
    private void DoATTACK02Exit()
    {

    }
    private void DoATTACK02Update()
    {

    }

    // PROTECTION
    private void DoPROTECTIONEnter()
    {

    }
    private void DoPROTECTIONExit()
    {

    }
    private void DoPROTECTIONUpdate()
    {
        if (_getBruteInput.ProtectionInput.IsUp)
        {
            TransitionToState(NewPlayerAttackState.IDLE);
            return;
        }
    }

    // DODGE
    private void DoDODGEEnter()
    {

    }
    private void DoDODGEExit()
    {

    }
    private void DoDODGEUpdate()
    {

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
    }

    public void SetTransition(NewPlayerAttackState pas)
    {
        TransitionToState(pas);
    }

    private GUIStyle _style;

    private NewPlayerAttackState _currentState;
}
