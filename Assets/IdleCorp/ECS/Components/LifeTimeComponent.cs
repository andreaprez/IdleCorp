using Unity.Entities;

namespace IdleCorp.ECS.Components
{
    public struct LifeTimeComponent : IComponentData
    {
        public float RemainingSeconds;
    }
}