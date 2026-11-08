using IdleCorp.ECS.Components;
using Unity.Entities;

namespace IdleCorp.ECS.Systems
{
    public partial struct LifeTimeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (lifetime, entity) 
                     in SystemAPI.Query<RefRW<LifeTimeComponent>>().WithEntityAccess())
            {
                lifetime.ValueRW.RemainingSeconds -= deltaTime;
                if (lifetime.ValueRO.RemainingSeconds <= 0f)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }

        public void OnDestroy(ref SystemState state)
        {
        }
    }
}