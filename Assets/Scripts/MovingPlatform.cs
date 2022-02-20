using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Config.")]
    public LeanTweenType _moveEaseType;
    public float moveSpeed;
    public float moveDistance;
    public float delayTime = 1;
    public bool _isXAxis;
    public bool _isYAxis;
    public StateManager _states;

    [Header("Status.")]
    public Vector3 _targetlocalPos;
    public Vector3 _initLocalPos;
    public bool _isGoingBack;

    public void Init()
    {
        _initLocalPos = transform.localPosition;

        if (_isXAxis)
        {
            _targetlocalPos = _initLocalPos + new Vector3(moveDistance, 0, 0);
        }
        else if (_isYAxis)
        {
            _targetlocalPos = _initLocalPos + new Vector3(0, moveDistance, 0);
        }
        else
        {
            _targetlocalPos = _initLocalPos + new Vector3(0, 0, moveDistance);
        }
            
        _isGoingBack = true;
        SetIsGoingBackStatus(false);

        _states = SessionManager.singleton._states;
    }
    
    private void SetIsGoingBackStatus(bool _isGoingBack)
    {
        if (_isGoingBack)
        {
            if (!this._isGoingBack)
            {
                this._isGoingBack = true;
                LeanTween.move(gameObject, _initLocalPos, moveSpeed).setEase(_moveEaseType).setDelay(delayTime).setOnComplete(OnCompleteSetIsGoingBackToFalse);
            }
        }
        else
        {
            if (this._isGoingBack)
            {
                this._isGoingBack = false;
                LeanTween.move(gameObject, _targetlocalPos, moveSpeed).setEase(_moveEaseType).setDelay(delayTime).setOnComplete(OnCompleteSetIsGoingBackToTrue);
            }
        }
    }

    void OnCompleteSetIsGoingBackToTrue()
    {
        SetIsGoingBackStatus(true);
    }

    void OnCompleteSetIsGoingBackToFalse()
    {
        SetIsGoingBackStatus(false);
    }
}
