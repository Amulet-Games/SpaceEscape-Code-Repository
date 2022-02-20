using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGZonesVisualizer : MonoBehaviour
{
    public bool _enable;
    public Color _zoneColor;

    private void OnDrawGizmos()
    {
        if (_enable)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _zoneColor;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}
