using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineController : MonoBehaviour
{
    LineRenderer lineRenderer;
    KeyPickupable keypickable;
    StateManager states;
    NavMeshAgent _statesAgent;

    [SerializeField]
    Texture[] textures;

    int animationStep;

    float fps = 30f;
    float fpsCounter;

    Transform target;

    public void Init(StateManager _states)
    {
        states = _states;
        _statesAgent = states._agent;
        keypickable = states._desiredKey;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    
    private void Update()
    {
        UpdateSetLineRendererPositions();
        AnimLineRenderer();
    }

    void UpdateSetLineRendererPositions()
    {
        if (_statesAgent.hasPath)
        {
            lineRenderer.positionCount = _statesAgent.path.corners.Length;
            lineRenderer.SetPositions(_statesAgent.path.corners);
        }
    }

    void AnimLineRenderer()
    {
        fpsCounter += states.delta;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }

    public void OnRenderer()
    {
        enabled = true;
        lineRenderer.enabled = true;
        animationStep = 0;
    }

    public void OffRenderer()
    {
        enabled = false;
        lineRenderer.enabled = false;
    }
}
