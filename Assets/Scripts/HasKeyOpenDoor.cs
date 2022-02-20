using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasKeyOpenDoor : MonoBehaviour
{
    [Header("Config.")]
    public Collider _openDoorCollider;
    public float _moveDis = 8;
    public int _representKeyNumber;

    private StateManager _states;
    private Vector3 _targetPos;

    public void Init()
    {
        _states = SessionManager.singleton._states;
        _targetPos = transform.localPosition + new Vector3(0, _moveDis, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (_states._hasKey)
            {
                _states._hasKey = false;
                _states._currentAreaNum++;
                _states._currentDesireKeyNumber++;
                _states.RefreshDesireKey();
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        LeanTween.move(gameObject, _targetPos, 2).setEase(LeanTweenType.easeOutSine);
    }
}
