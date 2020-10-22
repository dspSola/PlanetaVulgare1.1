using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMoverController : MonoBehaviour
{
    [SerializeField] private NewPlayerEntity _newPlayerEntity;
    [SerializeField] private GetInputBrute _getInputBrute;
    [SerializeField] private NewStateVertical _newStateVertical;
    [SerializeField] private NewStateHorizontal _newStateHorizontal;
    [SerializeField] private NewStateAttack _newStateAttack;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Vector3 _transformedInput;
    [SerializeField] private float _movementQty, _currentSpeed;
    [SerializeField] [Range(0, 5)] private float _gravityFallMultiplier;

    [Header("Ground testers")]
    [SerializeField] private CollisionRaycastTester _floorFinder;
    [SerializeField] private float _floorOffsetY;

    [SerializeField] private Vector3 _verticalVelocity, _horizontalVelocity, _velocity;

    [SerializeField] private bool _isGrounded;

    private void Start()
    {
        _currentSpeed = _newPlayerEntity.SpeedWalk;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Horizontal velocity
        if (_newStateHorizontal.CurrentState != NewPlayerHorizontalState.IDLE)
        {
            // On change le référentiel de _movementInput pour le passer dans le référentiel local au référentiel global
            // Attention, _movementInput n'est pas une direction car le vecteur n'est pas encore normalisé.
            _transformedInput = _newPlayerEntity.PlayerTransform.TransformVector(_getInputBrute.Movement);
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
        //if (_doJump)
        //{
        //    _verticalVelocity = Vector3.up * _jumpPower;
        //    _doJump = false;
        //}

        if (_newStateVertical.CurrentState == NewPlayerVerticalState.GROUNDED)
        {
            _verticalVelocity = Vector3.zero;
        }
        else
        {
            // Si on n'est pas au sol, applique la gravité
            _verticalVelocity += Physics.gravity * _gravityFallMultiplier * Time.fixedDeltaTime;
        }
        _velocity = _horizontalVelocity + _verticalVelocity;
        _rigidbody.velocity = _velocity;
    }

    private void FixedUpdate()
    {
        RotateTowardsCameraForward();

        // Si on est au sol, on colle au sol
        if (_newStateVertical.CurrentState == NewPlayerVerticalState.GROUNDED)
        {
            StickToGround();
        }
    }

    private void RotateTowardsCameraForward()
    {
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.y = 0;
        Quaternion desiredRotation = Quaternion.LookRotation(cameraForward);
        Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, desiredRotation, _newPlayerEntity.SpeedTurn * Time.fixedDeltaTime);
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

    private Vector3 _rigidbodyOnFloorPosition;
}