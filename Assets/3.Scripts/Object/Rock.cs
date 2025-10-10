using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Rock : MonoBehaviour, IMinable
{
    [SerializeField] private int durability = 5;
    public bool IsDepleted => durability <= 0; // Indicates whether the rock is depleted
    public ItemData itemData;
    public ItemData Data => itemData; // Item data rewarded when the rock is mined
    public Guid resourceId; // Unique identifier for the resource, used for ECS linkage
    public GameObject minedItem; // Prefab of the mined item to be spawned after mining

    /// <summary>
    /// Handles mining interaction by a player or tool
    /// </summary>
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

    /// <summary>
    /// Handles mining a digger
    /// </summary>
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

    /// <summary>
    /// Handles automated mining for ECS-based diggers
    /// </summary>
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

    /// <summary>
    /// ECS baker class that converts this MonoBehaviour into ECS data components
    /// </summary>
    class Baker : Baker<Rock>
    {
        public override void Bake(Rock authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            authoring.resourceId = Guid.NewGuid();
            AddComponent(entity, new SpawnMinedItemConfig { MinedItemEntity = GetEntity(authoring.minedItem, TransformUsageFlags.None), Pos = authoring.transform.position + new Vector3(0, 1f, 0.9f), ID = authoring.resourceId });
        }
    }

    /// <summary>
    /// Triggers the ECS system to spawn a mined item based on its resource ID
    /// </summary>
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
                entityManager.AddComponentData(foundEntity, new SpawnTrigger { Trigger = true });
            }
        }
        ids.Dispose();
        entities.Dispose();
    }
}

/// <summary>
/// ECS component that holds data for spawning mined items
/// </summary>
public struct SpawnMinedItemConfig : IComponentData
{
    public Entity MinedItemEntity;
    public float3 Pos;
    public Guid ID;
}

/// <summary>
/// ECS component used as a trigger to initiate spawn behavior
/// </summary>
public struct SpawnTrigger : IComponentData
{
    public bool Trigger;
}