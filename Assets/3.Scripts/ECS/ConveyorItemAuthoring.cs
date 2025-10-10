using Unity.Entities;
using UnityEngine;

public class ConveyorItemAuthoring : MonoBehaviour { }

public class ConveyorItemBaker : Baker<ConveyorItemAuthoring>
{
    public override void Bake(ConveyorItemAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new ConveyorItem { T = 0f });
    }
}