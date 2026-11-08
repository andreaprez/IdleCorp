using Unity.Entities;
using Unity.Mathematics;

namespace IdleCorp.ECS.Components
{
    public struct TargetedMovementComponent : IComponentData
    {
        public float3 TargetPosition;
        public float TargetReachedThreshold;
    }
}
