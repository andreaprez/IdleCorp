using IdleCorp.ECS.Components;
using ProjectDawn.Navigation;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace IdleCorp.ECS.Systems
{
    public partial struct MovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, movement, agent, entity) 
                     in SystemAPI.Query<RefRO<LocalToWorld>, RefRO<MovementComponent>, RefRW<AgentBody>>().WithEntityAccess())
            {
                agent.ValueRW.SetDestination(movement.ValueRO.TargetPosition);
                if (HasReachedTarget(transform, movement, agent))
                {
                    var ecb = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>()
                        .CreateCommandBuffer(state.WorldUnmanaged);
                    ecb.DestroyEntity(entity);
                }
            }
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        private bool HasReachedTarget(RefRO<LocalToWorld> transform, RefRO<MovementComponent> movement,
            RefRW<AgentBody> agent)
        {
            var distance = agent.ValueRO.Destination - transform.ValueRO.Position;
            return distance.x < movement.ValueRO.TargetReachedThreshold
                   && distance.y < movement.ValueRO.TargetReachedThreshold;
        }
    }
}