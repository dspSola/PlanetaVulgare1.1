using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackState
{
    IDLE,
    CHANGEWEAPON,
    ATTACK01,
    ATTACK02,
    PROTECTION,
    DODGE
}

public class StateMachineAttack : MonoBehaviour
{
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private BruteAnimatorController _bruteAnimatorController;
    [SerializeField] private WeaponColliderManager _weaponColliderManager;
    [SerializeField] private Collider _colliderIdle, _colliderDodge, _colliderProtection;
    [SerializeField] private GameObject _weaponMesh, _weaponAxeCollider, _weaponAllCollider, _weaponBackMesh;
    [SerializeField] private bool _isArmed, _isAnim, _canSlice;

    [SerializeField] private bool _isInCombo;
    [SerializeField] private int _cptCombo;
    [SerializeField] private float _timeCombo, _timeComboMax;

    public PlayerAttackState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    #region Unity Lifecycle

    private void Awake()
    {
        SetActiveWeapon(false);
    }

    private void Start()
    {
        TransitionToState(_currentState, PlayerAttackState.IDLE);
    }

    private void Update()
    {
        DoUpdate();
        GestionTimeCombo();
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

            case PlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponEnter();
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
            case PlayerAttackState.DODGE:
                DoDODGEEnter();
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

            case PlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponExit();
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
            case PlayerAttackState.DODGE:
                DoDODGEExit();
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

            case PlayerAttackState.CHANGEWEAPON:
                DoChangeWeaponUpdate();
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

            case PlayerAttackState.DODGE:
                DoDODGEUpdate();
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
        _weaponAllCollider.GetComponent<WeaponColliderManager>().enabled = false;
        _weaponAllCollider.GetComponent<MeshCollider>().enabled = false;
    }
    private void DoIdleExit()
    {
        _weaponAllCollider.GetComponent<WeaponColliderManager>().enabled = true;
        _weaponAllCollider.GetComponent<MeshCollider>().enabled = true;
    }
    private void DoIdleUpdate()
    {
        if (_getBruteInput.Attack01Input.IsActive || _getBruteInput.TriggerRight == 1)
        {
            if (_isArmed)
            {
                TransitionToState(PlayerAttackState.ATTACK01);
                return;
            }
            else
            {
                TransitionToState(PlayerAttackState.CHANGEWEAPON);
                return;
            }
        }
        if (_getBruteInput.Attack02Input.IsActive || _getBruteInput.TriggerLeft == 1)
        {
            TransitionToState(PlayerAttackState.ATTACK02);
            return;
        }
        if (_getBruteInput.ProtectionInput.IsActive)
        {
            if (_isArmed)
            {
                TransitionToState(PlayerAttackState.PROTECTION);
                return;
            }
            else
            {
                TransitionToState(PlayerAttackState.CHANGEWEAPON);
                return;
            }
        }
        if (_getBruteInput.DodgeInput.IsActive)
        {
            TransitionToState(PlayerAttackState.DODGE);
            return;
        }

        if (_getBruteInput.UseInput.IsActive)
        {
            if (_isArmed)
            {
                TransitionToState(PlayerAttackState.CHANGEWEAPON);
                return;
            }
        }
    }

    // CHANGEWEAPON
    private void DoChangeWeaponEnter()
    {
        _bruteAnimatorController.SetChangeWeapon(true);
        _weaponColliderManager.SetSonChangeWeapon();
    }
    private void DoChangeWeaponExit()
    {

    }
    private void DoChangeWeaponUpdate()
    {
        if (!_isAnim)
        {
            TransitionToState(PlayerAttackState.IDLE);
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
        _colliderIdle.enabled = false;
        _colliderProtection.enabled = true;
        _bruteAnimatorController.SetProtection(true);
    }
    private void DoPROTECTIONExit()
    {
        _colliderProtection.enabled = false;
        _colliderIdle.enabled = true;
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

    // DODGE
    private void DoDODGEEnter()
    {
        _bruteAnimatorController.SetDodge(true);
        if(_getBruteInput.Movement.z >= 0)
        {
            _colliderIdle.enabled = false;
            _colliderDodge.enabled = true;
        }
    }
    private void DoDODGEExit()
    {
        _bruteAnimatorController.SetDodge(false);
        if(_colliderDodge.enabled)
        {
            _colliderDodge.enabled = false;
            _colliderIdle.enabled = true;
        }
    }
    private void DoDODGEUpdate()
    {
        if(_getBruteInput.DodgeInput.IsDown)
        {
            TransitionToState(PlayerAttackState.IDLE);
            return;
        }

        if (!_isAnim)
        {
            TransitionToState(PlayerAttackState.IDLE);
            return;
        }
    }

    public void GestionTimeCombo()
    {
        if(_cptCombo != 0 && !_isAnim)
        {
            if (_timeCombo < _timeComboMax)
            {
                _timeCombo += Time.deltaTime;
            }
            else
            {
                _cptCombo = 0;
                _timeCombo = 0;
                _isInCombo = false;
                _bruteAnimatorController.SetCptCombo(_cptCombo);
            }
        }
    }

    #endregion

    private void OnGUI()
    {
        //if (_style == null)
        //{
        //    _style = new GUIStyle("button");
        //    _style.fontSize = 24;
        //    _style.alignment = TextAnchor.MiddleLeft;
        //    _style.padding = new RectOffset(15, 15, 0, 0);
        //}
        //using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.1f)))
        //{
        //    using (new GUILayout.VerticalScope())
        //    {
        //        GUILayout.Button($"HState: {_currentState}", _style, GUILayout.ExpandHeight(true));
        //    }
        //}
        //using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f)))
        //{
        //    using (new GUILayout.VerticalScope())
        //    {
        //        GUILayout.Button($"IsAnim: {_isAnim}", _style, GUILayout.ExpandHeight(true));
        //    }
        //}
        //using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.1f)))
        //{
        //    using (new GUILayout.VerticalScope())
        //    {
        //        GUILayout.Button($"_isArmed: {_isArmed}", _style, GUILayout.ExpandHeight(true));
        //    }
        //}
        //using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.6f, Screen.width * 0.2f, Screen.height * 0.1f)))
        //{
        //    using (new GUILayout.VerticalScope())
        //    {
        //        GUILayout.Button($"_canSlice: {_canSlice}", _style, GUILayout.ExpandHeight(true));
        //    }
        //}
    }

    public void SetActiveWeapon(bool value)
    {
        if (_weaponMesh.activeSelf != value && _weaponAxeCollider.activeSelf != value && _weaponAllCollider.activeSelf != value)
        {
            _weaponMesh.SetActive(value);
            _weaponAxeCollider.SetActive(value);
            _weaponAllCollider.SetActive(value);
            _weaponBackMesh.SetActive(!value);
        }
    }

    public void AddForce(Vector3 valueForce)
    {
        _playerMove.AddForce(valueForce);
    }
    public void AddForce(float valueXZ, float valueY)
    {
        _playerMove.AddForce(valueXZ, valueY);
    }

    public void SetTransition(PlayerAttackState pas)
    {
        TransitionToState(pas);
    }

    public bool IsAnim { get => _isAnim; set => _isAnim = value; }
    public bool CanSlice { get => _canSlice; set => _canSlice = value; }
    public bool IsArmed { get => _isArmed; set => _isArmed = value; }
    public int CptCombo { get => _cptCombo; set => _cptCombo = value; }

    private GUIStyle _style;

    private PlayerAttackState _currentState;
}
