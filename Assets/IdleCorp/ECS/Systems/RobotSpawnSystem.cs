using IdleCorp.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

namespace IdleCorp.ECS.Systems
{
    public partial struct RobotSpawnSystem : ISystem
    {
        private float PositionRandomizer => Random.Range(-0.2f,0.2f);

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var spawnComp in SystemAPI.Query<RefRW<RobotSpawnComponent>>())
            {
                for (var i = 0; i < spawnComp.ValueRO.AmountToSpawn; i++)
                {
                    var robotEntity = ecb.Instantiate(spawnComp.ValueRO.RobotPrefab);
                    ecb.SetComponent(robotEntity, new LocalTransform
                    {
                        Position = GetSpawnPosition(spawnComp),
                        Scale = 1
                    });
                }
                spawnComp.ValueRW.AmountToSpawn = 0;
            }
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        private float3 GetSpawnPosition(RefRW<RobotSpawnComponent> spawnComp)
        {
            var forward = new float3(0, 0, 1);
            var right = new float3(1, 0, 0);
            return spawnComp.ValueRW.SpawnPosition
                   + forward * PositionRandomizer 
                   + right * PositionRandomizer;
        }
    }
}