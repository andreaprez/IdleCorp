using IdleCorp.ECS.Components;
using Unity.Entities;
using Unity.Transforms;

namespace IdleCorp.ECS.Systems
{
    public partial struct SimpleMovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, movement) 
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SimpleMovementComponent>>())
            {
                transform.ValueRW.Position += transform.ValueRW.Forward() * movement.ValueRO.Speed * deltaTime;
            }
        }

        public void OnDestroy(ref SystemState state)
        {
        }
    }
}