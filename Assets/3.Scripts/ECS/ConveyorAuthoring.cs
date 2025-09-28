using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConveyorAuthoring : MonoBehaviour
{
    public Transform start;
    public Transform control;
    public Transform end;
    public float speed = 1f;
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