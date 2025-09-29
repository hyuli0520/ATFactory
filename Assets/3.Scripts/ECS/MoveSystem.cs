using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// Implement Movement Script
/// </summary>
[BurstCompile]
public partial struct MoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;


        foreach (var (translation, speed, dir) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeed>, RefRO<MoveDir>>())
        {
            float3 newPosition = translation.ValueRO.Position + dir.ValueRO.Direction * speed.ValueRO.Speed * deltaTime;
            translation.ValueRW.Position = newPosition;
        }
    }
}
