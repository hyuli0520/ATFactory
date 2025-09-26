using Unity.Entities;
using UnityEngine;

public class MoveSpeedBaker : Baker<CubeMove>
{
    public override void Bake(CubeMove authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new MoveSpeed { Speed = authoring.speed });
        AddComponent(entity, new MoveDir { Direction = authoring.direction });
    }
}
