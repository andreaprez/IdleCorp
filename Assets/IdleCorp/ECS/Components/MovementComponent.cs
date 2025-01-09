using Unity.Entities;

namespace IdleCorp.ECS.Components
{
    public struct MovementComponent : IComponentData
    {
        public float Speed;
        public float TargetReachThreshold;
    }
}
