using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGZones : MonoBehaviour
{
    [Header("Config.")]
    public LayerMask _affectedMask;
    private bool _hasPlayerEnterZone;

    public StateManager _states;
    public Collider[] _cols = new Collider[1];

    public void Init()
    {
        _affectedMask = 1 << 9;
        _states = SessionManager.singleton._states;
    }

    private void Update()
    {
        int i = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale / 2, _cols, transform.rotation, _affectedMask);
        if (i > 0)
        {
            _hasPlayerEnterZone = true;
            _states._curOxygenDepleteSpeed = _states._zeroOxygenDepleteSpeed;
        }
        else
        {
            SetPlayerLeaveZone(false);
        }
    }
    
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 9)
    //    {
    //        Debug.Log("OnTriggerExit");
    //        _states._curOxygenDepleteSpeed = _states._initOxygenDepleteSpeed;
    //    }
    //}

    void SetPlayerLeaveZone(bool _hasPlayerEnterZone)
    {
        if (!_hasPlayerEnterZone)
        {
            if (this._hasPlayerEnterZone)
            {
                this._hasPlayerEnterZone = false;
                _states._curOxygenDepleteSpeed = _states._initOxygenDepleteSpeed;
            }
        }
    }
}
