using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ConveyorSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        // 씬에 존재하는 모든 ConveyorPath 조회
        foreach (var path in SystemAPI.Query<RefRO<ConveyorPath>>())
        {
            // 모든 ConveyorItem 조회
            foreach (var (transform, item) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<ConveyorItem>>())
            {
                // 이동 비율 (0 → 1)
                item.ValueRW.T += path.ValueRO.Speed * deltaTime * 0.1f;
                if (item.ValueRW.T > 1f) item.ValueRW.T = 1f;

                // Bezier 곡선을 따라 좌표 계산
                float3 pos = Util.Bezier(
                    path.ValueRO.Start,
                    path.ValueRO.Control,
                    path.ValueRO.End,
                    item.ValueRO.T
                );

                // Transform에 적용
                transform.ValueRW.Position = pos;
            }
        }
    }
}