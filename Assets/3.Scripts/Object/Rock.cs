using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Rock : MonoBehaviour, IMinable
{
    [SerializeField] private int durability = 5;
    public bool IsDepleted => durability <= 0;
    public ItemData itemData;
    public ItemData Data => itemData;
    public Guid resourceId;
    public GameObject minedItem;

    public void Mine(int power)
    {
        if (IsDepleted)
            return;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            Managers.UI.inven.AddItem(itemData);
            Destroy(gameObject);
        }
    }

    public void MineDigger(int power, IMinable minable)
    {
        if (IsDepleted)
            return;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            Managers.UI.digger.Digging(minable.Data);
            durability = 5;
        }
    }

    public bool AutoDigger(int power)
    {
        if (IsDepleted)
            return true;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            durability = 5;
            return true;
        }

        return false;
    }

    class Baker : Baker<Rock>
    {
        public override void Bake(Rock authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            authoring.resourceId = Guid.NewGuid();
            AddComponent(entity, new SpawnMinedItemConfig { MinedItemEntity = GetEntity(authoring.minedItem, TransformUsageFlags.None), Pos = authoring.transform.position + new Vector3(0, 1f, 0.9f), ID = authoring.resourceId });
        }
    }

    public void MakeMinedItem()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(typeof(SpawnMinedItemConfig));
        var ids = query.ToComponentDataArray<SpawnMinedItemConfig>(Unity.Collections.Allocator.Temp);
        var entities = query.ToEntityArray(Unity.Collections.Allocator.Temp);

        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i].ID == resourceId)
            {
                var foundEntity = entities[i];
                // 컴포넌트 추가로 신호 전달
                entityManager.AddComponentData(foundEntity, new SpawnTrigger { Trigger = true });
                ids.Dispose();
                entities.Dispose();
            }
        }
    }
}

public struct SpawnMinedItemConfig : IComponentData
{
    public Entity MinedItemEntity;
    public float3 Pos;
    public Guid ID;
}

public struct SpawnTrigger : IComponentData
{
    public bool Trigger;
}