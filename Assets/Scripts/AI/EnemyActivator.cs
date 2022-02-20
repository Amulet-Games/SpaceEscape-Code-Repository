using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public Enemy[] enemies;

    // Update is called once per frame
    void Update()
    {
        if (SessionManager.singleton.isSceneFullyLoaded)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Init();
                enemies[i].gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
