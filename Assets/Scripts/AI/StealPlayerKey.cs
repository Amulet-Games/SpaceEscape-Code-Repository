using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealPlayerKey : MonoBehaviour
{
    [Header("Trigger Collider.")]
    public Collider _triggerCollider;

    Enemy _enemy;
    public StateManager _states;

    public void Init(StateManager _states)
    {
        this._states = _states;
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;

        if (other.gameObject.layer == 9)
        {
            if (_states._hasKey)
            {
                _states._hasKey = false;
                KeySpawner.singleton.RespawnKey();
            }
        }
    }
}
