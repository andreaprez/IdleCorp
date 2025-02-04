using IdleCorp.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class RobotAuthoring : MonoBehaviour
    {
        public float TargetReachedThreshold;
    }

    public class RobotBaker : Baker<RobotAuthoring>
    {
        public override void Bake(RobotAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new MovementComponent
            {
                TargetPosition = new float3(2.35f, 0, 6f), //TODO: Get from service
                TargetReachedThreshold = authoring.TargetReachedThreshold
            });
        }
    }
}