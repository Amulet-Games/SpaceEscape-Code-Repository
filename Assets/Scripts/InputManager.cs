using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input Type")]
    public InputTypeEnum _currentInputType;

    [Header("Delta")]
    public float delta;
    public float fixedDelta;

    [Header("Player_1")]
    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float vertical;
    [SerializeField]
    private bool enter;

    [Header("References")]
    public StateManager _states;
    public CameraHandler _camHandler;
    public MainHudManager _mainHudManager;

    private void Awake()
    {
        InitInputType();
        InitStates();
        InitCamHandler();
        InitMainHudManager();
    }

    private void Start()
    {
        SetupStates();

        SetupCamHandler();
    }

    private void Update()
    {
        delta = Time.deltaTime;

        UpdateDeltas();

        UpdateInputsByType();

        States_Tick();
        
        MainHud_Tick();
    }

    private void FixedUpdate()
    {
        fixedDelta = Time.fixedDeltaTime;

        UpdateFixedDeltas();

        States_FixedTick();
    }

    private void LateUpdate()
    {
        CamHandler_Tick();
    }

    #region Tick.
    void UpdateInputsByType()
    {
        switch (_currentInputType)
        {
            case InputTypeEnum.Main:
                _states.UpdateStateInput_Main();
                _camHandler.UpdateInputs_Main();
                break;

            case InputTypeEnum.Menu:
                break;
        }
    }

    void UpdateDeltas()
    {
        _states.delta = delta;
        _camHandler._delta = delta;
    }

    void States_Tick()
    {
        _states.Tick();
    }

    void CamHandler_Tick()
    {
        _camHandler.Tick();
    }

    void MainHud_Tick()
    {
        _mainHudManager.Tick();
    }
    #endregion

    #region FixedTick.
    void UpdateFixedDeltas()
    {
        _states.fixedDelta = fixedDelta;
        _camHandler._fixedDelta = fixedDelta;
    }

    void States_FixedTick()
    {
        _states.FixedTick();
    }

    void CamHandler_FixedTick()
    {

    }
    #endregion

    #region Setup.
    void SetupStates()
    {
        _states.Setup();
    }

    void SetupCamHandler()
    {
        _camHandler.Setup();
    }
    #endregion

    #region Init.
    void InitInputType()
    {
        _currentInputType = InputTypeEnum.Main;
    }

    void InitStates()
    {
        _states = GetComponent<StateManager>();
        _states.Init();
    }

    void InitCamHandler()
    {
        _camHandler = GetComponentInChildren<CameraHandler>();
        _camHandler.Init(this);
    }

    void InitMainHudManager()
    {
        _mainHudManager = MainHudManager.singleton;
        _mainHudManager.Init(this);
    }
    #endregion
}

public enum InputTypeEnum
{
    Main,
    Menu
}
