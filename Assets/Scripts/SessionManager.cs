using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    [Header("Status.")]
    public bool isPlayerCreated;
    public bool isSceneFullyLoaded;

    [Header("Public Refs.")]
    public StateManager _states;

    [Header("Private Refs.")]
    public AsyncOperation _currentAsyncOperation;

    public static SessionManager singleton;
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadFirstLevel()
    {
        isSceneFullyLoaded = false;
        StartCoroutine(LoadLevel());
    }

    #region Create Player.
    void LoadPlayer()
    {
        StateManager _states = Instantiate(Resources.Load<StateManager>("Player"));
        this._states = _states;
    }
    #endregion

    #region On Scene.
    void OnSceneLoadReady()
    {

    }

    void OnSceneFullyLoaded()
    {
        LoadPlayer();
        _currentAsyncOperation = null;
        isSceneFullyLoaded = true;
    }
    #endregion

    #region Load Target Level
    IEnumerator LoadLevel()
    {
        _currentAsyncOperation = SceneManager.LoadSceneAsync("Level_1");
        //_currentAsyncOperation.allowSceneActivation = false;

        while (!_currentAsyncOperation.isDone)
        {
            if (_currentAsyncOperation.progress >= 0.9f)
            {
                OnSceneLoadReady();
            }

            yield return null;
        }

        if (_currentAsyncOperation.isDone)
        {
            OnSceneFullyLoaded();
        }
    }
    #endregion
}
