using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Clamp X Rotation.")]
    public bool isClampX = true;
    public float maxXValue = 35;
    public float minXValue = -25;

    [Header("Freemode Rotate Speed")]
    public float yRotateSpeed = 3;
    public float xRotateSpeed = 3;

    [Header("Status.")]
    public float mouseX;
    public float mouseY;
    public float _currentXAngle;
    public float _currentYAngle;
    public float _fixedDelta;
    public float _delta;

    [Header("Refs.")]
    public StateManager _states = null;
    public InputManager _inp = null;
    public Camera _mainCamera = null;
    public Transform _mTransform;

    public void Init(InputManager _inp)
    {
        this._inp = _inp;

        InitRefs();
    }

    public void Setup()
    {
        SetupLockScreenCursor();
    }

    public void Tick()
    {
        UpdateInputs_Main();

        RotateCameraInFreemode();
    }

    #region Tick.
    public void UpdateInputs_Main()
    {
        mouseX = Input.GetAxis("mouseX");
        mouseY = Input.GetAxis("mouseY");
    }

    void RotateCameraInFreemode()
    {
        _currentYAngle += mouseX * yRotateSpeed;
        _currentXAngle -= mouseY * xRotateSpeed;

        if (isClampX)
        {
            _currentXAngle = Mathf.Clamp(_currentXAngle, minXValue, maxXValue);
        }

        _mTransform.localRotation = Quaternion.Euler(_currentXAngle, _currentYAngle, 0);
    }
    #endregion

    #region Setup.
    void SetupLockScreenCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Init.
    void InitRefs()
    {
        _states = GetComponentInParent<StateManager>();
        _states._camHandler = this;

        _mainCamera = Camera.main;
        _mTransform = transform;
    }
    #endregion

}
