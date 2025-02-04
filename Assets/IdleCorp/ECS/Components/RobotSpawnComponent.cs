using Unity.Entities;
using Unity.Mathematics;

namespace IdleCorp.ECS.Components
{
    public struct RobotSpawnComponent : IComponentData
    {
        public Entity RobotPrefab;
        public float3 SpawnPosition;
        public int AmountToSpawn;
    }
}