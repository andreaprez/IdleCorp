using IdleCorp.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class RobotAuthoring : MonoBehaviour
    {
    }

    public class RobotBaker : Baker<RobotAuthoring>
    {
        public override void Bake(RobotAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new MovementComponent());
        }
    }
}