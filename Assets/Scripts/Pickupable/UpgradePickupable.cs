using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickupable : Pickupable
{
    [Header("Config.")]
    public float oxygenExtendedAmount;

    public override void Init()
    {
    }

    public override void OnInteract(Collider other)
    {
        StateManager _states = other.GetComponent<StateManager>();
        _states._totalOxygenAmonut += oxygenExtendedAmount;
        _states._curOxygenAmount = _states._totalOxygenAmonut;
        MainHudManager.singleton.RefreshSliderWidthByType();

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
