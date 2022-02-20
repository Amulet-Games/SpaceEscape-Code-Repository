using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    [Header("Pickupable Type.")]
    public PickupableTypeEnum _pickupType;

    public abstract void Init();

    public abstract void OnInteract(Collider other);

    public abstract void TurnOffInteraction();
}

public enum PickupableTypeEnum
{
    Oxygen,
    Upgradable,
    Key,
}