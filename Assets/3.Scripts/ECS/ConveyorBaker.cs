using Unity.Entities;
using UnityEngine;

public class ConveyorBaker : Baker<ConveyorAuthoring>
{
    public override void Bake(ConveyorAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new ConveyorPath
        {
            Start = authoring.start.position,
            Control = authoring.control.position,
            End = authoring.end.position,
            Speed = authoring.speed,
        });
    }
}
