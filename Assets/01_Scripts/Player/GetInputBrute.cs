using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ButtonInput
{
    #region Public properties

    [SerializeField] public bool IsActive { get; }
    [SerializeField] public bool IsDown { get; }
    [SerializeField] public bool IsUp { get; }
    [SerializeField] public bool IsDoubleTap { get; }

    #endregion


    #region Constructors

    public ButtonInput(bool isActive, bool isDown, bool isUp, bool isDoubleTap)
    {
        IsActive = isActive;
        IsDown = isDown;
        IsUp = isUp;
        IsDoubleTap = isDoubleTap;
    }

    #endregion
}

public class GetInputBrute : MonoBehaviour
{
    [SerializeField] private float _doubleTapDelay;
    [SerializeField] private Vector3 _movementInput;
    [SerializeField] private float _triggerLeft, _triggerRight;

    private void Update()
    {
        // Move
        _horizontalInput = GetButtonInput("Horizontal");
        _verticalInput = GetButtonInput("Vertical");
        _movementInput = new Vector3()
        {
            x = Input.GetAxis("Horizontal"),
            y = 0,
            z = Input.GetAxis("Vertical")
        };
        _sneakInput = GetButtonInput("Sneak");
        _runInput = GetButtonInput("Run");
        _jumpInput = GetButtonInput("Jump");

        // Attack
        _attack01Input = GetButtonInput("Attack01");
        _attack02Input = GetButtonInput("Attack02");
        _triggerLeft = Input.GetAxis("Attack02");
        _triggerRight = Input.GetAxis("Attack01");
        _protectionInput = GetButtonInput("Protection");
        _dodgeInput = GetButtonInput("Dodge");

        // Other
        _useInput = GetButtonInput("Use");
    }

    private ButtonInput GetButtonInput(string buttonName)
    {
        bool isDoubleTap = false;
        if (_doubleTapTimes.TryGetValue(buttonName, out float doubleTapTime))
        {
            if (Input.GetButtonDown(buttonName))
            {
                isDoubleTap = Time.time < doubleTapTime;
                doubleTapTime = Time.time + _doubleTapDelay;
                _doubleTapTimes[buttonName] = doubleTapTime;
            }
        }
        else
        {
            isDoubleTap = false;
            doubleTapTime = Time.time + _doubleTapDelay;
            _doubleTapTimes.Add(buttonName, doubleTapTime);
        }

        return new ButtonInput(
            Input.GetButton(buttonName),
            Input.GetButtonDown(buttonName),
            Input.GetButtonUp(buttonName),
            isDoubleTap);
    }

    public ButtonInput HorizontalInput
    {
        get => _horizontalInput;
    }
    public ButtonInput VerticalInput
    {
        get => _verticalInput;
    }
    public Vector3 Movement
    {
        get => _movementInput;
    }
    public ButtonInput SneakInput
    {
        get => _sneakInput;
    }
    public ButtonInput RunInput
    {
        get => _runInput;
    }
    public ButtonInput JumpInput
    {
        get => _jumpInput;
    }

    public ButtonInput Attack01Input
    {
        get => _attack01Input;
    }
    public ButtonInput Attack02Input
    {
        get => _attack02Input;
    }
    public ButtonInput ProtectionInput
    {
        get => _protectionInput;
    }
    public ButtonInput DodgeInput
    {
        get => _dodgeInput;
    }

    public ButtonInput UseInput
    {
        get => _useInput;
    }
    public float TriggerLeft { get => _triggerLeft; set => _triggerLeft = value; }
    public float TriggerRight { get => _triggerRight; set => _triggerRight = value; }

    // Move
    [SerializeField] private ButtonInput _horizontalInput;
    [SerializeField] private ButtonInput _verticalInput;
    [SerializeField] private ButtonInput _sneakInput;
    [SerializeField] private ButtonInput _runInput;
    [SerializeField] private ButtonInput _jumpInput;

    // Attack
    [SerializeField] private ButtonInput _attack01Input;
    [SerializeField] private ButtonInput _attack02Input;
    [SerializeField] private ButtonInput _protectionInput;
    [SerializeField] private ButtonInput _dodgeInput;

    // Other
    [SerializeField] private ButtonInput _useInput;

    // Dico
    private Dictionary<string, float> _doubleTapTimes = new Dictionary<string, float>();
}
