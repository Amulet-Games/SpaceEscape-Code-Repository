using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupable : Pickupable
{
    public override void Init()
    {
    }

    public override void OnInteract(Collider other)
    {
        StateManager _states = other.GetComponent<StateManager>();
        _states._hasKey = true;

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
