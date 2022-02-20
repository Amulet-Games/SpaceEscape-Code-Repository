using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroZoneActivator : MonoBehaviour
{
    public ZeroGZones[] _zeroZones;

    private void Update()
    {
        if (SessionManager.singleton.isSceneFullyLoaded)
        {
            for (int i = 0; i < _zeroZones.Length; i++)
            {
                _zeroZones[i].Init();
                _zeroZones[i].gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
