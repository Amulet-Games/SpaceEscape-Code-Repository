using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    public HasKeyOpenDoor[] _doors;
    
    // Update is called once per frame
    void Update()
    {
        if (SessionManager.singleton.isSceneFullyLoaded)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].Init();
                _doors[i].gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
