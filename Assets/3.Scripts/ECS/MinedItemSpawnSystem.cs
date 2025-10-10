using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public partial struct MinedItemSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnMinedItemConfig>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);


        foreach (var (trigger, entity) in SystemAPI.Query<SpawnTrigger>().WithEntityAccess())
        {
            if (trigger.Trigger)
            {
                var spawn = SystemAPI.GetComponentRO<SpawnMinedItemConfig>(entity);
                Entity mined = entityManager.Instantiate(spawn.ValueRO.MinedItemEntity);

                var transition = SystemAPI.GetComponentRW<LocalTransform>(mined);
                transition.ValueRW.Position = spawn.ValueRO.Pos;

                ecb.RemoveComponent<SpawnTrigger>(entity);
            }
        }

        ecb.Playback(entityManager);
        ecb.Dispose();
    }
}