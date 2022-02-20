using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [Header("Key.")]
    public KeyPickupable[] _keyPickupables;
    
    public static KeySpawner singleton;
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        RespawnKey();
    }

    public void RespawnKey()
    {
        //_keyPickupable.transform.localPosition = _spawnTransform.localPosition;
        //_keyPickupable.transform.localEulerAngles = _spawnTransform.localEulerAngles;
        for (int i = 0; i < _keyPickupables.Length; i++)
        {
            _keyPickupables[i].gameObject.SetActive(true);
        }
    }
}
