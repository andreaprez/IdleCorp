using Unity.Entities;
using Unity.Mathematics;

namespace IdleCorp.ECS.Components
{
    public struct RobotSpawnComponent : IComponentData
    {
        public Entity RobotPrefab;
        public int AmountToSpawn;
        public float3 SpawnPosition;
        public float3 TargetPosition;
        public float TargetReachedThreshold;
    }
}