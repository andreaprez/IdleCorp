using Unity.Entities;
using Unity.Mathematics;

namespace IdleCorp.ECS.Components
{
    public struct VehicleSpawnComponent : IComponentData
    {
        public Entity VehiclePrefab;
        public float3 SpawnPosition;
    }
}