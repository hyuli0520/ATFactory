using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConveyorAuthoring : MonoBehaviour
{
    public Transform start;
    public Transform control;
    public Transform end;
    public float speed = 1f;

    [Range(2, 50)]
    public int segments = 20;

    private void OnDrawGizmos()
    {
        if (start == null || control == null || end == null)
            return;

        Gizmos.color = Color.yellow;

        float3 prevPos = start.position;
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            float3 nextPos = Util.Bezier((float3)start.position,
                                         (float3)control.position,
                                         (float3)end.position,
                                         t);
            Gizmos.DrawLine(prevPos, nextPos);
            prevPos = nextPos;
        }

        // Optionally, control lines
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start.position, control.position);
        Gizmos.DrawLine(control.position, end.position);
    }
}

public struct ConveyorPath : IComponentData
{
    public float3 Start;
    public float3 Control;
    public float3 End;
    public float Speed;
}

public struct ConveyorItem : IComponentData
{
    public float T;
}