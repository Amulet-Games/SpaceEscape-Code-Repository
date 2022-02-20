using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Config.")]
    public float _lookAtPlayerDis;
    public float _rotateSlerpSpeed;
    public int _protectingArea;

    [Header("Status.")]
    public float _delta;
    public float _fixedDelta;
    public Vector3 _dirToPlayer;
    public float _disToPlayer;
    public float _angleToPlayer;

    [Header("Spawn Status.")]
    public Vector3 _spawnPoint;
    public float _disToSpawnPoint;

    [Header("General Status.")]
    public bool _isChasingPlayer;
    public bool _isStartStealing;

    [Header("Refs.")]
    public StateManager _states;
    public NavMeshAgent _agent;
    public StealPlayerKey _stealPlayerKey;

    public void Init()
    {
        _states = SessionManager.singleton._states;

        _agent = transform.GetChild(0).GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;

        _spawnPoint = transform.position;

        _stealPlayerKey = GetComponent<StealPlayerKey>();
        _stealPlayerKey.Init(_states);
    }

    private void Update()
    {
        UpdateDeltas();
        GetDisDirAngle();
    }

    private void FixedUpdate()
    {
        UpdateFixedDeltas();
        MoveWithAgent();
        RotateWithAgent();
    }

    void UpdateDeltas()
    {
        _delta = Time.deltaTime;
    }

    void UpdateFixedDeltas()
    {
        _fixedDelta = Time.fixedDeltaTime;
    }

    void GetDisDirAngle()
    {
        _dirToPlayer = _states.mTransform.position - transform.position;
        _dirToPlayer.y = 0;

        _disToPlayer = Vector3.Magnitude(_dirToPlayer);
        _angleToPlayer = Vector3.Angle(transform.forward , _dirToPlayer);

        if (!_isChasingPlayer)
        {
            _disToSpawnPoint = Vector3.Distance(transform.position, _spawnPoint);
        }
    }
    
    void MoveWithAgent()
    {
        if (_states._hasKey)
        {
            if (_states._currentAreaNum == _protectingArea)
            {
                SetIsStartStealingStatus(true);
                ChasePlayer();
            }
        }
        else
        {
            if (_disToSpawnPoint > 1)
            {
                SetIsStartStealingStatus(false);
                GoBackToSpawnPoint();
            }
        }
    }

    void ChasePlayer()
    {
        _agent.SetDestination(_states.mTransform.position);
        _agent.stoppingDistance = 1.5f;

        Vector3 _targetPos = _agent.nextPosition;
        _targetPos.y = transform.position.y;
        transform.position = _targetPos;
    }

    void GoBackToSpawnPoint()
    {
        _agent.SetDestination(_spawnPoint);
        _agent.stoppingDistance = 0;

        Vector3 _targetPos = _agent.nextPosition;
        _targetPos.y = transform.position.y;
        transform.position = _targetPos;
    }

    void RotateWithAgent()
    {
        if (_disToPlayer < _lookAtPlayerDis)
        {
            LookAtPlayer();
        }
        else
        {
            LookAtRoute();
        }
    }
    
    void LookAtPlayer()
    {
        float angle = Vector3.SignedAngle(transform.forward, _dirToPlayer, Vector3.up);
        float rot = _rotateSlerpSpeed * _fixedDelta;
        rot = Mathf.Min(Mathf.Abs(angle), rot);
        transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
    }

    void LookAtRoute()
    {
        float angle = Vector3.SignedAngle(transform.forward, _agent.velocity, Vector3.up);
        float rot = _rotateSlerpSpeed * _fixedDelta;
        rot = Mathf.Min(Mathf.Abs(angle), rot);
        transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
    }

    void SetIsStartStealingStatus(bool _isStartStealing)
    {
        if (_isStartStealing)
        {
            if (!this._isStartStealing)
            {
                this._isStartStealing = true;
                _stealPlayerKey.enabled = true;
            }
        }
        else
        {
            if (this._isStartStealing)
            {
                this._isStartStealing = false;
                _stealPlayerKey.enabled = false;
            }
        }
    }
}
