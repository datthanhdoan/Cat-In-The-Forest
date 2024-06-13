using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle,
    Move
}

public class Player : GenericSingleton<Player>
{
    public static event Action<PlayerState> OnPlayerStateChanged;
    public PlayerState playerState { get; private set; } = PlayerState.Idle;
    private PlayerState previousState = PlayerState.Idle;
    [NonSerialized] public NavMeshAgent _agent;

    [SerializeField] float _speed = 5;

    private PlayerInputAction _inputActions;
    private Vector2 _moveInput;
    private Vector3 _clickPosition;
    private Vector3 _targetVector;
    private bool _isGamepadMove = false;
    private bool _isClickMove = false;

    // target object
    public IInteractive interactiveObject;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;
        OnPlayerStateChanged?.Invoke(playerState);

        _inputActions = new PlayerInputAction();
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;

        _inputActions.Player.Interactive.performed += OnInteractive;
        _inputActions.Player.ButtonInteractive.performed += OnButtonInteractive;
        // _inputActions.Player.Click.performed += OnClick;
        _inputActions.Enable();
    }

    private void OnButtonInteractive(InputAction.CallbackContext context)
    {
        Debug.Log("ButtonInteractive");
        if (interactiveObject != null)
        {
            interactiveObject.OnButtonInteractive();
        }
    }

    private void OnInteractive(InputAction.CallbackContext context)
    {
        Debug.Log("Interactive");
        if (interactiveObject != null)
        {
            interactiveObject.OnInteractive();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _isGamepadMove = _moveInput != Vector2.zero;
    }

    public void SetInteractiveObject(IInteractive interactive)
    {
        interactiveObject = interactive;
    }

    public IInteractive GetInteractiveObject()
    {
        return interactiveObject;
    }

    // private void OnClick(InputAction.CallbackContext context)
    // {
    //     if (Mouse.current != null)
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    //         if (Physics.Raycast(ray, out RaycastHit hit))
    //         {
    //             _clickPosition = hit.point;
    //             _isClickMove = true;
    //         }
    //     }
    // }


    private void OnClick(Vector3 clickPosition)
    {
        _clickPosition = clickPosition;
        _isClickMove = true;
    }

    private void FixedUpdate()
    {
        bool isMoving = false;

        if (_isClickMove)
        {
            // Di chuyển đến vị trí được click
            _agent.SetDestination(_clickPosition);
            _isClickMove = false;
            isMoving = true;
        }
        else if (_isGamepadMove)
        {
            // Di chuyển bằng gamepad
            Vector3 inputVector = new Vector3(_moveInput.x, _moveInput.y, 0);
            Vector3 targetPosition = transform.position + inputVector * _speed * 7 * Time.fixedDeltaTime;

            if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                isMoving = true;
            }
        }

        UpdatePlayerState(isMoving);
        DetectStateChange();
    }
    private void DetectStateChange()
    {
        if (playerState != previousState)
        {
            OnPlayerStateChanged?.Invoke(playerState);
            previousState = playerState;
        }
    }

    public void UpdatePlayerState(bool isMoving)
    {
        if (_agent.enabled)
        {
            if (_agent.remainingDistance < 0.1f)
            {
                playerState = PlayerState.Idle;
            }
            else
            {
                playerState = PlayerState.Move;
            }
        }
        else
        {
            playerState = isMoving ? PlayerState.Move : PlayerState.Idle;
        }
    }

    private void OnEnable()
    {
        if (_inputActions != null)
        {
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;
            // _inputActions.Player.Click.performed += OnClick;
            _inputActions.Enable();
        }

        InputManager.OnClick += OnClick;
    }

    private void OnDisable()
    {
        if (_inputActions != null)
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMove;
            // _inputActions.Player.Click.performed -= OnClick;
            _inputActions.Disable();
        }

        InputManager.OnClick -= OnClick;
    }

    public void PlayerSpeed(float newSpeed)
    {
        _speed = newSpeed;
        _agent.speed = _speed;
    }
}
