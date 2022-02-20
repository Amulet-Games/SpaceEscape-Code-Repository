using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    [Header("Loco Config.")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight = 9;
    public float gravity = -13f;
    private float velocityY;

    [Header("Other Config.")]
    public float _initTotalOxygenAmount = 120;
    public float _totalOxygenAmonut = 120;
    public float _initOxygenDepleteSpeed = 2;
    public float _zeroOxygenDepleteSpeed = 6;
    public float _useHintDepleteAddon = 1.25f;
    public float _curOxygenDepleteSpeed;
    public float _lowOxygenThershold = 40;

    [Header("Decision Variables.")]
    public float delta;
    public float fixedDelta;
    public float moveAmount;
    public Vector3 moveDir;

    [Header("Inputs")]
    public float horizontal;
    public float vertical;
    public bool _l_shift;
    public bool _space;
    public bool _r_mouse;
    public bool enter;

    [Header("Status.")]
    public float _curOxygenAmount;
    public bool _isLowOnOxygen;
    public bool _hasKey;
    public bool _useHint;
    public int _currentAreaNum;
    public int _currentDesireKeyNumber;

    [Header("Refs")]
    [HideInInspector] public InputManager inp;
    [HideInInspector] public CameraHandler _camHandler;
    [HideInInspector] public Transform mTransform;
    [HideInInspector] public Collider col;
    [HideInInspector] public CharacterController controllerComponent;
    [HideInInspector] public LineController _lineController;
    [HideInInspector] public NavMeshAgent _agent;
    [HideInInspector] public KeyPickupable _desiredKey;

    [HideInInspector] public readonly Vector3 vector3Zero = new Vector3(0, 0, 0);
    [HideInInspector] public readonly Vector3 vector3Up = new Vector3(0, 1, 0);
    [HideInInspector] public Vector2 vector2Zero = new Vector2(0, 0);
    [HideInInspector] public Vector2 vector2Up = new Vector2(0, 1);

    public void Init()
    {
        InitGameObject();
        InitCharacterController();
        InitCollider();

        _currentDesireKeyNumber = 0;
        RefreshDesireKey();
        InitAgent();
        InitLineController();
    }

    public void Setup()
    {
        SetupOxygenAmount();
    }

    public void Tick()
    {
        UpdateStateInput_Main();

        UpdateDecisionVariables();

        UpdatePlayerMovement();

        UpdatePlayerUseHint();

        UpdateOxygenAmount();
    }

    public void FixedTick()
    {

    }

    #region Tick.
    public void UpdateStateInput_Main()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        _space = Input.GetButtonDown("Space");
        _l_shift = Input.GetButton("L_Shift");
        _r_mouse = Input.GetButton("R_Mouse");
        enter = Input.GetButtonDown("Enter");
    }

    void UpdateDecisionVariables()
    {
        moveDir = _camHandler._mTransform.forward * vertical;
        moveDir += _camHandler._mTransform.right * horizontal;
        moveDir.y = 0;

        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
    }

    void UpdatePlayerMovement()
    {
        if (controllerComponent.isGrounded)
        {
            velocityY = 0f;
        }
        else
        {
            velocityY += gravity * delta;
        }

        Vector3 velocity = vector3Zero;

        if (_space)
        {
            velocityY += jumpHeight;
        }

        if (!_l_shift)
        {
            velocity = (moveDir * moveAmount * walkSpeed) + (vector3Up * velocityY);
        }
        else
        {
            velocity = (moveDir * moveAmount * runSpeed) + (vector3Up * velocityY);
        }
        

        controllerComponent.Move(velocity * delta);
    }

    void UpdatePlayerUseHint()
    {
        if (!_hasKey)
            SetUseHintStatus(_r_mouse);
    }

    void UpdateOxygenAmount()
    {
        if (_curOxygenAmount != 0)
        {
            _curOxygenAmount -= _curOxygenDepleteSpeed * delta;
            if (_curOxygenAmount < _lowOxygenThershold)
            {
                SetIsLowOnOxygenStatus(true);

                if (_curOxygenAmount <= 0)
                {
                    MainHudManager.singleton.ShowLoseMessage();
                }
            }

            if (_curOxygenAmount < 0)
            {
                _curOxygenAmount = 0;
            }
        }
    }

    void UpdateAgentPath()
    {
        _agent.SetDestination(_desiredKey.transform.position);
    }

    void ClearAgentPath()
    {
        _agent.path.ClearCorners();
        _agent.ResetPath();
    }
    #endregion
    
    #region Set Status.
    void SetIsLowOnOxygenStatus(bool _isLowOnOxygen)
    {
        if (_isLowOnOxygen)
        {
            if (!this._isLowOnOxygen)
            {
                this._isLowOnOxygen = true;
            }
        }
        else
        {
            if (this._isLowOnOxygen)
            {
                this._isLowOnOxygen = false;
            }
        }
    }

    void SetUseHintStatus(bool _useHint)
    {
        /// If Player wants to use hint.
        if (_useHint)
        {
            /// If Player isn't using any hint right now.
            if (!this._useHint)
            {
                this._useHint = true;

                UpdateAgentPath();
                _lineController.OnRenderer();
                _curOxygenDepleteSpeed += _useHintDepleteAddon;
            }
            else
            {
                UpdateAgentPath();
            }
        }
        else
        {
            if (this._useHint)
            {
                this._useHint = false;

                ClearAgentPath();
                _lineController.OffRenderer();
                _curOxygenDepleteSpeed -= _useHintDepleteAddon;
            }
        }
    }

    #endregion

    #region Setup.
    void SetupOxygenAmount()
    {
        _totalOxygenAmonut = _initTotalOxygenAmount;
        _curOxygenAmount = _totalOxygenAmonut;
        _curOxygenDepleteSpeed = _initOxygenDepleteSpeed;
    }
    #endregion

    #region Init.
    void InitGameObject()
    {
        mTransform = gameObject.transform;
        gameObject.layer = 9;
    }

    void InitCharacterController()
    {
        controllerComponent = GetComponent<CharacterController>();
    }

    void InitCollider()
    {
        col = GetComponent<Collider>();
    }

    public void RefreshDesireKey()
    {
        _desiredKey = KeySpawner.singleton._keyPickupables[_currentDesireKeyNumber];
    }

    void InitAgent()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = true;
        _agent.updateRotation = false;
    }

    void InitLineController()
    {
        _lineController = GetComponent<LineController>();
        _lineController.Init(this);
    }
    #endregion
}
