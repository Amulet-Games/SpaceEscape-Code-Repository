using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformActivator : MonoBehaviour
{
    public MovingPlatform[] _plateforms;
    
    // Update is called once per frame
    void Update()
    {
        if (SessionManager.singleton.isSceneFullyLoaded)
        {
            for (int i = 0; i < _plateforms.Length; i++)
            {
                _plateforms[i].Init();
                _plateforms[i].gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
