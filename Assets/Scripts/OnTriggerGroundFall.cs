using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerGroundFall : MonoBehaviour
{
    [Header("Config.")]
    public Collider _triggerCol;
    public Collider _colliderToDisable;
    public Rigidbody _triggeredRb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log("OnTriggerEnter");
            _triggeredRb.isKinematic = false;
            _colliderToDisable.enabled = false;
        }
    }
}
