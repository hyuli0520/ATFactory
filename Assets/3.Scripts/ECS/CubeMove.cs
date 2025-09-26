using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
}

public struct MoveSpeed : IComponentData
{
    public float Speed;
}

public struct MoveDir : IComponentData
{
    public float3 Direction;
}