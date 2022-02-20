using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillPickupable : Pickupable
{
    [Header("Oxygen Tank Config.")]
    public float _oxygenAmount;
    
    public override void Init()
    {
    }
    
    public override void OnInteract(Collider other)
    {
        StateManager _states = other.GetComponent<StateManager>();
        _states._curOxygenAmount += _oxygenAmount;
        if (_states._curOxygenAmount >= _states._totalOxygenAmonut)
        {
            _states._curOxygenAmount = _states._totalOxygenAmonut;
        }

        TurnOffInteraction();
    }

    public override void TurnOffInteraction()
    {
        gameObject.SetActive(false);
    }

    #region Callback.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9)
            return;

        OnInteract(other);
    }
    #endregion
}
