using IdleCorp.ECS.Components;
using ProjectDawn.Navigation;
using Unity.Entities;

namespace IdleCorp.ECS.Systems
{
    public partial struct MovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (target, agent) 
                     in SystemAPI.Query<RefRO<MovementComponent>, RefRW<AgentBody>>())
            {
                agent.ValueRW.SetDestination(target.ValueRO.TargetPosition);
            }
        }

        public void OnDestroy(ref SystemState state)
        {
        }
    }
}