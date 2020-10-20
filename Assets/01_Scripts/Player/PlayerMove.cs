using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private ScriptableTransform _scriptableTransform;
    [SerializeField] private GetInputBrute _getBruteInput;
    [SerializeField] private StateMachineVertical _stateMachineVertical;
    [SerializeField] private StateMachineHorizontal _stateMachineHorizontal;
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Vector3 _transformedInput, _velocity, _horizontalVelocity, _verticalVelocity;
    [SerializeField] private float _movementQty, _currentSpeed;
    [SerializeField] private bool _doJump, _canApplyForceAnimation, _applyForceAnimation;

    [Header("Parameters")]

    [SerializeField] private float _jogSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _sneakSpeed;
    [SerializeField] private float _changeSpeedDuration;
    [SerializeField] private float _turnSpeed;
    [SerializeField][Range(0, 5)] private float _gravityFallMultiplier;

    [SerializeField] private float _jumpPower;

    [Header("Max speeds")]
    [SerializeField] private float _maxHorizontalSpeed;
    [SerializeField] private float _maxVerticalSpeed;
    [SerializeField] private Transform _cameraTransform;

    [Header("Ground testers")]
    [SerializeField] private CollisionRaycastTester _floorFinder;
    [SerializeField] private float _floorOffsetY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerData.Position = Vector3.zero;
    }

    private void Update()
    {
        // Horizontal velocity
        if (_stateMachineHorizontal.CurrentState != PlayerHorizontalState.IDLE && !_stateMachineAttack.IsAnim)
        {
            // On change le référentiel de _movementInput pour le passer dans le référentiel local au référentiel global
            // Attention, _movementInput n'est pas une direction car le vecteur n'est pas encore normalisé.
            _transformedInput = _transformPlayer.TransformVector(_getBruteInput.Movement);
            // On met le y à 0 pour éviter de se déplacer vers le haut ou le bas
            _transformedInput.y = 0;

            // On calcule la quantité de mouvement dans l'input pour pouvoir la rapporter mouvement ensuite
            // On clamp 0-1 la quantité de mouvement pour éviter de se déplacer trop vite en diagonale
            _movementQty = Mathf.Clamp01(_transformedInput.magnitude);

            // Enfin on normalize
            _transformedInput.Normalize();

            // Calcule le vecteur velocity horizontal à partir des inputs 
            _horizontalVelocity = _transformedInput * _currentSpeed * _movementQty;
        }
        else
        {
            //_horizontalVelocity = _transformedInput * _currentSpeed;
            _horizontalVelocity = Vector3.zero;
        }

        // Vertical velocity
        if (_doJump)
        {
            _verticalVelocity = Vector3.up * _jumpPower;
            _doJump = false;
        }

        if (_stateMachineVertical.CurrentState == PlayerVerticalState.GROUNDED)
        {
            _verticalVelocity = Vector3.zero;
        }
        else
        {
            // Si on n'est pas au sol, applique la gravité
            _verticalVelocity += Physics.gravity * _gravityFallMultiplier * Time.fixedDeltaTime;
        }

        if (_canApplyForceAnimation)
        {
            if (_applyForceAnimation)
            {
                _velocity = (_transformedInput * _currentSpeed * _movementQty) + _verticalVelocity;
            }
            else
            {
                _velocity.x = 0;
                _velocity.z = 0;
            }
            _rigidbody.velocity = _velocity;
        }

        _playerData.Position = _transformPlayer.position;
        _scriptableTransform.value = _transformPlayer.GetComponent<Transform>();
        _scriptableTransform.value.position = transform.position;
    }

    private void FixedUpdate()
    {
        //if (_stateMachineHorizontal.CurrentState != PlayerHorizontalState.IDLE)
        //{
        //    if (_getBruteInput.Movement.z > 0.25f)
        //    {
        //        RotateTowardsCameraForward();
        //    }
        //}

        if (_stateMachineAttack.CurrentState == PlayerAttackState.IDLE)
        {
            if (_getBruteInput.Movement.z > 0.5f || _getBruteInput.Movement.z < -0.5f)
            {
                RotateTowardsCameraForward();
            }
        }

        // Si on est au sol, on colle au sol
        if (_stateMachineVertical.CurrentState == PlayerVerticalState.GROUNDED)
        {
            StickToGround();
        }

        if (_canApplyForceAnimation)
        {

        }
        else
        {
            // Calcule le vecteur velocity à partir des velocity verticale et horizontale
            _velocity = _verticalVelocity + _horizontalVelocity;

            // Clamp les différentes vitesses
            _velocity.x = Mathf.Clamp(_velocity.x, -_maxHorizontalSpeed, _maxHorizontalSpeed);
            _velocity.y = Mathf.Clamp(_velocity.y, -_maxVerticalSpeed, _maxVerticalSpeed);
            _velocity.z = Mathf.Clamp(_velocity.z, -_maxHorizontalSpeed, _maxHorizontalSpeed);

            // Applique le vecteur velocity au rigidbody
            _rigidbody.velocity = _velocity;
        }
    }

    private void RotateTowardsCameraForward()
    {
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.y = 0;
        Quaternion desiredRotation = Quaternion.LookRotation(cameraForward);
        Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, desiredRotation, _turnSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(rotation);
    }

    private void StickToGround()
    {
        // Calcule la position y du sol
        _floorFinder.AverageCollision(out Vector3 floorPosition);

        // Calcule la position où doit se trouver le rigidbody en fonction du sol
        _rigidbodyOnFloorPosition = new Vector3(_rigidbody.position.x, floorPosition.y + _floorOffsetY, _rigidbody.position.z);

        // Si la position a changé
        if (!_rigidbodyOnFloorPosition.Approximately(_rigidbody.position))
        {
            // Téléporte le rigidbody là où il doit être par rapport au sol
            _rigidbody.MovePosition(_rigidbodyOnFloorPosition);

            // Annule la velocity verticale
            _verticalVelocity = Vector3.zero;
        }
    }

    private IEnumerator ChangeSpeedCoroutine(float newSpeed)
    {
        float startSpeed = _currentSpeed;
        float timer = 0;

        while (timer < _changeSpeedDuration)
        {
            timer += Time.deltaTime;
            _currentSpeed = Mathf.Lerp(startSpeed, newSpeed, timer / _changeSpeedDuration);
            yield return null;
        }

        _currentSpeed = newSpeed;
    }

    public void DoJog()
    {
        if (_changeSpeedCoroutine != null)
        {
            StopCoroutine(_changeSpeedCoroutine);
        }
        _changeSpeedCoroutine = StartCoroutine(ChangeSpeedCoroutine(_jogSpeed));
    }

    public void DoRun()
    {
        if (_changeSpeedCoroutine != null)
        {
            StopCoroutine(_changeSpeedCoroutine);
        }
        _changeSpeedCoroutine = StartCoroutine(ChangeSpeedCoroutine(_runSpeed));
    }

    public void DoSneak()
    {
        if (_changeSpeedCoroutine != null)
        {
            StopCoroutine(_changeSpeedCoroutine);
        }
        _changeSpeedCoroutine = StartCoroutine(ChangeSpeedCoroutine(_sneakSpeed));
    }

    public void DoIdle()
    {
        if (_changeSpeedCoroutine != null)
        {
            StopCoroutine(_changeSpeedCoroutine);
        }
        _changeSpeedCoroutine = StartCoroutine(ChangeSpeedCoroutine(0));
    }

    public void DoJump()
    {
        _doJump = true;
    }

    public void AddForce(Vector3 valueForce)
    {
        _rigidbody.velocity = new Vector3(valueForce.x, valueForce.y, valueForce.z);
    }
    public void AddForce(float valueXZ, float valueY)
    {
        Vector3 forward = _transformPlayer.forward * valueXZ;
        _rigidbody.velocity = new Vector3(forward.x, valueY, forward.z);
    }

    public Vector3 VelocityRb
    {
        get => _rigidbody.velocity;
    }

    public float MovementSpeed
    {
        get => _currentSpeed;
    }
    public bool ApplyForceAnimation { get => _applyForceAnimation; set => _applyForceAnimation = value; }
    public bool CanApplyForceAnimation { get => _canApplyForceAnimation; set => _canApplyForceAnimation = value; }

    private Vector3 _rigidbodyOnFloorPosition;
    private Coroutine _changeSpeedCoroutine;
}
